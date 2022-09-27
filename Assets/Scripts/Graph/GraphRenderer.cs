using System.Collections.Generic;
using UnityEngine;
using Graph.DataStructure;
 
namespace Graph{

    public class GraphRenderer : MonoBehaviour
    {  
        #region Properties
        public static GraphRenderer Current;

        [SerializeField]
        private Graph.DataStructure.GraphNetwork _graph1;

        public Graph.DataStructure.GraphNetwork graph1 { get { return _graph1; } }

        [SerializeField]
        private GameObject NodesParent;

        [SerializeField]
        private GameObject PageNodePrefab;

        [SerializeField]
        private GameObject CatNodePrefab;

        [SerializeField]
        private GameObject EdgesParent;

        [SerializeField]
        private GameObject CylinderEdgePrefab;

        [SerializeField]
        private GameObject Springs;
        public Dictionary<long, GraphNode> GraphNodes;

        public List<GraphEdge> GraphEdges;

        public float MaxIterations = 10;
        public float area = 0;
        public float k = 0;

        #endregion
        void Awake()
        {
            Current = this;
        }


        public void Initialize(Graph.DataStructure.GraphNetwork graph1, int num)
        {
            _graph1 = graph1;
            Display(num);
            
        }

        private void Display(int initnum)
        {
            // Clear everything
            Clear();

            // Display nodes
            DisplayNodes(initnum);

            // Display edges
            DisplayEdges();
        }
        #region Graph Rendering 
            
        private void Clear()
        {
            // Clear nodes
            GraphNodes = new Dictionary<long, GraphNode>();
            foreach (Transform node in NodesParent.transform)
                GameObject.DestroyImmediate(node.gameObject, true);

            // Clear edges
            GraphEdges = new List<GraphEdge>();
            foreach (Transform edge in CylinderEdgePrefab.transform)
                GameObject.DestroyImmediate(edge.gameObject, true);
        }

        private void DisplayNodes(int initNum)
        {               
            // For each node, create a game object
            foreach (Nodes dnode in graph1?.nodes1)
            {
                if(dnode.Label == "Page")
                {
                    System.Random random = new System.Random();

                    GameObject graphNode = Instantiate(PageNodePrefab, NodesParent.transform);
                    //graphNode.transform.position = startingPosition;
                    graphNode.transform.position = new Vector3(random.Next(0, initNum), random.Next(0, initNum), random.Next(0, initNum));
                    graphNode.transform.rotation = Quaternion.Euler(Vector3.zero);
                    graphNode.transform.name = dnode.Title;

                    // Extract the script                
                    GraphNode nscript = graphNode.GetComponent<GraphNode>();

                    // Initialize data
                    nscript.InitializeNode(dnode, graph1);

                    // Add to list
                    if (!GraphNodes.ContainsKey(dnode.Id))
                    {
                    GraphNodes?.Add(dnode.Id, nscript); 
                    }
                                
                }
                else
                {
                    System.Random random = new System.Random();

                    GameObject graphNode = Instantiate(CatNodePrefab, NodesParent.transform);
                    //graphNode.transform.position = startingPosition;
                    graphNode.transform.position = new Vector3(random.Next(0, initNum), random.Next(0, initNum), random.Next(0, initNum));
                    graphNode.transform.rotation = Quaternion.Euler(Vector3.zero);
                    graphNode.transform.name = dnode.Title;      

                    // Extract the script
                    GraphNode nscript = graphNode.GetComponent<GraphNode>();

                    // Initialize data
                    nscript.InitializeNode(dnode, graph1);

                    // Add to list
                    if (!GraphNodes.ContainsKey(dnode.Id))
                    {
                    GraphNodes?.Add(dnode.Id, nscript); 
                    }
                }
            }
            getInfo();
            k = Mathf.Sqrt(area / (GraphNodes.Count));
        }

        private void DisplayEdges()
        {
            // For each edge, create a game object
            foreach (Edges dedge in graph1?.edges1)
            {
                // Find graph nodes
                if (!GraphNodes.ContainsKey(dedge.StartNodeID) || !GraphNodes.ContainsKey(dedge.EndNodeID))
                    continue;

                GraphNode firstNode = GraphNodes?[dedge.StartNodeID];
                GraphNode secondNode = GraphNodes?[dedge.EndNodeID];

                GameObject graphEdge = Instantiate(CylinderEdgePrefab, EdgesParent.transform);
                graphEdge.transform.position = Vector3.zero;
                graphEdge.transform.localRotation = Quaternion.Euler(Vector3.zero);
                graphEdge.transform.name = dedge.StartNodeID + " -> " + dedge.EndNodeID;

                // Extract the script
                GraphEdge escript = graphEdge.GetComponent<GraphEdge>();

                // Initialize data
                escript.Initialize(dedge, firstNode, secondNode);
                
                // Add to list
                GraphEdges.Add(escript);
                
            }
        }
        public void AddSpringJoint(GameObject ParentNode)
        {
            GameObject springjoint = Instantiate(Springs, ParentNode.transform);

        }

        // Define parameters for graph layout depending on graph size
        private void getInfo()
        {
            if(GraphNodes.Count < 12){
                area = 1000;
                MaxIterations = 10;

            }else if(GraphNodes.Count < 32){
                area = 2000;
                MaxIterations = 20;

            }else if(GraphNodes.Count < 62){
                area = 5000;
                MaxIterations = 30;

            }else if(GraphNodes.Count < 102){
                area = 10000;
                MaxIterations = 60;

            }else if(GraphNodes.Count < 202){
                area = 15000;
                MaxIterations = 80;

            }else if(GraphNodes.Count < 252){
                area = 20000;
                MaxIterations = 110;

            }else if(GraphNodes.Count < 302){
                area = 30000;
                MaxIterations = 130;

            }else if(GraphNodes.Count < 402){
                area = 60000;
                MaxIterations = 150;

            }else if(GraphNodes.Count < 502){
                area = 80000;
                MaxIterations = 170;
            }      
        } 

        #endregion  
    }
}
