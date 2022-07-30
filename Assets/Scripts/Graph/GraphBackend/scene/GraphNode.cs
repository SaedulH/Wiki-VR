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

        [SerializeField]
        private StringSO SO;
    
        [SerializeField]
        [Tooltip("prefab used for the cat canvas.")]
        private GameObject LoadCat;

        [SerializeField]
        [Tooltip("prefab used for the page canvas.")] 
        private GameObject LoadPage; 

        #endregion

        #region Initialization

        /// <summary>
        /// Executes once on start.
        /// </summary>
        private void Awake()
        {
            Rigidbody1 = GetComponent<Rigidbody>();
            //Draggable = GetComponent<Draggable>();
            //Rigidbody.freezeRotation = true;          
            //wait5();

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
        public void InitializeNode(Nodes node)
        {
            _Node = node;

            // Set the color
            //GetComponentInChildren<TextMeshPro>().text = node.Label;
            if(node.Label == "Category")
            {
            GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor",Color.red);
            GetComponentInChildren<Transform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            else
            {
            GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor",Color.yellow);                
            }

            // Set title
            GetComponentInChildren<TextMeshProUGUI>().text = node.ToString();
            
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
        /// Updates the forces applied to the node.
        /// </summary>    
        private void Update()
        {

            /*Rigidbody.velocity = Vector3.zero;

            Vector3 velocity = Vector3.zero;
             if (Forces != null)
                foreach (var force in Forces)
                    velocity += force;

            velocity = velocity.normalized * Mathf.Clamp(velocity.magnitude, 0f, MAX_VELOCITY_MAGNITUDE);

            Rigidbody.AddForce(velocity);*/
        }        

        #endregion

        #region Interactions

        public void checkType()
        {
            
            if(Node.Label == "Category")
            {
                
                SO.Cat = Node.Title;
                Debug.Log("this is the category: "+SO.Cat);
                showLoad();
            }
            else
            {   
                SO.PageName = Node.Title;
                Debug.Log("This is the page: "+ Node.Title);
                showPage();
            }
        }

        public void showLoad()
        {

            GameObject ConfirmCanvas = Instantiate(LoadCat, new Vector3(0, 0, 0), Quaternion.identity);
            var categoryname = ConfirmCanvas.transform.Find("Canvas").Find("catName");
            categoryname.GetComponent<TextMeshProUGUI>().text = SO.Cat+"?";

            ConfirmCanvas.transform.position = Camera.main.transform.position + Camera.main.transform.forward*2f;
            ConfirmCanvas.transform.rotation = Camera.main.transform.rotation;
            Debug.Log("showing cat canvas");
        }

        public void showPage()
        {   

            GameObject PageCanvas = Instantiate(LoadPage, new Vector3(-150, 0, -150), Quaternion.identity);
            var Pagename = PageCanvas.transform.Find("Pagename");
            Pagename.GetComponent<TextMeshProUGUI>().text = SO.PageName;
            
            Camera.main.transform.position = PageCanvas.transform.position + Vector3.up;


            Debug.Log("Show page canvas");
            
        }
        
            #endregion
        }

        
}

