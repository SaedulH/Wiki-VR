using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Graph.DataStructure;
using TMPro;
 
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
    [Tooltip("Template used for initiating nodes.")]
    private GameObject NodePrefab;

        [Header("Edges")]


    [SerializeField]
    [Tooltip("References the parent holding all links.")]
    private GameObject EdgesParent;


    [SerializeField]
    [Tooltip("Template used for initiating links.")]
    private GameObject EdgePrefab;

    public Dictionary<long, GraphNode> GraphNodes;

    public List<GraphEdge> GraphEdges;

    public float area = 0;
    public float maxDisplace = 0;
    public float k = 0;

    private ForceDirectedLayout graphLayout;

    void Awake()
    {
        Current = this;
    }


    public void Initialize(Graph.DataStructure.GraphNetwork graph1)
    {
        _graph1 = graph1;
        Display();
        Debug.Log("Initialized");
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

    private void Display()
    {
        // Clear everything
        Clear();

        // Display nodes
        DisplayNodes();

        // Display links
        DisplayLinks();

        InitialLayout();
    }

    private void Clear()
    {
        // Clear nodes
        GraphNodes = new Dictionary<long, GraphNode>();
        foreach (Transform entity in NodesParent.transform)
            GameObject.DestroyImmediate(entity.gameObject, true);

        // Clear paths
        GraphEdges = new List<GraphEdge>();
        foreach (Transform path in EdgePrefab.transform)
            GameObject.DestroyImmediate(path.gameObject, true);
    }

    private void DisplayNodes()
    {
            // For each position, create an entity
        foreach (Nodes dnode in graph1?.nodes1)
        {
            // Create a new entity instance
            GameObject graphNode = Instantiate(NodePrefab, NodesParent.transform);
            graphNode.transform.position = Vector3.zero;
            graphNode.transform.rotation = Quaternion.Euler(Vector3.zero);
            graphNode.transform.name = dnode.Title;
            

            // Extract the script
            GraphNode nscript = graphNode.GetComponent<GraphNode>();

            // Initialize data
            nscript.InitializeNode(dnode);

            // Add to list
            if (!GraphNodes.ContainsKey(dnode.Id))
            {
            GraphNodes?.Add(dnode.Id, nscript); 
            }
   

        }
        area = GraphNodes.Count * 20F;
        maxDisplace = (float)(Mathf.Sqrt(area) / 3F);
        k = (float)Mathf.Sqrt(area / (1 + GraphNodes.Count));
    }

    private void DisplayLinks()
    {
            // For each position, create an entity
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

    private void InitialLayout()
    {
        System.Random random = new System.Random();
        foreach (var node in GraphNodes.Values)
            node.ApplyInitialForces(new List<Vector3>() { new Vector3(random.Next(-50, 50), random.Next(-50, 50), random.Next(-50, 50)) }, true);
    }

    void Update()
    {
        //graphLayout.DoIterations(3);
    }
}
}
