using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WristMenuFunctions : MonoBehaviour
{

    public bool _Exitpressed = false;
    public bool _Menupressed = false;
    public bool _Catpressed = false;

    [SerializeField]
    public GameObject firstScreen;
    [SerializeField]
    public GameObject secondScreen;

    [SerializeField]
    public TextMeshProUGUI roottext;

    [SerializeField]
    public TextMeshProUGUI rootcatname;

    [SerializeField]
    public TextMeshProUGUI ExitorMenu;

    [SerializeField]
    public TextMeshProUGUI catname;

    [SerializeField]
    private StringSO SO;

    [SerializeField]
    private UIcheckerSO uIcheckerSO;


    [SerializeField]
    [Tooltip("prefab used for the search canvas.")]
    private GameObject SearchCanvas;

    void Start()
    {
        rootcatname.text = SO.Cat;
    }

    public void Exitgamepressed()
    {
        _Exitpressed = true;
        firstScreen.SetActive(false);
        secondScreen.SetActive(true);
        ExitorMenu.text = "Are you sure you want to exit game?";
    }
    
    public void Backtomenupressed()
    {
        _Menupressed = true;
        firstScreen.SetActive(false);
        secondScreen.SetActive(true);
        ExitorMenu.text = "Are you sure you want to return to menu?";
    }

    public void Categorypressed()
    {
        if(SO.LastCat == "")
        {   
            StartCoroutine(Notavailable());
        }
        else
        {
        _Catpressed = true;
        firstScreen.SetActive(false);
        secondScreen.SetActive(true);
        ExitorMenu.text = "Load graph for";
        catname.text = SO.LastCat;            
        }

    }

    IEnumerator Notavailable()
    {   
        roottext.text = "";
        rootcatname.text = "No previous category stored";
        yield return new WaitForSeconds(2);
        roottext.text = "Root Category:";
        rootcatname.text = SO.Cat;
    }

    public void Searchpressed()
    {
        gameObject.SetActive(false);
        showSearch();
    }

    public void Confirmpressed()
    {
        if(_Exitpressed)
        {
             #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        else if(_Menupressed)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if(_Catpressed)
        {

            SO.Cat = SO.LastCat;
            SO.LastCat = SO.SecondLastCat;
            SO.SecondLastCat = "";
            SceneManager.LoadScene("FDG");
            
        }
    }

    public void goBack()
    {
        catname.text = "";
        _Catpressed = false;
        _Exitpressed = false;
        _Menupressed = false;
        secondScreen.SetActive(false);
        firstScreen.SetActive(true);
    }

    public void showSearch()
    {
        uIcheckerSO.showingUI = true;
        GameObject searchCanvas = Instantiate(SearchCanvas, new Vector3(0, 0, 0), Quaternion.identity);

        searchCanvas.transform.position = Camera.main.transform.position + Camera.main.transform.forward*2.5f;
        searchCanvas.transform.rotation = Camera.main.transform.rotation;
        Debug.Log("showing search canvas");
    }    

}
