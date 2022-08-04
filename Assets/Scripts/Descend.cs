
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class Descend : MonoBehaviour
{
    [SerializeField]
    private InputActionReference DescendReference;

    [SerializeField]
    private float DescendForce = 75F;
    
    [SerializeField]
    private Rigidbody _Dbody;

    void Awake()
    {
        _Dbody = GetComponent<Rigidbody>();
        DescendReference.asset.Enable();
    }

    void Update()
    {
        float Dvalue = DescendReference.action.ReadValue<float>();
        OnDescend(Dvalue);
    }

    public void OnDescend(float Dvalue)
    {

        if(Dvalue == 1)
        {
            _Dbody.AddForce(Vector3.down * DescendForce);
        }
        else
        {
            //do nothing
        }            

    }
}
