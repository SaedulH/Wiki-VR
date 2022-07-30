using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graph
{
	public class ForceDirectedLayout : MonoBehaviour
	{
		
		private float speed = 5;

        private GraphRenderer graphComponents;

        private float iterator = 0;
        private float MaxIterations = 1500;  
 

        public void DoIterations()
		{

            while(iterator <= MaxIterations)
            {
                iterator += Time.deltaTime * 200f;
                
                ApplyForce();
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
                        float dist = (float)Mathf.Sqrt(xDist * xDist + yDist * yDist + zDist * zDist);

                        if (dist > 0)
                        {
                            float repulsiveF = GraphRenderer.Current.k * GraphRenderer.Current.k / dist;
                            n1.Rigidbody1.AddForce(new Vector3(xDist / dist * repulsiveF, yDist / dist * repulsiveF, zDist / dist * repulsiveF) * speed);
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
                float dist = (float)Mathf.Sqrt(xDist * xDist + yDist * yDist + zDist * zDist);

                float attractiveF = dist * dist / GraphRenderer.Current.k;

                if (dist > 0)
                {
                    nf.AddForce(new Vector3(-xDist / dist * attractiveF, -yDist / dist * attractiveF, -zDist / dist * attractiveF) * speed);
                    nt.AddForce(new Vector3(xDist / dist * attractiveF, yDist / dist * attractiveF, zDist / dist * attractiveF) * speed);
                }
            }

        }
	}
}

