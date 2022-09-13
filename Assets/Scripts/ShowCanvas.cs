using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ShowCanvas : MonoBehaviour
{
    [SerializeField]
    [Tooltip("prefab used for the cat canvas.")]
    private GameObject LoadCat;

    [SerializeField]
    [Tooltip("prefab used for the page canvas.")] 
    private GameObject LoadPage;  

    [SerializeField]
    private TextMeshProUGUI ItemName;

    [SerializeField]
    private StringSO SO;

    [SerializeField]
    private UIcheckerSO uIcheckerSO;

    public Animator Toggle;

    public Animator Fade;

    public Animator EnterNode;

    private AudioManager Audio;

    void Awake()
    {
        Fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>();
        Audio = GameObject.FindGameObjectWithTag("Audio").GetComponentInChildren<AudioManager>();

    }

    public void loadnewCat()
    {
        SO.SecondLastCat = SO.LastCat;
        SO.LastCat = SO.Cat;
        SO.Cat = ItemName.text;
        
        Audio.Play("LoadCat");
        StartCoroutine(FadeToCat());
    }

    IEnumerator FadeToCat()
    {
        Fade.SetTrigger("FadeOut");
        EnterNode.SetTrigger("Enter");
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("FDG");
    }

    public void loadnewPage()
    {
        SO.PageName = ItemName.text;

        Audio.Play("LoadPage");
        StartCoroutine(FadeToPage());
    }

    IEnumerator FadeToPage()
    {
        Fade.SetTrigger("FadeOut");
        EnterNode.SetTrigger("Enter");
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("WikiPage");
    }

    public void backfromCat()
    {
        uIcheckerSO.showingUI = false;

        Audio.Play("Back");
        StartCoroutine(TurnOffCat());
        
    }

    IEnumerator TurnOffCat()
    {
       Toggle.SetTrigger("LoadCatOff");
       yield return new WaitForSeconds(1);

       Destroy(LoadCat); 
    }
    public void backfromPage()
    {
        uIcheckerSO.showingUI = false;

        Audio.Play("Back");
        StartCoroutine(TurnOffPage());
    }

    IEnumerator TurnOffPage()
    {
       Toggle.SetTrigger("LoadPageOff");
       yield return new WaitForSeconds(1);

       Destroy(LoadPage); 
    }

    public void HidePageConfirm()
    {
        Toggle.SetTrigger("LoadPageOff");
        gameObject.SetActive(false);
    }
}
