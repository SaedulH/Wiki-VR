using System.Collections;
using UnityEngine;

public class ControlScheme : MonoBehaviour
{

    public Animator Controls;

    public UIcheckerSO uIcheckerSO;

    public void Start()
    {
        uIcheckerSO.showingUI = true;
    }
    public void turnOff()
    {
        StartCoroutine(turnOffControls());
    }

    IEnumerator turnOffControls()
    {
        Controls.SetTrigger("ControlsOff");

        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        uIcheckerSO.showingUI = false;
    }
}
