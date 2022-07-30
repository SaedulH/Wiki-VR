using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graph.DataStructure{
public class GraphNetwork {


    /// <summary>
    /// Empty constructor.
    /// </summary>    
    public GraphNetwork()
        : this(new List<Nodes>(), new List<Edges>())
    {
    }
    
    /// <summary>
    /// Default constructor.
    /// </summary> 
    public GraphNetwork(List<Nodes> nodes, List<Edges> edges)
    {
        _Nodes = nodes;
        _Edges = edges;
    }    

    /// <summary>
    /// Clone constructor.
    /// </summary>
    public GraphNetwork(GraphNetwork graph)
        : this()
    {
        // Clone the nodes
        foreach (var node in graph.nodes1)
            nodes1.Add(new Nodes(node));

        // Clone the links
        foreach (var edge in graph.edges1)
            edges1.Add(new Edges(edge));
    }
    

    [SerializeField]
    private List<Nodes> _Nodes;
    public List<Nodes> nodes1 { get { return _Nodes; } }    


    [SerializeField]
    private List<Edges> _Edges;
    public List<Edges> edges1 { get { return _Edges; } }
}
}