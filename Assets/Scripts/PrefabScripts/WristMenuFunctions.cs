using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Graph;

public class WristMenuFunctions : MonoBehaviour
{

    public static WristMenuFunctions current;

    public bool _Exitpressed = false;
    public bool _Menupressed = false;
    public bool _Catpressed = false;
    public bool MinGraph;

    public Vector3 MyPosition;

    [SerializeField]
    [Tooltip("References the parent holding all nodes.")]
    private GameObject NodesParent;

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
    private GameObject Graph; 

    [SerializeField]
    private GameObject Max;

    [SerializeField]
    private GameObject Min;    

    [SerializeField]
    private GameObject PlayerIcon;

    [SerializeField]
    [Tooltip("prefab used for the search canvas.")]
    private GameObject SearchCanvas;

    public Animator Fade;

    public Animator Toggle;

    public Animator GraphAnim;

    public Animator GraphPanelAnim;


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
        //Toggle.SetBool("FirstScreen", false);
    }
    
    public void Backtomenupressed()
    {
        _Menupressed = true;
         firstScreen.SetActive(false);
         secondScreen.SetActive(true);
        ExitorMenu.text = "Are you sure you want to return to menu?";
        //Toggle.SetBool("FirstScreen", false);
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
        //Toggle.SetBool("FirstScreen", false);        
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

        StartCoroutine(WristOff());
        showSearch();
    }

    IEnumerator WristOff()
    {
        Toggle.SetTrigger("TurnWristOff");
        yield return new WaitForSeconds(1);
        
        gameObject.SetActive(false);
    }

    public void Confirmpressed()
    {
        if(_Exitpressed)
        {
            StartCoroutine(Exiting());

        }
        else if(_Menupressed)
        {
            StartCoroutine(Confirmed("MainMenu"));
        }
        else if(_Catpressed)
        {

            SO.Cat = SO.LastCat;
            SO.LastCat = SO.SecondLastCat;
            SO.SecondLastCat = "";
            StartCoroutine(Confirmed("FDG"));
            
        }
    }

    IEnumerator Exiting()
    {
        Fade.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1);
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    IEnumerator Confirmed(string Scenename)
    {
        Fade.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(Scenename);
    }

    public void goBack()
    {
        catname.text = "";
        _Catpressed = false;
        _Exitpressed = false;
        _Menupressed = false;
         secondScreen.SetActive(false);
         firstScreen.SetActive(true);
        //Toggle.SetBool("FirstScreen", true);
    }

    public void showSearch()
    {
        uIcheckerSO.showingUI = true;
        GameObject searchCanvas = Instantiate(SearchCanvas, new Vector3(0, 0, 0), Quaternion.identity);

        searchCanvas.transform.position = Camera.main.transform.position + Camera.main.transform.forward*2.5f;
        searchCanvas.transform.rotation = Camera.main.transform.rotation;
        Debug.Log("showing search canvas");
    } 

    public void ExpandPressed()
    {
        if(!MinGraph)
        {
            MinimizeGraph();
            MyPosition = GameObject.Find("XR Origin").transform.position;


            GameObject playerIcon = Instantiate(PlayerIcon, NodesParent.transform);
            playerIcon.transform.position = MyPosition;
            playerIcon.transform.name = "PlayerIcon";
            
            Debug.Log("Old Position = "+ MyPosition);
            GameObject.Find("XR Origin").transform.position = new Vector3(0,0,-90);

            GraphAnim.SetBool("MinGraph", true);
            MinGraph = true;

            GraphPanelAnim.SetBool("PanelOn", true);
        }
        else
        {
            MaximizeGraph();
            Destroy(NodesParent.transform.Find("PlayerIcon").gameObject);

            GameObject.Find("XR Origin").transform.position = MyPosition;

            GraphAnim.SetBool("MinGraph", false);
            MinGraph = false;

            GraphPanelAnim.SetBool("PanelOn", false);
        }
    }

    public void MinimizeGraph()
    {
        uIcheckerSO.showingUI = true;
        Min.SetActive(false);
        Max.SetActive(true);
        // Graph.transform.Find("NodesParent").localScale = new Vector3(0.07F,0.07F,0.07F);

        // foreach(Transform node in Graph.transform.Find("NodesParent").transform)
        // {
        //     var MinScale = new Vector3(node.localScale.x * 0.6F, node.localScale.y * 0.6F, node.localScale.z * 0.6F);
        //     node.localScale = MinScale;
        // }
        
        foreach(Transform edge in Graph.transform.Find("EdgesParent").transform)
        {
            var MinScale = new Vector3(0.35F, 0.35F, edge.localScale.z);
            edge.localScale = MinScale;
        }

        if(GraphPanel.current.TopTenPages.Count == 0)
        {
            GenTopPages();
        }
    }   

    public void MaximizeGraph()
    {
        uIcheckerSO.showingUI = false;
        Max.SetActive(false);
        Min.SetActive(true);
        // Graph.transform.Find("NodesParent").localScale = new Vector3(1F,1F,1F);
    
        // foreach(Transform node in Graph.transform.Find("NodesParent").transform)
        // {
        //     var MinScale = new Vector3(node.localScale.x * 1.67F, node.localScale.y * 1.67F, node.localScale.z * 1.67F);
        //     node.localScale = MinScale;
        // }

        foreach(Transform edge in Graph.transform.Find("EdgesParent").transform)
        {
            var MaxScale = new Vector3(0.65F, 0.65F, edge.localScale.z);
            edge.localScale = MaxScale;
        }

    }

    public void GenTopPages()
    {
        StartCoroutine(GraphPanel.current.GeneratePageList());
    }

}
