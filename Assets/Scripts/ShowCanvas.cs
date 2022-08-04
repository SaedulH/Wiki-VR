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

    public void loadnewCat()
    {
        SO.SecondLastCat = SO.LastCat;
        SO.LastCat = SO.Cat;
        SO.Cat = ItemName.text;
        
        SceneManager.LoadScene("FDG");
    }

    public void loadnewPage()
    {
        SO.PageName = ItemName.text;
        SceneManager.LoadScene("WikiPage");
    }

    public void backfromCat()
    {
        uIcheckerSO.showingUI = false;
        Destroy(LoadCat);
    }
    public void backfromPage()
    {
        uIcheckerSO.showingUI = false;
        Destroy(LoadPage);
    }
}
