using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WristUI : MonoBehaviour
{
    
    public InputActionReference MenuReference;

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
        bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);
        //_wristCanvas.enabled = !_wristCanvas.enabled;
    }
}
