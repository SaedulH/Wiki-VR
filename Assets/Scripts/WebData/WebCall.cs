using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

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
        private StringSO SO;
        public string encodedName;

        public string sectionNumber;

        public Dictionary<int, string> ListofContents;

        private string APIintro = "https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro&explaintext&redirects=1&titles=";

        private string APIsection = "https://en.wikipedia.org/w/api.php?format=json&action=parse&prop=wikitext&disabletoc=1&page=";
        private string GetList = "https://en.wikipedia.org/w/api.php?format=json&action=parse&prop=sections&disabletoc=1&page=";

        // Start is called before the first frame update
        private void Start()
        {
            ListofContents = new Dictionary<int, string>();

            
            pageName.text = SO.PageName;
            encodedName = pageName.text.Replace(" ", "%20"); 
            //StartCoroutine(GetRequest());

            StartCoroutine(GetContentList());
            StartCoroutine(GetIntroRequest());
        }


        IEnumerator GetIntroRequest()
        {
            using(UnityWebRequest request = UnityWebRequest.Get(APIintro+encodedName))
            {
                yield return request.SendWebRequest();  

                pageText.text  = request.downloadHandler.text;
                
            }
            
        }

        IEnumerator GetSectionRequest(string sectionNum)
        {
            using(UnityWebRequest request = UnityWebRequest.Get(APIsection+encodedName+"&section="+sectionNum))
            {
                yield return request.SendWebRequest();  

                pageText.text  = request.downloadHandler.text;
                
            }
            
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
            printList();
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
        public void printList()
        {
            foreach(var item in ListofContents)
            {
                Debug.Log(item.Key+" : "+item.Value);
            }
        }

        public void OnButtonPress()
        {
            string buttonText = GetComponentInChildren<TextMeshProUGUI>().text;
            if(buttonText == "Brief Description")
            {
                GetIntroRequest();
            }
            else
            {
                string number = new string(buttonText.SkipWhile(c=>!char.IsDigit(c))
                         .TakeWhile(c=>char.IsDigit(c))
                         .ToArray());
                
                GetSectionRequest(number);
            }
        }
    }
}