using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Graph.DataStructure;
using TMPro;

namespace Graph
{
    public class GraphNode : MonoBehaviour
    {

        #region Values 

        private const float MAX_VELOCITY_MAGNITUDE = 1000f;

        //public Vector3 increaseSize;

        public static GraphNode thisNode;

        [SerializeField]
        private StringSO SO;

        [SerializeField]
        private UIcheckerSO uIcheckerSO;
    
        [SerializeField]
        [Tooltip("prefab used for the loadcat canvas.")]
        private GameObject LoadCat;

        [SerializeField]
        [Tooltip("prefab used for the loadpage canvas.")] 
        private GameObject LoadPage; 

        
        [SerializeField]
        [Tooltip("The graph data being displayed.")]
        private Graph.DataStructure.GraphNetwork _graph1;

        public Graph.DataStructure.GraphNetwork graph1 { get { return _graph1; } }

        #endregion

        #region Initialization

        /// <summary>
        /// Executes once on start.
        /// </summary>
        private void Awake()
        {   
            
            Rigidbody1 = GetComponent<Rigidbody>();
            thisNode = this;
        }

        IEnumerator wait5()
        {       
        yield return new WaitForSeconds(5);
        
            Rigidbody1.angularVelocity = Vector3.zero;
            Rigidbody1.constraints = RigidbodyConstraints.FreezePosition; 
        }

        /// <summary>
        /// Initializes the graph node.
        /// </summary>
        /// <param name="node">The node being presented.</param>
        public void InitializeNode(Nodes node, Graph.DataStructure.GraphNetwork graph1)
        {
            _graph1 = graph1;
            _Node = node;

            // Set title
            GetComponentInChildren<TextMeshProUGUI>().text = node.ToString();
            
            // Set size
            if(node.Label == "Category")
            {
                Vector3 increaseSize = new Vector3(0.02F, 0.02F, 0.02f);
                foreach(var edge in graph1.edges1)
                {
                    if(node.Id == edge.StartNodeID || node.Id == edge.EndNodeID)
                    {
                        //Debug.Log(node.Id + " to " + edge.Type);
                        transform.GetComponentInChildren<Transform>().localScale += increaseSize;
                        
                    }
                }
            }

        }

        #endregion

        #region Fields/Properties

        [SerializeField]
        [Tooltip("The node being presented.")]
        private Nodes _Node;

        public Nodes Node { get { return _Node; } }

        /// <summary>
        /// References the rigid body that handles the movements of the node.
        /// </summary>
        public Rigidbody Rigidbody1;


        /// <summary>
        /// List of all forces to apply.
        /// </summary>
        private List<Vector3> Forces;

        #endregion

        #region Movement

        /// <summary>
        /// Apply forces to the node.
        /// </summary>
        public void ApplyInitialForces(List<Vector3> forces, bool applyImmediately = false)
        {
            if (applyImmediately)
                foreach (var force in forces)
                    Rigidbody1.AddForce(force);
            else
                Forces = forces;
        }         

        
        /// <summary>
        /// Apply initial position to the node.
        /// </summary>
		public void SetPosition(Vector3 position)
		{
			gameObject.transform.position = position;
		}


        #endregion

        #region Interactions

        public void checkType()
        {
            
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

        public void showLoad(string type)
        {
            uIcheckerSO.showingUI = true;
            if(type == "Category")
            {

            GameObject ConfirmCanvas = Instantiate(LoadCat, new Vector3(0, 0, 0), Quaternion.identity);
            var categoryname = ConfirmCanvas.transform.Find("Canvas").Find("catName");
            categoryname.GetComponent<TextMeshProUGUI>().text = Node.Title;

            ConfirmCanvas.transform.position = Camera.main.transform.position + Camera.main.transform.forward*2.25f;
            ConfirmCanvas.transform.rotation = Camera.main.transform.rotation;
            Debug.Log("showing cat canvas");  

            }
            else if(type == "Page")
            {

            GameObject ConfirmCanvas = Instantiate(LoadPage, new Vector3(0, 0, 0), Quaternion.identity);
            var pagename = ConfirmCanvas.transform.Find("Canvas").Find("PageName");
            pagename.GetComponent<TextMeshProUGUI>().text = SO.PageName;

            ConfirmCanvas.transform.position = Camera.main.transform.position + Camera.main.transform.forward*2.25f;
            ConfirmCanvas.transform.rotation = Camera.main.transform.rotation;
            Debug.Log("showing page canvas");                 
            }

        }

        // public void showPage()
        // {   

        //     GameObject PageCanvas = Instantiate(HexaPage, new Vector3(-150, 0, -150), Quaternion.identity);
        //     var Pagename = PageCanvas.transform.Find("Pagename");
        //     Pagename.GetComponent<TextMeshProUGUI>().text = SO.PageName;
            
        //     Camera.main.transform.position = new Vector3(-150, 2, -150);

        //     Debug.Log("Show page canvas");
            
        // }
        
            #endregion
        }

        
}

