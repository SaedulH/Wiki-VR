using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WristUI : MonoBehaviour
{
    
    public InputActionReference MenuReference;

    public Animator Toggle;
    private Canvas _wristCanvas;
    
    void Awake()
    {
        _wristCanvas = GetComponent<Canvas>();
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

        if(gameObject.activeSelf == true)
        {
            StartCoroutine(WristOff());
            
        }
        else if(gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
            //Toggle.SetTrigger("TurnWristOn");

        }
        //_wristCanvas.enabled = !_wristCanvas.enabled;
    }

    IEnumerator WristOff()
    {
        Toggle.SetTrigger("TurnWristOff");
        yield return new WaitForSeconds(1);
        
        gameObject.SetActive(false);
    }
}
