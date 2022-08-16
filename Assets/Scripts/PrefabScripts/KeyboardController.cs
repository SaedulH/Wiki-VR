using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardController : MonoBehaviour
{   
    private KeyboardController keyboardController;

    [SerializeField]
    private GameObject Keyboard;

    [SerializeField]
    private GameObject keys;

    [SerializeField]
    private TMP_InputField searchField;

    public Animator Toggle;
    public bool CapsOn = true;

    
    // Start is called before the first frame update
    void Start()
    {
        keyboardController = this;
        CapsLock();
    }

    // Update is called once per frame
    void Update()
    {
        if(searchField.text == "" && !CapsOn)
        {
            CapsLock();
        }


    }


    public void KeyInput(Button keybutton)
    {
        string key = keybutton.GetComponentInChildren<TextMeshProUGUI>().text;

        if (key == "space")
        {
        searchField.text += " ";
        }
        else
        {
        searchField.text += key;    
        } 


        if(searchField.text.Length == 1 && CapsOn)
        {
            CapsLock();
        }


         
    }

    public void ClearAll()
    {
        searchField.text = "";
    }


    public void BackSpace()
    {
        if(searchField.text != "")
        {
            searchField.text = searchField.text.Remove(searchField.text.Length -1, 1);
        }
        else
        {
            Debug.Log("Inputfield is empty!");
        }
        
    }

    public void Enter()
    {

    }

    public void closeKeyboard()
    {
        StartCoroutine(closeKeyboardCanvas());
        
    }

    IEnumerator closeKeyboardCanvas()
    {
        Toggle.SetTrigger("KeyboardOff");
        
        yield return new WaitForSeconds(0.5F);

        Keyboard.SetActive(false);
    }


    public void CapsLock()
    {
        if(!CapsOn)
        {        
            foreach(Transform key in keys.transform)
            {   
  
                string keyupper = key.GetComponentInChildren<TextMeshProUGUI>().text.ToUpper();
                key.GetComponentInChildren<TextMeshProUGUI>().text = keyupper;
                //Debug.Log(key.GetComponentInChildren<TextMeshProUGUI>().text);
                                
            }
            CapsOn = true;
        }
        else
        {
            foreach(Transform key in keys.transform)
            { 
                string keylower = key.GetComponentInChildren<TextMeshProUGUI>().text.ToLower();
                key.GetComponentInChildren<TextMeshProUGUI>().text = keylower;
                //Debug.Log(key.GetComponentInChildren<TextMeshProUGUI>().text);  
                              
            }
            CapsOn = false;
        }
    }



}
