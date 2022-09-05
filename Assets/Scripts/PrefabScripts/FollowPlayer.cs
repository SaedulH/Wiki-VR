using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private TextMeshProUGUI nodeText;
    public float Range = 300f;

    // Start is called before the first frame update
    void Start()
    {
        //mCamera = Camera.main;
        player = GameObject.Find("XR Origin");
        nodeText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(player.transform.position);  

        transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);

        float distance = Vector3.Distance(player.transform.position, transform.position);

        if(distance <= Range)
        {
            nodeText.enabled = true;
        }
        else
        {
            nodeText.enabled = false;
        }        
    
    }
}
