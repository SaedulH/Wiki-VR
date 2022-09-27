using UnityEngine;
using Graph.DataStructure;

namespace Graph
{
    public class GraphEdge : MonoBehaviour
    {

        public Rigidbody sourceRb;
        public Rigidbody targetRb;

        #region Initialization

        // Initializes the graph edge.
        public void Initialize(Edges edge, GraphNode firstNode, GraphNode secondNode)
        {
            _Edge = edge;
            _FirstNode = firstNode;
            _SecondNode = secondNode;
            
            sourceRb = firstNode.GetComponent<Rigidbody>();
            targetRb = secondNode.GetComponent<Rigidbody>();

            //set color
            if(edge.Type == "SUBCAT_OF")
            {
                GetComponent<Renderer>().material.color = new Color(0.47F,0,0,1);
                GetComponent<Renderer>().material.SetColor ("_EmissionColor", new Color(0,0,0,1));
            }
            else if(edge.Type == "IN_CATEGORY")
            {
                GetComponent<Renderer>().material.color = new Color(0.47F,0.47F,0,1);
                GetComponent<Renderer>().material.SetColor ("_EmissionColor", new Color(0,0,0,1));
            }
        }

        #endregion

        #region Fields/Properties

        // The relationship being presented.
        [SerializeField]
        private Edges _Edge;
        public Edges edge { get { return _Edge; } }

        // The first graph node this edge is attached to.
        [SerializeField]
        private GraphNode _FirstNode;
        public GraphNode FirstNode { get { return _FirstNode; } }

        // The second graph node this edge is attached to.
        [SerializeField]
        private GraphNode _SecondNode;
        public GraphNode SecondNode { get { return _SecondNode; } }

        #endregion

        #region Methods

        // Update the edge to keep the two nodes connected at all times.
        private void Update()
        {
            Vector3 firstPosition = _FirstNode.transform.position + (_SecondNode.transform.position - _FirstNode.transform.position).normalized * 0.1f;
            Vector3 secondPosition = _SecondNode.transform.position + (_FirstNode.transform.position - _SecondNode.transform.position).normalized * 0.1f;

            Vector3 offset = secondPosition - firstPosition;
            Vector3 position = firstPosition + (offset / 2.0f);

            gameObject.transform.position = position;
            gameObject.transform.LookAt(firstPosition);
            Vector3 localScale = gameObject.transform.localScale;
            localScale.z = (secondPosition - firstPosition).magnitude;
            gameObject.transform.localScale = localScale;
        }

        #endregion

    }
}
