using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using WebData;


namespace Graph
{
    public class GraphPanel : MonoBehaviour
    {   
        public static GraphPanel current;

        [SerializeField]
        private GameObject SearchCatCanvas;

        [SerializeField]
        private GameObject NodesParent;

        [SerializeField]
        private GameObject EdgesParent;

        [SerializeField]
        private TextMeshProUGUI TNodes;

        [SerializeField]
        private TextMeshProUGUI TLinks; 

        [SerializeField]
        private TextMeshProUGUI CNodes; 

        [SerializeField]
        private TextMeshProUGUI PNodes;    

        [SerializeField]
        private TextMeshProUGUI CatName;

        [SerializeField]
        private StringSO SO;

        private GraphRenderer graphRenderer;
        private float TnodeCount = 0;
        private float TlinkCount = 0;    
        private float CnodeCount = 0;
        private float PnodeCount = 0; 

        public Dictionary<string, int> ListofPages;

        public List<string> TopTenPages;

        [SerializeField]
        [Tooltip("References the parent holding all Top ten page buttons.")]
        private GameObject TopTenButtonsParent;

        [SerializeField]
        [Tooltip("References the prefab for Top ten page buttons.")]
        private GameObject TopTenButtonsTemplate;

        // Start is called before the first frame update
        void Awake()
        {
            graphRenderer = GameObject.FindGameObjectWithTag("Graph").GetComponent<GraphRenderer>();
            current = this;
            CatName.text = SO.Cat;
            ListofPages = new Dictionary<string, int>();
            //Debug.Log(GetDates());
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnAwake()
        {
       
        }

        public void GetAllInfo()
        {
            GetStats();
            MostViewedStats();

        }

        public void GetStats()
        {

            foreach(Transform nodes in NodesParent.transform)
            {
                
                
                if(nodes.GetComponent<GraphNode>().Node.Label == "Category")
                {
                    
                    CnodeCount +=1;
                }
                else if(nodes.GetComponent<GraphNode>().Node.Label == "Page")
                {
                    
                    PnodeCount +=1;
                }
                
                TnodeCount +=1;
            }

            TlinkCount = graphRenderer.GraphEdges.Count;
            //Debug.Log("Total Nodes: " + TnodeCount);

            TNodes.text = TnodeCount.ToString();
            CNodes.text = CnodeCount.ToString();
            PNodes.text = PnodeCount.ToString();
            TLinks.text = TlinkCount.ToString();
        }

        public void MostViewedStats()
        {      
            //ListofPages = new Dictionary<string, string>();
            foreach(GraphNode nodes in graphRenderer.GraphNodes.Values)
            {
                
                if(nodes.Node.Label == "Page")
                {
                    string encodedName = nodes.Node.Title.Replace(" ", "_");

                    StartCoroutine(GetViewedData(encodedName));
                }
                else
                {
                    //do nothing
                }
            }
            
        }

        public IEnumerator GetViewedData(string encodedName)
        {
            string pageviewRequest = "https://wikimedia.org/api/rest_v1/metrics/pageviews/per-article/en.wikipedia/all-access/all-agents/"+encodedName+"/monthly/"+GetDates();
            //Debug.Log(pageviewRequest);

            using(UnityWebRequest request = UnityWebRequest.Get(pageviewRequest))
            {
                yield return request.SendWebRequest();  
                
                string Content = request.downloadHandler.text;
                var PageViewData = Newtonsoft.Json.JsonConvert.DeserializeObject<PageViewBase>(Content);

                //Debug.Log(encodedName + " : " + PageViewData.items[0].views.ToString());
                if(PageViewData.items == null)
                {
                    //Debug.Log(encodedName);
                    //do nothing
                }
                else
                {
                    ListofPages.Add(encodedName, PageViewData.items[0].views);  
                }
                //TopTenPages.Add(PageViewData.items[0].views.ToString()); 
                //TopTenPages.Add(encodedName); 
                //Debug.Log(ListofPages.Count);


            }

        }

        public string GetDates()
        {
            int month = System.DateTime.Now.Month - 1;
            int year = System.DateTime.Now.Year;
            //Debug.Log("month is " + month);
            //Debug.Log("year is " + year);

            string Period = year+"0"+month+"01/"+year+"0"+month+"31";

            return Period;
        
        }

        public IEnumerator GeneratePageList()
        {
            //Debug.Log("gen page list");
            foreach(var item in ListofPages.OrderByDescending(x => x.Value))
            {
                //Debug.Log(item.Key + " : " + item.Value);

                if(TopTenPages.Count <= 10)
                {
                    TopTenPages.Add(item.Key.Replace("_"," "));
                }
                else
                {
                    break;
                }
            }
            yield return new WaitForSeconds(0.5F);

            GeneratePageButtons();
        }

        public void GeneratePageButtons()
        {
            foreach (var page in TopTenPages)
            {
                GameObject button = Instantiate(TopTenButtonsTemplate, TopTenButtonsParent.transform) as GameObject;
                button.SetActive(true);

                button.GetComponent<TopTenButton>().setText(page);
                
            }
        }

        public void ReturntoGraph()
        {
            //GameObject.Find("XR Origin").transform.Find("Camera Offset").transform.Find("Left Hand Controller").transform.Find("WristCanvas").GetComponent<WristMenuFunctions>().MaximizeGraph();
            //GameObject.Find("XR Origin").GetComponentInChildren<WristMenuFunctions>().MaximizeGraph();
            WristMenuFunctions.current.MaximizeGraph();
        }

        public void SearchInCat()
        {   
            if(SearchCatCanvas.activeSelf == false)
            SearchCatCanvas.SetActive(true);
        }
    }
}