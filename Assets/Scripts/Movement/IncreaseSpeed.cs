using UnityEngine;
using UnityEngine.InputSystem;

public class IncreaseSpeed : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.XR.Interaction.Toolkit.ActionBasedContinuousMoveProvider moveProvider;

    [SerializeField]
    private InputActionReference SpeedReference;
    // Start is called before the first frame update
    void Start()
    {
         SpeedReference.asset.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        float Svalue = SpeedReference.action.ReadValue<float>();
        OnSpeed(Svalue);        
    }

    public void OnSpeed(float Svalue)
    {
        
        if(Svalue == 0)
        {
            moveProvider.moveSpeed = 20F;
        }
        else if(moveProvider.moveSpeed <= 80)
        {
            moveProvider.moveSpeed += Svalue;
        }
           

    }
}
