using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IncreaseVertical : MonoBehaviour
{
    private Ascend ascend;

    private Descend descend;

    [SerializeField]
    private InputActionReference VerticalSpeedReference;
    // Start is called before the first frame update
    void Start()
    {
        VerticalSpeedReference.asset.Enable();
        ascend = gameObject.GetComponent<Ascend>();
        descend = gameObject.GetComponent<Descend>();
    }

    // Update is called once per frame
    void Update()
    {
        float value = VerticalSpeedReference.action.ReadValue<float>();
        OnVertical(value);        
    }

    public void OnVertical(float value)
    {
        
        if(value == 0)
        {
            ascend.AscendForce = 50F;
            descend.DescendForce = 50F;

        }
        else if(ascend.AscendForce < 150)
        {
            ascend.AscendForce += value;
            descend.DescendForce += value;
        }
           

    }
}
