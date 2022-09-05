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

        private float iterator = 0; 

        public static ForceDirectedLayout currentLayout; 
        private bool _graphReady;
        public bool graphReady
        {
            get { return _graphReady; }
            set
            {
                //Check if the bloolen variable changes from false to true
                if (_graphReady == false && value == true)
                {
                    // Do something
                    StartCoroutine(FreezeGraph());
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

            while(iterator <= GraphRenderer.Current.MaxIterations)
            {
                iterator += Time.deltaTime * 200f;
                
                ApplyForce();
        
            }

            if(iterator >=  GraphRenderer.Current.MaxIterations)
            {
                
                graphReady = true;
                //StartCoroutine(FreezeGraph());
                //FreezeGraph();
            }
		}

        // public void ApplyIterations()
        // {
        //     for(int i = 0; i <= GraphRenderer.Current.MaxIterations; i+=1)
        //     {
        //         if(i == GraphRenderer.Current.MaxIterations)
        //         {
        //             graphReady = true;
        //             FreezeGraph();
        //         }
        //         else
        //         {
        //             ApplyForce();                    
        //         }


        //     }
        // }

        // public void DoIntervalIterations()
		// {

        //     if(iterator <= GraphRenderer.Current.MaxIterations)
        //     {
        //         iterator += Time.deltaTime * 60f;
                
        //         ApplyForce();
        
        //     }
        //     else
        //     {   
        //         graphReady = true;
        //         //StartCoroutine(FreezeGraph());
        //         FreezeGraph();
        //     }
		// }

        // public void cycleLayout()
        // {
        //     for(int i = 0; i <=200; i++)
        //     {
        //         ApplyForce();
        //     }
        // }

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
                            if(n1.Node.Label == n2.Node.Label && n1.Node.Label == "Category"  && dist <= 100)
                            {
                                float repulsiveF = 4F* GraphRenderer.Current.k * GraphRenderer.Current.k / dist;
                                n1.Rigidbody1.AddForce(new Vector3(xDist / dist * repulsiveF, yDist / dist * repulsiveF, zDist / dist * repulsiveF) * GraphRenderer.Current.speed);

                            }
                            else
                            {
                                float repulsiveF = 2F* GraphRenderer.Current.k * GraphRenderer.Current.k / dist;
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

                float attractiveF = (dist * dist)/ GraphRenderer.Current.k;

                if (dist > 0)
                {
                    // if(iterator >= (GraphRenderer.Current.MaxIterations/2) && 
                    if(edge.FirstNode.Node.Label != edge.SecondNode.Node.Label && dist < 100)
                    {
                        //do nothing, nodes are close enough
                    }
                    else if(edge.FirstNode.Node.Label != edge.SecondNode.Node.Label && dist >= 100)
                    {
                        nf.AddForce(new Vector3(-xDist * 50/ dist * attractiveF , -yDist * 50/ dist * attractiveF , -zDist * 50/ dist * attractiveF ) * GraphRenderer.Current.speed);
                        nt.AddForce(new Vector3(xDist * 50/ dist * attractiveF , yDist * 50/ dist * attractiveF , zDist * 50/ dist * attractiveF ) * GraphRenderer.Current.speed);                             
                    }
                    else if(edge.FirstNode.Node.Label == edge.SecondNode.Node.Label && edge.FirstNode.Node.Label == "Category" && dist >= 200)
                    {
                        nf.AddForce(new Vector3(-xDist * 2/ dist * attractiveF , -yDist * 2/ dist * attractiveF , -zDist * 2/ dist * attractiveF ) * GraphRenderer.Current.speed);
                        nt.AddForce(new Vector3(xDist * 2/ dist * attractiveF , yDist * 2/ dist * attractiveF , zDist * 2/ dist * attractiveF ) * GraphRenderer.Current.speed);                        
                    }
                    else
                    {
                        nf.AddForce(new Vector3(-xDist / dist * attractiveF, -yDist / dist * attractiveF, -zDist / dist * attractiveF) * GraphRenderer.Current.speed);
                        nt.AddForce(new Vector3(xDist / dist * attractiveF, yDist / dist * attractiveF, zDist / dist * attractiveF) * GraphRenderer.Current.speed);                         
                    }


                }
            }

        }
	}
}

