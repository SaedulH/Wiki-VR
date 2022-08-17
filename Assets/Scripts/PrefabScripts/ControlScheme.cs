using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScheme : MonoBehaviour
{

    private GameObject player;

    public Animator Toggle;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("XR Origin");
        StartCoroutine(turnOffControls());
    }

    // Update is called once per frame
    void Update()
    {

        transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);

        float distance = Vector3.Distance(player.transform.position, transform.position);     
    
    }

    IEnumerator turnOffControls()
    {
        yield return new WaitForSeconds(15);

        Toggle.SetTrigger("ControlsOff");

        yield return new WaitForSeconds(1);

        gameObject.SetActive(false);
    }
}
