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
    private GameObject EdgePrefab;

    public Dictionary<long, GraphNode> GraphNodes;

    public List<GraphEdge> GraphEdges;

    public float MaxIterations = 0;
    public float area = 0;
    public float maxDisplace = 0;
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
        foreach(Nodes nodes in graph1.nodes1)
        {
            Debug.Log(nodes.Id);
        }
        foreach(Edges edge in graph1.edges1){
            Debug.Log(edge.StartNodeID + " -> " +edge.EndNodeID);
        }

    }

    private void Display(int num)
    {
        // Clear everything
        Clear();

        // Display nodes
        DisplayNodes();

        // Display edges
        DisplayEdges();

        //Spread out nodes
        InitialLayout(num);
    }

    private void Clear()
    {
        // Clear nodes
        GraphNodes = new Dictionary<long, GraphNode>();
        foreach (Transform node in NodesParent.transform)
            GameObject.DestroyImmediate(node.gameObject, true);

        // Clear edges
        GraphEdges = new List<GraphEdge>();
        foreach (Transform edge in EdgePrefab.transform)
            GameObject.DestroyImmediate(edge.gameObject, true);
    }

    private void DisplayNodes()
    {               
            // For each position, create an object
        foreach (Nodes dnode in graph1?.nodes1)
        {
            //Vector3 startingPosition = new Vector3(random.Next(-25, 25), random.Next(-25, 25), random.Next(-25, 25));

            // Create a new entity instance

            if(dnode.Label == "Page")
            {
                GameObject graphNode = Instantiate(PageNodePrefab, NodesParent.transform);
                //graphNode.transform.position = startingPosition;
                graphNode.transform.position = Vector3.zero;
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
                GameObject graphNode = Instantiate(CatNodePrefab, NodesParent.transform);
                //graphNode.transform.position = startingPosition;
                graphNode.transform.position = Vector3.zero;
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

        // foreach(GraphNode node in GraphNodes.Values)
        // {
        //     Debug.Log(node.name);
        // }
         
        getInfo();
        maxDisplace = (float)(Mathf.Sqrt(area) / 3F);
        k = (float)Mathf.Sqrt(area / (1 + GraphNodes.Count));
    }


    private void DisplayEdges()
    {
            // For each position, create an object
        foreach (Edges dedge in graph1?.edges1)
        {
            // Find graph nodes
            if (!GraphNodes.ContainsKey(dedge.StartNodeID) || !GraphNodes.ContainsKey(dedge.EndNodeID))
                continue;

            GraphNode firstNode = GraphNodes?[dedge.StartNodeID];
            GraphNode secondNode = GraphNodes?[dedge.EndNodeID];

                // Create a new entity instance
            GameObject graphEdge = Instantiate(EdgePrefab, EdgesParent.transform);
            graphEdge.transform.position = Vector3.zero;
            graphEdge.transform.localRotation = Quaternion.Euler(Vector3.zero);
            graphEdge.transform.name = dedge.StartNodeID + " -> " + dedge.EndNodeID;
             
            //Debug.Log(dedge.StartNodeID + " -> "+ dedge.EndNodeID);
            // Extract the script
            GraphEdge escript = graphEdge.GetComponent<GraphEdge>();

            // Initialize data
            escript.Initialize(dedge, firstNode, secondNode);
              
            // Add to list
            GraphEdges.Add(escript);
            
            
        }
    }
    private void getInfo()
    {
        if(GraphNodes.Count < 12){
            area = 100;
            MaxIterations = 50;
            speed = 15;

        }else if(GraphNodes.Count < 32){
            area = 200;
            MaxIterations = 300;
            speed = 13;

        }else if(GraphNodes.Count < 62){
            area = 500;
            MaxIterations = 500;
            speed = 12;

        }else if(GraphNodes.Count < 102){
            area = 1000;
            MaxIterations = 600;
            speed = 11;

        }else if(GraphNodes.Count < 202){
            area = 4000;
            MaxIterations = 800;
            speed = 10;

        }else if(GraphNodes.Count < 252){
            area = 6000;
            MaxIterations = 1200;
            speed = 9;

        }else if(GraphNodes.Count < 302){
            area = 9000;
            MaxIterations = 1600;
            speed = 8;

        }else if(GraphNodes.Count < 402){
            area = 12000;
            speed = 7;
            MaxIterations = 2000;

        }else if(GraphNodes.Count < 502){
            area = 15000;
            speed = 5;
            MaxIterations = 2200;

        }else{
            area = 18000;
            speed = 5;
            MaxIterations = 2400;
        }         
    }




    private void InitialLayout(int num)
    {
        System.Random random = new System.Random();
        foreach (var node in GraphNodes.Values)
            node.ApplyInitialForces(new List<Vector3>() { new Vector3(random.Next(-num, num), random.Next(-num, num), random.Next(-num, num)) }, true);
    }

    // void Update()
    // {
    //     graphLayout.DoIterations(1);
    // }
}
}
