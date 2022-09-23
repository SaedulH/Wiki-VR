using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Graph.DataStructure;
 
namespace Graph{

public class GraphRenderer : MonoBehaviour
{   

    public static GraphRenderer Current;

        [Header("Data")]   

    [SerializeField]
    [Tooltip("The graph data being displayed.")]
    private Graph.DataStructure.GraphNetwork _graph1;

    public Graph.DataStructure.GraphNetwork graph1 { get { return _graph1; } }


        [Header("Nodes")]

    [SerializeField]
    [Tooltip("References the parent holding all nodes.")]
    private GameObject NodesParent;

    [SerializeField]
    [Tooltip("Prefabs used for initiating Page nodes.")]
    private GameObject PageNodePrefab;

    [SerializeField]
    [Tooltip("Prefabs used for initiating  Category nodes.")]
    private GameObject CatNodePrefab;

        [Header("Edges")]


    [SerializeField]
    [Tooltip("References the parent holding all edges.")]
    private GameObject EdgesParent;


    [SerializeField]
    [Tooltip("Prefabs used for initiating edges.")]
    private GameObject CylinderEdgePrefab;


    [SerializeField]
    private GameObject Springy;
    public Dictionary<long, GraphNode> GraphNodes;

    public List<GraphEdge> GraphEdges;

    public float MaxIterations = 10;
    public float area = 0;
    public float maxDisplacement = 0;
    public float k = 0;
    public float speed = 10;

    private ForceDirectedLayout graphLayout;

    void Awake()
    {
        Current = this;
    }


    public void Initialize(Graph.DataStructure.GraphNetwork graph1, int num)
    {
        _graph1 = graph1;
        Display(num);
        
    }


    public static void CheckData(GraphNetwork graph1)
    {
        Debug.Log("Graph network retrieved!");
        foreach(Nodes nodes in graph1.nodes1)
        {
            Debug.Log(nodes.Id);
        }
        foreach(Edges edge in graph1.edges1){
            Debug.Log(edge.StartNodeID + " -> " +edge.EndNodeID);
        }

    }

    private void Display(int initnum)
    {
        // Clear everything
        Clear();

        // Display nodes
        DisplayNodes(initnum);

        // Display edges
        DisplayEdges();

        //Spread out nodes
        //InitialLayout(num);
    }

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
        maxDisplacement = Mathf.Sqrt(area) / 3F;
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
        GameObject springjoint = Instantiate(Springy, ParentNode.transform);

    }


    private void getInfo()
    {
        if(GraphNodes.Count < 12){
            area = 100;
            MaxIterations = 500;
            speed = 15;

        }else if(GraphNodes.Count < 32){
            area = 200;
            MaxIterations = 2000;
            speed = 13;

        }else if(GraphNodes.Count < 62){
            area = 500;
            MaxIterations = 4000;
            speed = 12;

        }else if(GraphNodes.Count < 102){
            area = 1000;
            MaxIterations = 6000;
            speed = 11;

        }else if(GraphNodes.Count < 202){
            area = 5000;
            MaxIterations = 8000;
            speed = 10;

        }else if(GraphNodes.Count < 252){
            area = 9000;
            MaxIterations = 10000;
            speed = 9;

        }else if(GraphNodes.Count < 302){
            area = 20000;
            MaxIterations = 12000;
            speed = 8;

        }else if(GraphNodes.Count < 402){
            area = 30000;
            speed = 7;
            MaxIterations = 14000;

        }        
    }   

    private void InitialLayout(int num)
    {
        System.Random random = new System.Random();
        foreach (var node in GraphNodes.Values)
            node.ApplyInitialForces(new List<Vector3>() { new Vector3(random.Next(-num, num), random.Next(-num, num), random.Next(-num, num)) }, true);
    }
}
}
