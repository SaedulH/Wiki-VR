using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FollowPlayer : MonoBehaviour
{
    Camera mCamera;

    [SerializeField]
    private TextMeshProUGUI nodeText;
    public float Range = 200f;

    // Start is called before the first frame update
    void Start()
    {
        mCamera = Camera.main;
        nodeText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + mCamera.transform.rotation * Vector3.forward);  

        //transform.rotation = Quaternion.LookRotation(mCamera.transform.forward);

        float distance = Vector3.Distance(Camera.main.transform.position, transform.position);

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
