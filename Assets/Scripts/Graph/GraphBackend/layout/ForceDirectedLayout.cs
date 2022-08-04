using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graph
{
	public class ForceDirectedLayout : MonoBehaviour
	{
		
        private GraphRenderer graphComponents;

        private float iterator = 0; 

        public bool graphReady = false;

        public void DoIterations()
		{

            while(iterator <= GraphRenderer.Current.MaxIterations)
            {
                iterator += Time.deltaTime * 200f;
                
                ApplyForce();
        
            }

            if(iterator >=  GraphRenderer.Current.MaxIterations)
            {
                fixRotation();
                graphReady = true;
            }
		}

        public void cycleLayout()
        {
            for(int i = 0; i <=200; i++)
            {
                ApplyForce();
            }
        }

        public void InitializeForces()
        {
            graphComponents = GetComponent<GraphRenderer>();
        }

        public void fixRotation()
        {
            foreach(GraphNode node in graphComponents.GraphNodes.Values)
            {
                node.transform.localRotation = Quaternion.Euler(0,0,0);
            }
        }

        public void ApplyForce() {
                      
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

                        if (dist > 0)
                        {
                            if(n1.Node.Label == n2.Node.Label && n1.Node.Label == "Category")
                            {
                                float repulsiveF = 5F* GraphRenderer.Current.k * GraphRenderer.Current.k / dist;
                                n1.Rigidbody1.AddForce(new Vector3(xDist / dist * repulsiveF, yDist / dist * repulsiveF, zDist / dist * repulsiveF) * GraphRenderer.Current.speed);

                            }
                            else
                            {
                                float repulsiveF = GraphRenderer.Current.k * GraphRenderer.Current.k / dist;
                                n1.Rigidbody1.AddForce(new Vector3(xDist / dist * repulsiveF, yDist / dist * repulsiveF, zDist / dist * repulsiveF) * GraphRenderer.Current.speed);
                            }


                        }
                    }
                }
            }

            foreach (GraphEdge edge in graphComponents.GraphEdges)
            {
                Rigidbody nf = edge.sourceRb;
                Rigidbody nt = edge.targetRb;

                float xDist = nf.transform.position.x - nt.transform.position.x;
                float yDist = nf.transform.position.y - nt.transform.position.y;
                float zDist = nf.transform.position.z - nt.transform.position.z;
                float dist = Vector3.Distance(nf.transform.position, nt.transform.position);

                float attractiveF = (5F* dist * dist)/ GraphRenderer.Current.k;

                if (dist > 0)
                {
                    
                    nf.AddForce(new Vector3(-xDist / dist * attractiveF, -yDist / dist * attractiveF, -zDist / dist * attractiveF) * GraphRenderer.Current.speed);
                    nt.AddForce(new Vector3(xDist / dist * attractiveF, yDist / dist * attractiveF, zDist / dist * attractiveF) * GraphRenderer.Current.speed);
                }
            }

        }
	}
}

