using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class Ascend : MonoBehaviour
{
    [SerializeField]
    private InputActionReference AscendReference;

    public float AscendForce = 75F;
    
    [SerializeField]
    private Rigidbody _Abody;

    void Awake()
    {
        _Abody = GetComponent<Rigidbody>();
        AscendReference.asset.Enable();
        //AscendReference.action.started += OnAscend;
    }

    void Update()
    {
        float Avalue = AscendReference.action.ReadValue<float>();
        OnAscend(Avalue);
    }

    public void OnAscend(float Avalue)
    {

        if(Avalue == 1)
        {
            _Abody.AddForce(Vector3.up * AscendForce);
        }
        else
        {
            //do nothing
        }            

    }

}
