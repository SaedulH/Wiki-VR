using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace Search
{

    public class SearchManager : MonoBehaviour
    {
        public bool showingPage = false;

        public static SearchManager thissearchManager;

        [SerializeField]
        private Button categoriesButton;
        
        [SerializeField]
        private Button pagesButton;

        [SerializeField]
        private TMP_InputField inputField;

        [SerializeField]
        private SearchListControl searchListControl;

        [SerializeField]
        private GameObject ConfirmPanel;

        [SerializeField]
        private GameObject SearchCanvas;

        [SerializeField]
        private TextMeshProUGUI resultText;

        [SerializeField]
        private UIcheckerSO UIcheckerSO;

        [SerializeField]
        private StringSO SO;

        [SerializeField]
        private GameObject KeyboardCanvas;

        public Animator Toggle;

        public Animator SearchConfirm;

        private Animator Fade;

        void Awake()
        {
            Fade = GameObject.FindGameObjectWithTag("Fade").GetComponentInChildren<Animator>();
            thissearchManager = this;
            showingPage = false;
            checkSelect();
            //inputField.text = "Databases";
            //searchInput();
            //Debug.Log(showingPage);
        }


        // public void switchResults()
        // {
        //     showingPage = !showingPage;
        // }

        public void switchtoCat()
        {
            if(!showingPage)    //is already showing categories
            {
                //do nothing
                return;
            }
            else
            {
                showingPage = false;
            }
            checkSelect();
            searchInput();
        }

        public void switchtoPage()  
        {
            if(showingPage)     //is already showing pages
            {
                //do nothing
                return;
            }
            else
            {
                showingPage = true;
            } 
            checkSelect();
            searchInput();           
        }

        public void checkSelect()
        {    
            if(showingPage)
            {
                pagesButton.GetComponent<Image>().color = new Color32(0, 63, 147, 255);
                categoriesButton.GetComponent<Image>().color = new Color32(0, 63, 147, 100);                

            }
            else
            {
                categoriesButton.GetComponent<Image>().color = new Color32(0, 63, 147, 255);
                pagesButton.GetComponent<Image>().color = new Color32(0, 63, 147, 100);           
            }
            
        }

        public void searchInput()
        {
            if(inputField.text != "")
            {

                if(!showingPage)
                {
                StartCoroutine(searchListControl.SearchCat(inputField.text));    
                }
                else
                {
                StartCoroutine(searchListControl.SearchPage(inputField.text));
                }

                Debug.Log("search complete"); 
            }
            else
            {
                //do nothing
            }           
        }   

        public void backtoSearch()
        {
            StartCoroutine(backtoSearchCanvas());
        }

        IEnumerator backtoSearchCanvas()
        {
            SearchConfirm.SetTrigger("SearchOff");
            yield return new WaitForSeconds(0.75F);

            ConfirmPanel.SetActive(false);
        }

        public void confirmMade()
        {
            if(!showingPage)
            {   
                SO.SecondLastCat = SO.LastCat;
                SO.LastCat = SO.Cat;
                SO.Cat = resultText.text;
                //load graph for category
                StartCoroutine(DoConfirmMade("FDG"));
            }
            else
            {
                SO.PageName = resultText.text;
                //load the page
                StartCoroutine(DoConfirmMade("WikiPage"));
            }
        }

        IEnumerator DoConfirmMade(string scene)
        {   
            Fade.SetTrigger("Start");
            yield return new WaitForSeconds(1);

            SceneManager.LoadScene(scene);
        }

        public void exitCanvas()
        {
            UIcheckerSO.showingUI = false;
            StartCoroutine(CanvasOff());
        }

        IEnumerator CanvasOff()
        {   
            Toggle.SetTrigger("SearchCanvasOff");
            yield return new WaitForSeconds(1);

            //Destroy(SearchCanvas);
            SearchCanvas.SetActive(false);
        }
    

        public void KeyboardOn()
        {
            KeyboardCanvas.SetActive(true);
        }
    }
}
