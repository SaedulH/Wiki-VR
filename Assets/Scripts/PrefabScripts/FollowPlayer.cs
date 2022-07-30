using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    Camera mCamera;
    // Start is called before the first frame update
    void Start()
    {
        mCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + mCamera.transform.rotation * Vector3.forward);
        //transform.rotation = Quaternion.LookRotation(mCamera.transform.forward);
    }
}
