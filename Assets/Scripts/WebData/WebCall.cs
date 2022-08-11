using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using MwParserFromScratch;
using MwParserFromScratch.Nodes;
using System.Text.RegularExpressions;

namespace WebData
{
        
    public class WebCall : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("References the parent holding all buttons.")]
        private GameObject ButtonsParent;

        [SerializeField]
        private GameObject buttonTemplate; 
        [SerializeField]
        private TextMeshProUGUI pageText;

        [SerializeField]
        private TextMeshProUGUI pageName;

        [SerializeField]
        private TextMeshProUGUI parsetester;

        [SerializeField]
        private TextMeshProUGUI normaltester;

        [SerializeField]
        private RawImage InfoboxImage;

        [SerializeField]
        private StringSO SO;
        public string encodedName;

        public string sectionNumber;

        public Dictionary<int, string> ListofContents;

        private string APIintro = "https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro&explaintext&redirects=1&titles=";

        private string APIsection = "https://en.wikipedia.org/w/api.php?format=json&action=parse&prop=wikitext&disabletoc=1&page=";
        private string GetList = "https://en.wikipedia.org/w/api.php?format=json&action=parse&prop=sections&disabletoc=1&page=";

        private string GetRawImage = "https://en.wikipedia.org/w/api.php?format=json&action=query&prop=pageimages&titles=";

        //string experi = "https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&explaintext&disabletoc=1&titles=Racism%20in%20Israel&section=2";

        // Start is called before the first frame update
        private void Start()
        {
            ListofContents = new Dictionary<int, string>();

            
            pageName.text = SO.PageName;
            encodedName = pageName.text.Replace(" ", "%20"); 
            //StartCoroutine(GetRequest());

            StartCoroutine(GetContentList());
            StartCoroutine(GetIntroRequest());
            StartCoroutine(GetImageURL());
            //StartCoroutine(GetSectionRequest("1"));
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
                pageText.text = pageData.query.pages[firstKey].extract;
                
                
            }
            Debug.Log("got intro");
        }

        public IEnumerator GetSectionRequest(string sectionNum)
        {
            pageText.text = "Loading...";
            string sectionRequest = APIsection+encodedName+"&section="+sectionNum;
            Debug.Log(sectionRequest);

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
                //pageText.text = contentData.parse.sections[0].line;

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
            Debug.Log(GetRawImage+encodedName);
            using(UnityWebRequest request = UnityWebRequest.Get(GetRawImage+encodedName))
            {
                yield return request.SendWebRequest();

                string Content = request.downloadHandler.text;
                var imageData = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageBase>(Content);

                var firstKey = imageData.query.pages.First().Key;

                var ImageURL = imageData.query.pages[firstKey].thumbnail.source;

                //string pixel = ImageURL.Split("")[1].Split("")[0]; 

                var HDImageURL = Regex.Replace(ImageURL, @"jpg/([\w-]+)px", "jpg/400px");

                Debug.Log(HDImageURL);
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

        public void generateContentButtons()
        {   
            foreach (var contentData in ListofContents)
            {
                GameObject button = Instantiate(buttonTemplate, ButtonsParent.transform) as GameObject;
                button.SetActive(true);

                button.GetComponent<ContentListButton>().setText(contentData.Key + ":" + contentData.Value);
                
            }
        }

        public void parseText(string content)
        {
            string spaces = content.Replace("\n\n", "\n\n ");
            string nosquarecontent = spaces.Replace("[[", "").Replace("]]", "").Replace("[", "").Replace("]", "");
            string nocurlycontent = Regex.Replace(nosquarecontent, @"/{([^}]*)}/g", "");
            string noUrlText = Regex.Replace(nocurlycontent, @"http[^\s]+", "");

            pageText.text = noUrlText;

        }
        public void printList()
        {
            foreach(var item in ListofContents)
            {
                Debug.Log(item.Key+" : "+item.Value);
            }
        }


    }
}