using System;

namespace Graph.DataStructure{

    [Serializable]
    public class Edges
    {
        #region Constructors

        // Default Constructor
        public Edges(string type, long startNode, long endNode)
        {
            Type = type;
            StartNodeID = startNode;
            EndNodeID = endNode;
        }
        // Clone Constructor
        public Edges(Edges edge)
            : this(edge.Type, edge.StartNodeID, edge.EndNodeID)
        {
        } 
        #endregion

        #region Properties
        
        // The type of relationship of the edge
        public string Type;
        // The Node ID at the first endpoint
        public long StartNodeID;
        // The Node ID at the second endpoint
        public long EndNodeID;

        #endregion
    }
}