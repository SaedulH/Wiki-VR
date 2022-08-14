using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{

    [SerializeField]
    private Material blueGlass;

    [SerializeField]
    private Material blueGlassHovered;

    public Animator Fade;

    public void OnHover()
    {
        GetComponentInChildren<MeshRenderer>().material = blueGlassHovered;
    }

    public void OffHover()
    {
        GetComponentInChildren<MeshRenderer>().material = blueGlass; 
    }

    public void Exited()
    {
        StartCoroutine(exitPressed());
    }


    IEnumerator exitPressed()
    {
        Fade.SetTrigger("Start");
        
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("FDG");
    }
}
