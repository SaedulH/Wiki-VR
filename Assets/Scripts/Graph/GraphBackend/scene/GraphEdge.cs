using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Graph.DataStructure;

namespace Graph
{
    public class GraphEdge : MonoBehaviour
    {

        public Rigidbody sourceRb;
        public Rigidbody targetRb;

        #region Initialization

        /// <summary>
        /// Executes once on start.
        /// </summary>
        private void Awake()
        {
            // LineRenderer = GetComponent<LineRenderer>();
        }

        /// <summary>
        /// Initializes the graph edge.
        /// </summary>
        /// <param name="link">The link being presented.</param>
        /// <param name="firstNode">The first graph node this entity is attached to.</param>
        /// <param name="secondNode">The second graph node this entity is attached to.</param>
        public void Initialize(Edges edge, GraphNode firstNode, GraphNode secondNode)
        {
            _Edge = edge;
            _FirstNode = firstNode;
            _SecondNode = secondNode;

            
            // Set width
            //float width = 0.08f + 0.02f; // [0.02 -> 0.1]


            sourceRb = firstNode.GetComponent<Rigidbody>();
            targetRb = secondNode.GetComponent<Rigidbody>();

            if(edge.Type == "SUBCAT_OF")
            {
                // LineRenderer.startWidth = 1f;
                // LineRenderer.endWidth = 0.5f;
                //GetComponent<Renderer>().material.color = Color.green
                //GetComponent<Renderer>().material.SetColor ("_Color", Color.green);
                GetComponent<Renderer>().material.SetColor ("_EmissionColor", Color.green);

            }
            else if(edge.Type == "IN_CATEGORY")
            {
                // LineRenderer.startWidth = 0.5f;
                // LineRenderer.endWidth = 0.25f;
                //GetComponent<Renderer>().material.color = Color.blue;
                //GetComponent<Renderer>().material.SetColor ("_Color", Color.magenta);
                GetComponent<Renderer>().material.SetColor ("_EmissionColor", Color.magenta);
            }
        }

        #endregion

        #region Fields/Properties

        /// <summary>
        /// The link being presented.
        /// </summary>
        [SerializeField]
        [Tooltip("The link being presented.")]
        private Edges _Edge;

        /// <summary>
        /// The link being presented.
        /// </summary>
        public Edges edge { get { return _Edge; } }



        /// <summary>
        /// The first graph node this entity is attached to.
        /// </summary>
        [SerializeField]
        private GraphNode _FirstNode;

        /// <summary>
        /// The first graph node this entity is attached to.
        /// </summary>
        public GraphNode FirstNode { get { return _FirstNode; } }



        /// <summary>
        /// The second graph node this entity is attached to.
        /// </summary>
        [SerializeField]
        private GraphNode _SecondNode;

        /// <summary>
        /// The second graph node this entity is attached to.
        /// </summary>
        public GraphNode SecondNode { get { return _SecondNode; } }



        /// <summary>
        /// References the line renderer that displays the link.
        /// </summary>
        private LineRenderer LineRenderer;

        #endregion

        #region Methods

        /// <summary>
        /// Update the line to keep the two nodes connected.
        /// </summary>
        private void Update()
        {
            //LineRenderer.useWorldSpace = true;

            Vector3 firstPosition = _FirstNode.transform.position + (_SecondNode.transform.position - _FirstNode.transform.position).normalized * 0.1f;
            Vector3 secondPosition = _SecondNode.transform.position + (_FirstNode.transform.position - _SecondNode.transform.position).normalized * 0.1f;

            // LineRenderer.SetPosition(0, firstPosition);
            // LineRenderer.SetPosition(1, secondPosition);

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
