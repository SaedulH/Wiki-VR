using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PageOptions : MonoBehaviour
{   

    [SerializeField]
    private GameObject searchCanvas;

    [SerializeField]
    private GameObject optionCanvas;

    [SerializeField]
    private TextMeshProUGUI questionText;

    [SerializeField]
    private TextMeshProUGUI catNameText;

    [SerializeField]
    private StringSO SO;

    public bool backtocatPressed = false;
    public bool menuPressed = false;
    public bool exitPressed = false;

    public void showSearch()
    {
        searchCanvas.SetActive(true);
    }

    public void showMenuOptions()
    {
        menuPressed = true;
        optionCanvas.SetActive(true);
        questionText.text = "Are you sure you want to return to menu?";
        catNameText.text = "";
    }

    public void showExitOptions()
    {
        exitPressed = true;
        optionCanvas.SetActive(true);
        questionText.text = "Are you sure you want to exit application";
        catNameText.text = "";
    }

    public void showCatOptions()
    {
        backtocatPressed = true;
        optionCanvas.SetActive(true);
        questionText.text = "Load graph for";
        catNameText.text = SO.Cat;
    }

    // Update is called once per frame
    public void goBack()
    {   
        menuPressed = false;
        exitPressed = false;
        backtocatPressed =false;
        optionCanvas.SetActive(false);
    }
    
    public void confirmMade()
    {
        if(menuPressed)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if(exitPressed)
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        else if(backtocatPressed)
        {
            SceneManager.LoadScene("FDG");
        }
    }
 
}
