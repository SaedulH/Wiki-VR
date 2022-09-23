using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graph
{
	public class ForceDirectedLayout : MonoBehaviour
	{
        [SerializeField]
        private GameObject GraphParent; 
		
        private GraphRenderer graphComponents;

        private float iterations = 0; 

        public static ForceDirectedLayout currentLayout; 
        private bool _graphReady;
        public bool graphReady
        {
            get { return _graphReady; }
            set
            {
                //Check if the boolean variable changes from false to true
                if (_graphReady == false && value == true)
                {
                    Debug.Log("finished!");
                    StartCoroutine(FreezeGraph());
                    GraphPanel.current.GetAllInfo();
                }
                //Update the boolean variable
                _graphReady = value;
            }
        }

        void Awake()
        {
            currentLayout = this;
            graphReady = false;
        } 

        public void DoIterations()
		{
            
            while(iterations <= GraphRenderer.Current.MaxIterations)
            {
                //iterations += 1;
                iterations += Time.deltaTime * 200F;
                ApplyForce();
        
            }

            if(iterations >  GraphRenderer.Current.MaxIterations)
            { 
                graphReady = true;
                
            }
		}

        public void InitializeForces()
        {
            graphComponents = GetComponent<GraphRenderer>();
        }

        IEnumerator FreezeGraph()
        {
            yield return new WaitForSeconds(5);

            foreach(Transform node in GraphParent.transform.Find("NodesParent").transform)
            {
                
                node.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                //node.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                node.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                node.transform.localRotation = Quaternion.Euler(0,0,0);


            }

            foreach(Transform edge in GraphParent.transform.Find("EdgesParent").transform)
            {
                edge.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                //edge.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                edge.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }

            Debug.Log("Graph frozen");
                
        }
        
        public void ApplyForce() 
        {
            // Calculate the repulsive forces between every node in the graph         
            foreach (GraphNode n1 in graphComponents.GraphNodes.Values)
            {
                foreach (GraphNode n2 in graphComponents.GraphNodes.Values)
                {
                    if (n1.Node.Id != n2.Node.Id)
                    {
                        float xDist = n1.transform.position.x - n2.transform.position.x;
                        float yDist = n1.transform.position.y - n2.transform.position.y;
                        float zDist = n1.transform.position.z - n2.transform.position.z;
                        float dist = Vector3.Distance(n1.transform.position, n2.transform.position);

                        float repulsiveF = (GraphRenderer.Current.k * GraphRenderer.Current.k) / dist;

                        //n1.Rigidbody1.AddForce(new Vector3(xDist / dist * repulsiveF, yDist / dist * repulsiveF, zDist / dist * repulsiveF));
                        Vector3 displacement = new Vector3(xDist, yDist, zDist);
                        //n1.transform.position = n1.transform.position + ((displacement/dist) * repulsiveF);
                        n1.Displacement += ((displacement/dist) * repulsiveF); 
                        
                    }
                }
            }
            // Calculate the attractive forces between nodes that are connected
            foreach (GraphEdge edge in graphComponents.GraphEdges)
            {
                Rigidbody startNode = edge.sourceRb;
                Rigidbody endNode = edge.targetRb;
 
                float xDist = startNode.transform.position.x - endNode.transform.position.x;
                float yDist = startNode.transform.position.y - endNode.transform.position.y;
                float zDist = startNode.transform.position.z - endNode.transform.position.z;
                float dist = Vector3.Distance(startNode.transform.position, endNode.transform.position);

                float attractiveF = (dist * dist)/ GraphRenderer.Current.k;

                //startNode.AddForce(new Vector3(-xDist / dist * attractiveF, -yDist / dist * attractiveF, -zDist / dist * attractiveF));
                //endNode.AddForce(new Vector3(xDist / dist * attractiveF, yDist / dist * attractiveF, zDist / dist * attractiveF)); 

                Vector3 displacement = new Vector3(xDist, yDist, zDist);
                //startNode.transform.position = startNode.transform.position - ((displacement/dist) * attractiveF);
                //endNode.transform.position = endNode.transform.position + ((displacement/dist) * attractiveF);
                edge.FirstNode.Displacement -= ((displacement/dist) * attractiveF * 0.66F);
                edge.SecondNode.Displacement += ((displacement/dist) * attractiveF * 0.66F);
            }

            foreach (GraphNode node in graphComponents.GraphNodes.Values)
            {
                float traveldistance = node.Displacement.magnitude;
                node.transform.position = node.transform.position + ((node.Displacement / traveldistance));
            }
            
        }
	}
    }
    

