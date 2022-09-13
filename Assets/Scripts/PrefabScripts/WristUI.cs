using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WristUI : MonoBehaviour
{
    public InputActionReference MenuReference;

    [SerializeField]
    private GameObject WristMenu;

    public Animator Toggle;

    void Awake()
    {
        
        MenuReference.asset.Enable();
        MenuReference.action.performed += ToggleMenu;
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        MenuReference.action.performed -=ToggleMenu;
    }

    public void ToggleMenu(InputAction.CallbackContext context)
    {   

        if(WristMenu.activeSelf == true)
        {
            StartCoroutine(WristOff());
            //Toggle.SetTrigger("TurnWristOff");
            
        }
        else if(WristMenu.activeSelf == false)
        {
            WristMenu.SetActive(true);
            //Toggle.SetTrigger("TurnWristOn");

        }
        //_wristCanvas.enabled = !_wristCanvas.enabled;
    }

    IEnumerator WristOff()
    {
        Toggle.SetTrigger("TurnWristOff");
        yield return new WaitForSeconds(1);
        
        WristMenu.SetActive(false);
    }
}
