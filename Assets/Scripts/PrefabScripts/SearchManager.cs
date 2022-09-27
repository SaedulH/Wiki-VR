using System.Collections;
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

        private AudioManager Audio;

        void Awake()
        {
            Fade = GameObject.FindGameObjectWithTag("Fade").GetComponentInChildren<Animator>();
            Audio = GameObject.FindGameObjectWithTag("Audio").GetComponentInChildren<AudioManager>();
            thissearchManager = this;
            showingPage = false;
            checkSelect();
        }

        public void switchtoCat()
        {
            Audio.Play("Forward");
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
            Audio.Play("Forward");
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

        public void switchtoGraphCat()
        {
            Audio.Play("Forward");
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
            GraphSearchInput();
        }

        public void switchtoGraphPage()  
        {
            Audio.Play("Forward");
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
            GraphSearchInput();           
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
            Debug.Log("Searching...");
            Audio.Play("Forward");
            if(inputField.text != "")
            {
                
                if(!showingPage)
                {
                    Debug.Log("Searching cat");
                    StartCoroutine(searchListControl.SearchCat(inputField.text));    
                }
                else
                {
                    Debug.Log("Searching page");
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
            Audio.Play("Back");
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
                Audio.Play("LoadGraph");
                //load graph for category
                StartCoroutine(DoConfirmMade("FDG"));
                
            }
            else
            {
                SO.PageName = resultText.text;
                Audio.Play("LoadPage");

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
            Audio.Play("Back");
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
            Audio.Play("Forward");
            KeyboardCanvas.SetActive(true);
        }


        public void GraphSearchInput()
        {
            Audio.Play("Forward");
            if(inputField.text != "")
            {

                if(!showingPage)
                {
                StartCoroutine(searchListControl.GraphSearchCat(inputField.text));    
                }
                else
                {
                StartCoroutine(searchListControl.GraphSearchPage(inputField.text));
                }

                Debug.Log("search complete"); 
            }
            else
            {
                //do nothing
            }           
        } 
    }
}
