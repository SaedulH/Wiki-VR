using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Graph.DataStructure{

[Serializable]
public class Edges
{

    /// Default Constructor
    public Edges(string type, long startNode, long endNode)
    {
        Type = type;
        StartNodeID = startNode;
        EndNodeID = endNode;
    }
    /// Clone Constructor
    public Edges(Edges edge)
        : this(edge.Type, edge.StartNodeID, edge.EndNodeID)
    {
    }    

    public string Type;
    public long StartNodeID;
    public long EndNodeID;

}
}