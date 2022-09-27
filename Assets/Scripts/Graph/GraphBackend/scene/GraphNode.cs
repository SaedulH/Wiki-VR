using System.Collections.Generic;
using UnityEngine;
using Graph.DataStructure;
using TMPro;

namespace Graph
{
    public class GraphNode : MonoBehaviour
    {

        #region Values 

        public static GraphNode thisNode;

        [SerializeField]
        private StringSO SO;

        [SerializeField]
        private UIcheckerSO uIcheckerSO;
    
        [SerializeField]
        private GameObject LoadCat;

        [SerializeField]
        private GameObject LoadPage; 

        
        [SerializeField]
        private Graph.DataStructure.GraphNetwork _graph1;

        public Graph.DataStructure.GraphNetwork graph1 { get { return _graph1; } }

        #endregion

        #region Initialization

        private void Awake()
        {   
            
            Rigidbody1 = GetComponent<Rigidbody>();
            thisNode = this;
        }

        // Initializes the graph node.
        public void InitializeNode(Nodes node, Graph.DataStructure.GraphNetwork graph1)
        {
            _graph1 = graph1;
            _Node = node;

            //Set title
            GetComponentInChildren<TextMeshProUGUI>().text = node.ToString();
            
            //Set size
            if(node.Label == "Category")
            {
                Vector3 increaseSize = new Vector3(0.025F, 0.025F, 0.025f);
                foreach(var edge in graph1.edges1)
                {
                    if(node.Id == edge.StartNodeID || node.Id == edge.EndNodeID)
                    {
                        transform.GetComponentInChildren<Transform>().localScale += increaseSize;
                    }
                }
            }
        }

        #endregion

        #region Fields/Properties

        [SerializeField]
        private Nodes _Node;

        public Nodes Node { get { return _Node; } }

        // References the rigid body that handles the movements of the node.
        public Rigidbody Rigidbody1;

        // List of all forces to apply. (no longer used)
        private List<Vector3> Forces;

        #endregion

        #region Movement

        // Apply forces to the node. (no longer used)
        public void ApplyInitialForces(List<Vector3> forces, bool applyImmediately = false)
        {
            if (applyImmediately)
                foreach (var force in forces)
                    Rigidbody1.AddForce(force);
            else
                Forces = forces;
        }         

        // total displacement of node.
        public Vector3 Displacement = Vector3.zero;

        #endregion

        #region Interactions

        public void checkType()
        {
            GameObject.FindGameObjectWithTag("Audio").GetComponentInChildren<AudioManager>().Play("Forward");
            
            if(Node.Label == "Category")
            {
                Debug.Log("this is the category: "+Node.Title);
                showLoad("Category");
            }
            else
            {   
                SO.PageName = Node.Title;
                Debug.Log("This is the page: "+ Node.Title);
                showLoad("Page");
            }
        }

        // spawn confirm canvas for either page or category
        public void showLoad(string type)
        {
            uIcheckerSO.showingUI = true;
            if(type == "Category")
            {

            GameObject ConfirmCanvas = Instantiate(LoadCat, new Vector3(0, 0, 0), Quaternion.identity);
            var categoryname = ConfirmCanvas.transform.Find("BorderGroup").Find("CanvasGroup").Find("catName");
            categoryname.GetComponent<TextMeshProUGUI>().text = Node.Title;
            ConfirmCanvas.GetComponent<ShowCanvas>().EnterNode = gameObject.GetComponent<Animator>();

            ConfirmCanvas.transform.position = Camera.main.transform.position + Camera.main.transform.forward*2.25f;
            ConfirmCanvas.transform.rotation = Camera.main.transform.rotation;
            Debug.Log("showing cat canvas");  

            }
            else if(type == "Page")
            {

            GameObject ConfirmCanvas = Instantiate(LoadPage, new Vector3(0, 0, 0), Quaternion.identity);
            var pagename = ConfirmCanvas.transform.Find("BorderGroup").Find("CanvasGroup").Find("PageName");
            pagename.GetComponent<TextMeshProUGUI>().text = SO.PageName;
            ConfirmCanvas.GetComponent<ShowCanvas>().EnterNode = gameObject.GetComponent<Animator>();

            ConfirmCanvas.transform.position = Camera.main.transform.position + Camera.main.transform.forward*2.25f;
            ConfirmCanvas.transform.rotation = Camera.main.transform.rotation;
            Debug.Log("showing page canvas");                 
            }

        }
        #endregion
    }       
}

