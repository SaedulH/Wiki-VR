using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PageOptions : MonoBehaviour
{   
    #region Values
        
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

    public Animator Fade;
    public Animator ConfirmUI;

    public bool backtocatPressed = false;
    public bool menuPressed = false;
    public bool exitPressed = false;

    #endregion

    #region Page functions 

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

    
    public void goBack()
    {   
        menuPressed = false;
        exitPressed = false;
        backtocatPressed =false;
        StartCoroutine(Back());

    }

    IEnumerator Back()
    {
        ConfirmUI.SetTrigger("OptionsOff");

        yield return new WaitForSeconds(0.5F);

        optionCanvas.SetActive(false);
    }
    
    public void confirmMade()
    {
        if(menuPressed)
        {
            StartCoroutine(Confirmation("menu"));
        }
        else if(exitPressed)
        {
            StartCoroutine(Confirmation("exit"));
        }
        else if(backtocatPressed)
        {
            StartCoroutine(Confirmation("page"));           
        }
    }

    IEnumerator Confirmation(string choice)
    {
        Fade.SetTrigger("Start");

        yield return new WaitForSeconds(0.5F);

        if(choice == "exit")
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif            
        }
        else if(choice == "menu")
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if(choice == "page")
        {
            //SO.PageName = SO.lastPage;
            SceneManager.LoadScene("WikiPage");
        }
    }
    #endregion
}

