using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Menu;

    [SerializeField]
    private GameObject Choices;

    [SerializeField]
    private GameObject Settings;
    
    [SerializeField]
    private GameObject Categories;

    [SerializeField]
    private GameObject Confirm;

    [SerializeField]
    private StringSO catNameSO;

    [SerializeField]
    private TextMeshProUGUI confirmCat;

    public Animator transition;

    public void StartGame()
    {
        Menu.SetActive(false);
        Choices.SetActive(true);
        Categories.SetActive(true);
    }

    public void BacktoMenu()
    {

        Choices.SetActive(false);
        Categories.SetActive(false);
        Settings.SetActive(false);             
        Menu.SetActive(true);   
    }

    public void toSettings()
    {
        Menu.SetActive(false);
        Settings.SetActive(true);
    }

    public void StartTransition()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        transition.SetTrigger("Fadeout");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("FDG");
    }


    public void ExitGame(){
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }


    public void toChoices()
    {
        Choices.SetActive(true);
        Confirm.SetActive(false);
        
    }

        public void OpenfrontPanel()
    {
        Choices.SetActive(false);
        Confirm.SetActive(true);

        confirmCat.text = catNameSO.Cat+"?";
    }
}
