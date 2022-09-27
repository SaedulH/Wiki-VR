using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{   
    #region Values
    public static MenuManager current;

    private NeoQuery neoQuery;

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
    private StringSO SO;

    [SerializeField]
    private TextMeshProUGUI confirmCat;

    [SerializeField]
    private GameObject SearchCanvas;

    [SerializeField]
    private GameObject RandomCanvas;   

    public Animator Fade;
    public Animator StartUI;
    public Animator SettingsUI;
    public Animator ConfirmUI; 
    public Animator RandomUI;   
    public Animator ChoicesUI;
    public Animator Toggle;
    public Animator CatAnim;

    [SerializeField]
    private UnityEngine.XR.Interaction.Toolkit.ActionBasedContinuousTurnProvider smoothProvider;

    [SerializeField]
    private UnityEngine.XR.Interaction.Toolkit.ActionBasedSnapTurnProvider snapProvider;

    #endregion

    void Start()
    {
        current = this;
        checkTurn();
    }

    //checks the selected turn method
    public void checkTurn()
    {
        if(SO.Snapturn)
        {
            snapProvider.enabled = true;
            smoothProvider.enabled = false;
        }
        else 
        {   
            snapProvider.enabled = false;
            smoothProvider.enabled = true;
        }
        
    }

    #region MenuUI
    public void StarttoChoices()
    {
        StartCoroutine(StarttoChoicesMenu());
    }

    IEnumerator StarttoChoicesMenu()
    {
        StartUI.SetTrigger("MenuOff");
        CatAnim.SetBool("CatsOn", true);

        yield return new WaitForSeconds(0.33F);


        Menu.SetActive(false);
        Choices.SetActive(true);
    }
      
    

    public void StarttoSettings()
    {
        StartCoroutine(StarttoSetingsMenu());
    }


    IEnumerator StarttoSetingsMenu()
    {
        StartUI.SetTrigger("MenuOff");

        yield return new WaitForSeconds(0.5F);
        
        Menu.SetActive(false);
        Settings.SetActive(true);
    }
    

    public void ChoicestoStart()
    {
        StartCoroutine(ChoicestoStartMenu());
    }

    IEnumerator ChoicestoStartMenu()
    {
        ChoicesUI.SetTrigger("ChoicesOff");
        CatAnim.SetBool("CatsOn", false);

        yield return new WaitForSeconds(0.5F);
        
        Choices.SetActive(false);
        Menu.SetActive(true);

    }

    public void SettingstoStart()
    {
        StartCoroutine(SettingstoStartMenu());
    }

    IEnumerator SettingstoStartMenu()
    {
        SettingsUI.SetTrigger("SettingsOff");

        yield return new WaitForSeconds(0.5F);

        Settings.SetActive(false);
        Menu.SetActive(true);
    }

    public void ConfirmtoChoices()
    {
        StartCoroutine(ConfirmtoChoicesMenu());
        
    }

    IEnumerator ConfirmtoChoicesMenu()
    {
        ConfirmUI.SetTrigger("ConfirmOff");

        yield return new WaitForSeconds(0.66F);

        Confirm.SetActive(false);
        Choices.SetActive(true);
    }

    public void toConfirm()
    {
        StartCoroutine(toComfirmMenu());
    }

    IEnumerator toComfirmMenu()
    {
        ChoicesUI.SetTrigger("ChoicesOff");

        yield return new WaitForSeconds(0.33F);
        Choices.SetActive(false);

        confirmCat.text = SO.Cat;
        Confirm.SetActive(true);

    }
    public void toRandom()
    {
        StartCoroutine(toRandomMenu());
    }

    IEnumerator toRandomMenu()
    {
        ChoicesUI.SetTrigger("ChoicesOff");

        yield return new WaitForSeconds(0.33F);
        
        Choices.SetActive(false);
        RandomCanvas.SetActive(true);

    }

    public void StartTransitionCat()
    {
        StartCoroutine(LoadScene("FDG"));
    }

    public void StartTransitionPage()
    {
        StartCoroutine(LoadScene("WikiPage"));
    }

    IEnumerator LoadScene(string Type)
    {
        Fade.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(Type);
    }

    public void toSearchCanvas()
    {
        StartCoroutine(toSearchCanvasMenu());
    }

    IEnumerator toSearchCanvasMenu()
    {
        ChoicesUI.SetTrigger("ChoicesOff");

        yield return new WaitForSeconds(0.5F);

        Choices.SetActive(false);
        SearchCanvas.SetActive(true);
    }

    public void fromSearchCanvas()
    {
        StartCoroutine(fromSearchCanvasMenu());
    }

    IEnumerator fromSearchCanvasMenu()
    {
        Toggle.SetTrigger("SearchCanvasOff");

        yield return new WaitForSeconds(0.5F);

        Choices.SetActive(true);
        SearchCanvas.SetActive(false);
    }


    public void fromRandom()
    {
        StartCoroutine(fromRandomMenu());
    }
    
    IEnumerator fromRandomMenu()
    {
        RandomUI.SetTrigger("RandomOff");

        yield return new WaitForSeconds(0.5F);

        Choices.SetActive(true);
        RandomCanvas.SetActive(false);
    }

    public void ExitGame()
    {
        StartCoroutine(Exited());

    }

    IEnumerator Exited()
    {
        Fade.SetTrigger("Start");

        yield return new WaitForSeconds(1);

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif

    }

    #endregion
}
