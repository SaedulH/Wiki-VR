using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using MwParserFromScratch;
using System.Text.RegularExpressions;

namespace WebData
{
        
    public class WebCall : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("References the parent holding all content list buttons.")]
        private GameObject ButtonsParent;

        [SerializeField]
        [Tooltip("References the parent holding all content list buttons.")]
        private GameObject buttonTemplate; 

        [SerializeField]
        [Tooltip("References the parent holding all saved page buttons.")]
        private GameObject SavedButtonsParent;

        [SerializeField]
        [Tooltip("References the parent holding all saved page buttons.")]
        private GameObject SavedbuttonTemplate; 

        [SerializeField]
        private TextMeshProUGUI AddorRemove;

        [SerializeField]
        private TextMeshProUGUI pageText;

        [SerializeField]
        private TextMeshProUGUI pageName;

        [SerializeField]
        private TextMeshProUGUI parsetester;

        [SerializeField]
        private RawImage InfoboxImage;

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

        private string APIintro = "https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro&explaintext&redirects=1&titles=";

        private string APIsection = "https://en.wikipedia.org/w/api.php?format=json&action=parse&prop=wikitext&disabletoc=1&page=";
        private string GetList = "https://en.wikipedia.org/w/api.php?format=json&action=parse&prop=sections&disabletoc=1&page=";

        private string GetRawImage = "https://en.wikipedia.org/w/api.php?format=json&action=query&prop=pageimages&titles=";

        private string GetFeatured = "https://api.wikimedia.org/feed/v1/wikipedia/en/featured/2022/08/14";

        private string GetRandom = "https://en.wikipedia.org/w/api.php?action=query&format=json&list=random&rnnamespace=0&rnlimit=1";

        //string experi = "https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&explaintext&disabletoc=1&titles=Racism%20in%20Israel&section=2";

        // Start is called before the first frame update
        private void Start()
        {
            ListofContents = new Dictionary<int, string>();

            
            pageName.text = SO.PageName;
            encodedName = pageName.text.Replace(" ", "%20"); 

            StartCoroutine(GetContentList());
            StartCoroutine(GetIntroRequest());
            StartCoroutine(GetImageURL());
            StartCoroutine(GetSavedList());
            StartCoroutine(GetFeaturedArticle());
            StartCoroutine(GetRandomURL());
        
        }


        public IEnumerator GetIntroRequest()
        {   
            pageText.text = "Loading...";
            //Debug.Log(APIintro+encodedName); 
            using(UnityWebRequest request = UnityWebRequest.Get(APIintro+encodedName))
            {
                yield return request.SendWebRequest();  

                string Content = request.downloadHandler.text;
                var pageData = Newtonsoft.Json.JsonConvert.DeserializeObject<IntroBase>(Content);
                var firstKey = pageData.query.pages.First().Key;

                // var parser = new WikitextParser();
                // var ast = parser.Parse(pageData.query.pages[firstKey].extract);
                // pageText.text =  ast.Lines.ToString();
                pageText.text = pageData.query.pages[firstKey].extract;
                
                
            }
            Debug.Log("got intro");
        }

        public IEnumerator GetSectionRequest(string sectionNum)
        {
            pageText.text = "Loading...";
            string sectionRequest = APIsection+encodedName+"&section="+sectionNum;
            //Debug.Log(sectionRequest);

            using(UnityWebRequest request = UnityWebRequest.Get(sectionRequest))
            {
                yield return request.SendWebRequest();  
                
                string Content = request.downloadHandler.text;
                var pageData = Newtonsoft.Json.JsonConvert.DeserializeObject<SectionBase>(Content);

                var parser = new WikitextParser();

                var ast = parser.Parse(pageData.parse.wikitext.ToString());
                

                //parseText(ast.ToPlainText());
                //pageText.text =  ast.ToPlainText();                
                //parsetester.text = ast.ToPlainText();
                //normaltester.text = ast.ToString();
                //pageText.text = pageData.parse.wikitext.ToString();
                //pageText.text  = request.downloadHandler.text;
                    string content = ast.ToPlainText();
                    //string ready = Regex.Replace(content, @'/"*":, "=="/', "");
                    string spaces = Regex.Replace(content, @"\n\n", "\n\n ");
                    string nosquarecontent = spaces.Replace("[[", "").Replace("]]", "").Replace("[", "").Replace("]", "");
                    string nocurlycontent = Regex.Replace(nosquarecontent, @"/{([^}]*)}/g", "");
                    string noUrlText = Regex.Replace(nocurlycontent, @"http[^\s]+", "");
                        pageText.text = noUrlText;


            }
            Debug.Log("got section");
        }

        IEnumerator GetContentList()
        {
            using(UnityWebRequest request = UnityWebRequest.Get(GetList+encodedName))
            {
                yield return request.SendWebRequest();  

                string Content  = request.downloadHandler.text;
                var contentData = Newtonsoft.Json.JsonConvert.DeserializeObject<Base>(Content);                
                
                    foreach(var item in contentData.parse.sections)
                    {
                        ListofContents.Add(item.index, item.line);
                    }

            }
            yield return new WaitForSeconds(1); 

            generateContentButtons(); 
            //printList();
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

                var ImageURL = imageData.query.pages[firstKey].thumbnail.source;

                //string pixel = ImageURL.Split("")[1].Split("")[0]; 

                var HDImageURL = Regex.Replace(ImageURL, @"/([\w-]+)px", "/400px");

                //Debug.Log(HDImageURL);
                StartCoroutine(GetImage(HDImageURL));

                //https://upload.wikimedia.org/wikipedia/commons/thumb/e/e1/Hitler_portrait_crop.jpg/37px-Hitler_portrait_crop.jpg
                //https://upload.wikimedia.org/wikipedia/commons/thumb/e/e1/Hitler_portrait_crop.400-Hitler_portrait_crop.jpg
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

                button.GetComponent<ContentListButton>().setText(contentData.Key + ":" + contentData.Value);
                
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
                SO.Saved.Add(pageName.text);

                GameObject button = Instantiate(SavedbuttonTemplate, SavedButtonsParent.transform) as GameObject;
                button.SetActive(true);

                button.GetComponent<SavedListButton>().setText(pageName.text);

                AddorRemove.text = "Remove";
                

            }
            else
            {
                SO.Saved.Remove(pageName.text);

                foreach (Transform savebutton in SavedButtonsParent.transform)
                {
                    if(savebutton.GetComponentInChildren<TextMeshProUGUI>().text == pageName.text)
                    {
                        GameObject.Destroy(savebutton.gameObject);
                    }
                }
                AddorRemove.text = "Add";
            }
        
            //StartCoroutine(GetSavedList());
            
        }

        IEnumerator GetFeaturedArticle()
        {
            using(UnityWebRequest request = UnityWebRequest.Get(GetFeatured))
            {
                yield return request.SendWebRequest();
                string Content = request.downloadHandler.text;

                var contentData = Newtonsoft.Json.JsonConvert.DeserializeObject<FeaturedBase>(Content);
                featuredText.text = contentData.tfa.displaytitle; 
            }
        }

        public void Visit()
        {
            StartCoroutine(LoadFeatured());
        }

        IEnumerator LoadFeatured()
        {
            SO.PageName = featuredText.text;
            Fade.SetTrigger("Start");

            yield return new WaitForSeconds(0.5F);

            SceneManager.LoadScene("WikiPage");

        }

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

                Debug.Log("about to parse");
                
                var contentData = Newtonsoft.Json.JsonConvert.DeserializeObject<RandomBase>(Content);
                randomPageTitle = contentData.query.random[0].title; 
                Debug.Log("random page = " + randomPageTitle);
            }
        }

        IEnumerator LoadRandomPage()
        {
            SO.PageName = randomPageTitle;
            Fade.SetTrigger("Start");

            yield return new WaitForSeconds(0.5F);
            SceneManager.LoadScene("WikiPage");
        }

        public void parseText(string content)
        {
            string spaces = content.Replace("\n\n", "\n\n ");
            string nosquarecontent = spaces.Replace("[[", "").Replace("]]", "").Replace("[", "").Replace("]", "");
            string nocurlycontent = Regex.Replace(nosquarecontent, @"/{([^}]*)}/g", "");
            string noUrlText = Regex.Replace(nocurlycontent, @"http[^\s]+", "");

            pageText.text = noUrlText;

        }

    }
}