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
        private TextMeshProUGUI confirmationText;

        [SerializeField]
        private TextMeshProUGUI resultText;

        [SerializeField]
        private UIcheckerSO UIcheckerSO;

        [SerializeField]
        private StringSO SO;

        public Animator Toggle;
        void Awake()
        {
            showingPage = false;
            checkSelect();
            inputField.text = "Databases";
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

        public void selectMade()
        {
            ConfirmPanel.SetActive(true);
            if(!showingPage)
            {
                confirmationText.text = "Load graph for";   
            }
            else
            {
                confirmationText.text = "Load page for";
            }
            resultText.text = GetComponentInChildren<TextMeshProUGUI>().text;            
        }

        public void backtoSearch()
        {
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
                SceneManager.LoadScene("FDG");
            }
            else
            {
                SO.PageName = resultText.text;
                //load the page
                SceneManager.LoadScene("WikiPage");
            }
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

            Destroy(SearchCanvas);
        }
    }


}
