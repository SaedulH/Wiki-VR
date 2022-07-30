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
    public bool _Searchpressed = false;

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

    public StringSO SO;

    // Start is called before the first frame update

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
        ExitorMenu.text = "Load Graph for";
        catname.text = SO.LastCat;            
        }

    }

    IEnumerator Notavailable()
    {   
        roottext.text = "";
        rootcatname.text = "No Previous Category";
        yield return new WaitForSeconds(2);
        roottext.text = "Root Category:";
        rootcatname.text = SO.Cat;
    }

    public void Searchpressed()
    {
        _Searchpressed = true;
        firstScreen.SetActive(false);
        secondScreen.SetActive(true);
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
        else if(_Searchpressed)
        {
            //do nothing yet
        }
    }

    public void goBack()
    {
        _Catpressed = false;
        _Exitpressed = false;
        _Menupressed = false;
        _Searchpressed = false;
        secondScreen.SetActive(false);
        firstScreen.SetActive(true);
    }

}
