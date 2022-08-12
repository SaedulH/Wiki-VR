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

    void Awake()
    {
        Fade = GameObject.FindGameObjectWithTag("PageCrossfade").GetComponent<Animator>();
    }

    public void loadnewCat()
    {
        SO.SecondLastCat = SO.LastCat;
        SO.LastCat = SO.Cat;
        SO.Cat = ItemName.text;
        
        StartCoroutine(FadeToCat());
    }

    IEnumerator FadeToCat()
    {
        Fade.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("FDG");
    }

    public void loadnewPage()
    {
        SO.PageName = ItemName.text;
        
        StartCoroutine(FadeToPage());
    }

    IEnumerator FadeToPage()
    {
        Fade.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("WikiPage");
    }

    public void backfromCat()
    {
        uIcheckerSO.showingUI = false;
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
        StartCoroutine(TurnOffPage());
    }

    IEnumerator TurnOffPage()
    {
       Toggle.SetTrigger("LoadPageOff");
       yield return new WaitForSeconds(1);

       Destroy(LoadPage); 
    }

}
