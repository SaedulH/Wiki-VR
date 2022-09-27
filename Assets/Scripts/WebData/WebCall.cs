using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebData
{     
    public class WebCall : MonoBehaviour
    {
        #region Values

        [SerializeField]
        [Tooltip("References the parent holding all content list buttons.")]
        private GameObject ButtonsParent;

        [SerializeField]
        [Tooltip("References the prefab for content list buttons.")]
        private GameObject buttonTemplate; 

        [SerializeField]
        [Tooltip("References the parent holding all saved page buttons.")]
        private GameObject SavedButtonsParent;

        [SerializeField]
        [Tooltip("References the prefab for saved page buttons.")]
        private GameObject SavedbuttonTemplate; 
        [SerializeField]
        [Tooltip("References the parent holding all saved page buttons.")]

        private GameObject RelatedButtonsParent;

        [SerializeField]
        [Tooltip("References the prefab for saved page buttons.")]
        private GameObject RelatedbuttonTemplate; 

        [SerializeField]
        private TextMeshProUGUI AddorRemove;

        [SerializeField]
        private TextMeshProUGUI pageText;

        [SerializeField]
        private TextMeshProUGUI pageName;

        [SerializeField]
        private RawImage InfoboxImage;

        [SerializeField]
        private GameObject ImageCanvas;

        [SerializeField]
        private TextMeshProUGUI ImageCaption;

        [SerializeField]
        private GameObject CaptionBorder;

        [SerializeField]
        private TextMeshProUGUI featuredText;

        [SerializeField]
        private StringSO SO;

        public Animator Fade;
        public string encodedName;

        public string randomPageTitle;

        public string sectionNumber;

        public Dictionary<int, string> ListofContents;

        public List<string> ListofSaved;

        public List<string> ListofRelated;

        private string APIintro = "https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro&explaintext&redirects=1&titles=";

        private string APIsection = "https://en.wikipedia.org/w/api.php?format=json&action=parse&prop=wikitext&disabletoc=1&page=";
        private string GetList = "https://en.wikipedia.org/w/api.php?format=json&action=parse&prop=sections&disabletoc=1&page=";

        private string GetRelated = "https://en.wikipedia.org/w/api.php?format=json&action=query&prop=links&pllimit=max&titles=";

        private string GetRawImage = "https://en.wikipedia.org/w/api.php?format=json&action=query&prop=pageimages&titles=";

        private string GetFeatured = "https://api.wikimedia.org/feed/v1/wikipedia/en/featured/";

        private string GetRandom = "https://en.wikipedia.org/w/api.php?action=query&format=json&list=random&rnnamespace=0&rnlimit=1";

        private string GetInfoBox = "https://en.wikipedia.org/w/api.php?action=query&prop=revisions&rvprop=content&format=jsonfm&rvsection=0&titles=";

        #endregion

        // Start is called before the first frame update
        private void Start()
        {
            ListofContents = new Dictionary<int, string>();

            
            pageName.text = SO.PageName;
            encodedName = pageName.text.Replace(" ", "%20"); 

            StartCoroutine(SequenceStart());
        
        }

        //Get intro and check for redirect title
        IEnumerator SequenceStart()
        {
            yield return StartCoroutine(GetIntroRequest());
            yield return ConcurrentAPIs();
        }

        //Get the rest of the page
        public IEnumerator ConcurrentAPIs()
        {   
            yield return new WaitForEndOfFrame();

            StartCoroutine(GetContentList());
            StartCoroutine(GetImageURL());
            StartCoroutine(GetSavedList());
            StartCoroutine(GetFeaturedArticle());
            StartCoroutine(GetRandomURL());
            StartCoroutine(GetRelatedList());
        }        

        public IEnumerator GetIntroRequest()
        {   
            pageText.text = "Loading...";
            Debug.Log(APIintro+encodedName); 
            using(UnityWebRequest request = UnityWebRequest.Get(APIintro+encodedName))
            {
                yield return request.SendWebRequest();  

                string Content = request.downloadHandler.text;
                var pageData = Newtonsoft.Json.JsonConvert.DeserializeObject<IntroBase>(Content);
                var firstKey = pageData.query.pages.First().Key;

                // var parser = new WikitextParser();
                // var ast = parser.Parse(pageData.query.pages[firstKey].extract);
                // pageText.text =  ast.Lines.ToString();
                pageText.text = pageData.query.pages[firstKey].extract.Replace("\n", "\n\n");

                if(pageData.query.redirects != null)
                {
                Debug.Log(pageData.query.redirects[0].to);
                encodedName = pageData.query.redirects[0].to;    
                }
                
                
            }
            Debug.Log("got intro");
        }

        public IEnumerator GetSectionRequest(int sectionNum)
        {
            pageText.text = "Loading...";
            string sectionRequest = APIsection+encodedName+"&section="+sectionNum.ToString();

            using(UnityWebRequest request = UnityWebRequest.Get(sectionRequest))
            {
                yield return request.SendWebRequest();  
                
                string Content = request.downloadHandler.text;
                var pageData = Newtonsoft.Json.JsonConvert.DeserializeObject<SectionBase>(Content);

                WebData.PageParser pageParser = new PageParser();

                pageText.text = pageParser.parseText(pageData.parse.wikitext.ToString());
            }
            Debug.Log("got section");
        }

        IEnumerator GetContentList()
        {
            using(UnityWebRequest request = UnityWebRequest.Get(GetList+encodedName))
            {
                yield return request.SendWebRequest();  

                string Content  = request.downloadHandler.text;
                var contentData = Newtonsoft.Json.JsonConvert.DeserializeObject<ContentBase>(Content);                
                
                    foreach(var item in contentData.parse.sections)
                    {
                        if(item.line.Contains("See also") || item.line.Contains("References") || item.line.Contains("Notes"))
                        {
                            //do nothing
                        }
                        else
                        {
                            ListofContents.Add(item.index, item.number + ": " + item.line);
                        }
                    }

            }
            yield return new WaitForSeconds(1); 

            generateContentButtons(); 
        }

        IEnumerator GetRelatedList()
        {
            using(UnityWebRequest request = UnityWebRequest.Get(GetRelated+encodedName))
            {
                yield return request.SendWebRequest();  

                string Content  = request.downloadHandler.text;
                var pageData = Newtonsoft.Json.JsonConvert.DeserializeObject<RelatedBase>(Content);                
                var firstKey = pageData.query.pages.First().Key;

                    foreach(var item in pageData.query.pages[firstKey].links)
                    {
                        if(item.title.Contains("Category:") || item.title.Contains("Wikipedia:") || item.title.Contains("Template:") || item.title.Contains("Template talk:") || item.title.Contains("Help:"))
                        {
                            //do nothing
                        }
                        else
                        {
                            ListofRelated.Add(item.title);
                        }
                    }

            }
            yield return new WaitForSeconds(1); 

            generateRelatedButtons(); 
        }        

        IEnumerator GetImageURL()
        {   
            //Debug.Log(GetRawImage+encodedName);
            using(UnityWebRequest request = UnityWebRequest.Get(GetRawImage+encodedName))
            {
                yield return request.SendWebRequest();

                string Content = request.downloadHandler.text;
                var imageData = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageBase>(Content);

                var firstKey = imageData.query.pages.First().Key;

                if(imageData.query.pages[firstKey].thumbnail == null)
                {
                    CaptionBorder.SetActive(true);
                    ImageCaption.text = "No Image Available";
                    ImageCanvas.SetActive(false);
                }
                else
                {
                    var ImageURL = imageData.query.pages[firstKey].thumbnail.source;
                    var HDImageURL = Regex.Replace(ImageURL, @"/([\w-]+)px", "/400px");

                    StartCoroutine(GetImage(HDImageURL));
                }

            } 
        }

        IEnumerator GetImage(string imageurl)
        {
            using(UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageurl))
            {
                yield return request.SendWebRequest();  

                if (request.result != UnityWebRequest.Result.Success) 
                {
                    Debug.Log(request.error);
                    Debug.Log("No Infobox image here lol");
                }
                else 
                {
                    Texture retrievedTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                    InfoboxImage.texture = retrievedTexture;                     
                }
            }
        }

        IEnumerator GetSavedList()
        {   
            
            foreach(string page in SO.Saved)
            {
                ListofSaved.Add(page);
            }
            yield return new WaitForSeconds(1);

            if(ListofSaved.Contains(pageName.text))
            {
                AddorRemove.text = "Remove";
            }
            else
            {
                AddorRemove.text = "Add";
            }    

            generateSavedButtons();
        }

        public void generateContentButtons()
        {   
            foreach (var contentData in ListofContents)
            {
                GameObject button = Instantiate(buttonTemplate, ButtonsParent.transform) as GameObject;
                button.SetActive(true);

                button.GetComponent<ContentListButton>().setText(contentData.Value);
                
            }
            Selectedfromlist("Brief Description");
        }

        public void generateRelatedButtons()
        {   
            foreach (var page in ListofRelated)
            {
                GameObject button = Instantiate(RelatedbuttonTemplate, RelatedButtonsParent.transform) as GameObject;
                button.SetActive(true);

                button.GetComponent<SavedListButton>().setText(page);
            }
        }

        public void generateSavedButtons()
        {   
            
            foreach (var page in ListofSaved)
            {
                GameObject button = Instantiate(SavedbuttonTemplate, SavedButtonsParent.transform) as GameObject;
                button.SetActive(true);
                button.GetComponent<SavedListButton>().setText(page);
                
            }
        }

        public void AddorRemoveSave()
        {
            if(AddorRemove.text == "Add")
            {
                GameObject.FindGameObjectWithTag("Audio").GetComponentInChildren<AudioManager>().Play("Forward");

                SO.AddtoSaved(pageName.text);

                GameObject button = Instantiate(SavedbuttonTemplate, SavedButtonsParent.transform) as GameObject;
                button.SetActive(true);
                button.GetComponent<SavedListButton>().setText(pageName.text);

                AddorRemove.text = "Remove";
            }
            else
            {
                GameObject.FindGameObjectWithTag("Audio").GetComponentInChildren<AudioManager>().Play("Back");

                SO.RemovefromSaved(pageName.text);

                foreach (Transform savebutton in SavedButtonsParent.transform)
                {
                    if(savebutton.GetComponentInChildren<TextMeshProUGUI>().text == pageName.text)
                    {
                        GameObject.Destroy(savebutton.gameObject);
                    }
                }
                AddorRemove.text = "Add";
            }
        }

        // Get today's featured article on Wikipedia
        IEnumerator GetFeaturedArticle()
        {
            using(UnityWebRequest request = UnityWebRequest.Get(GetFeatured + System.DateTime.Now.ToString("yyyy/MM/dd")))
            {
                yield return request.SendWebRequest();
                string Content = request.downloadHandler.text;

                var contentData = Newtonsoft.Json.JsonConvert.DeserializeObject<FeaturedBase>(Content);

                Regex regexbracket = new Regex("<[^>]+>");
                featuredText.text = regexbracket.Replace(contentData.tfa.displaytitle, "");
            }
        }

        public void Visit()
        {
            StartCoroutine(LoadFeatured());
        }

        IEnumerator LoadFeatured()
        {
            SO.PageName3 = SO.PageName2;
            SO.PageName2 = SO.PageName;
            SO.PageName = featuredText.text;
            Fade.SetTrigger("Start");

            yield return new WaitForSeconds(0.5F);

            SceneManager.LoadScene("WikiPage");
        }

        // Pull a random page title from Wikipedia
        public void GetRandomPage()
        {
            StartCoroutine(LoadRandomPage());
        }

        IEnumerator GetRandomURL()
        {
            using(UnityWebRequest request = UnityWebRequest.Get(GetRandom))
            {
                yield return request.SendWebRequest();
                string Content = request.downloadHandler.text; 
                
                var contentData = Newtonsoft.Json.JsonConvert.DeserializeObject<RandomBase>(Content);
                randomPageTitle = contentData.query.random[0].title; 
                Debug.Log("random page = " + randomPageTitle);
            }
        }

        IEnumerator LoadRandomPage()
        {
            SO.PageName3 = SO.PageName2;
            SO.PageName2 = SO.PageName;
            SO.PageName = randomPageTitle;
            Fade.SetTrigger("Start");

            yield return new WaitForSeconds(0.5F);
            SceneManager.LoadScene("WikiPage");
        }

        public void Selectedfromlist(string Value)
        {
            foreach(Transform button in ButtonsParent.transform)
            {
                if(button.GetComponentInChildren<TextMeshProUGUI>().text == Value)
                {
                    button.GetComponent<Image>().color = new Color32(0,63,147,255);
                    button.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(180,180,180,255);
                }
                else
                {
                    button.GetComponent<Image>().color = new Color32(185,185,185,255);
                    button.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(50,50,50,255);
                }
            }
        }
    }
}