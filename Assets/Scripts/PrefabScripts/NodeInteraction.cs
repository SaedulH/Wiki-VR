using System.Collections;
using UnityEngine;
using Graph;


public class NodeInteraction : MonoBehaviour
{
    public Animator Hover;

    private float ConnectionCounter = 0;
    


    public void OnHover()
    {
        Hover.SetBool("IsHovering", true);
    }

    public void OffHover()
    {
        Hover.SetBool("IsHovering", false);
    }

    //Highlight every connected edge to the node
    public void NodeSelected()
    {   
        Debug.Log("Nodeselected");

        GraphNode graphNode = gameObject.GetComponent<GraphNode>();
        GraphRenderer graphComponents = GameObject.FindGameObjectWithTag("Graph").GetComponent<GraphRenderer>();
        foreach(GraphEdge edges in graphComponents.GraphEdges)
        {
            if(graphNode.Node.Id == edges.FirstNode.Node.Id || graphNode.Node.Id == edges.SecondNode.Node.Id)
            {
                Animator EdgeAnim = edges.GetComponent<Animator>();
                EdgeAnim.SetBool("Selected", true);    

                if(edges.edge.Type == "SUBCAT_OF")
                {   

                    edges.GetComponent<Renderer>().material.SetColor ("_Color", new Color(1,0,0,1));
                    edges.GetComponent<Renderer>().material.SetColor ("_EmissionColor", new Color(1,0,0,1) * 2F);

                }
                else if(edges.edge.Type == "IN_CATEGORY")
                {
                    edges.GetComponent<Renderer>().material.SetColor ("_Color", new Color(0.78F,1,0,1));
                    edges.GetComponent<Renderer>().material.SetColor ("_EmissionColor", new Color(0.78F,1,0,1) * 2F);
                
                }
                ConnectionCounter +=1;
            }
            
        }
        Debug.Log("This node has " + ConnectionCounter + " links");
    }

    // Unhighlight every connected edge to the node
    public void NodeDeselected()
    {
        StartCoroutine(DoNodeDeselected());
    }
    
    public IEnumerator DoNodeDeselected()
    {   
        yield return new WaitForSeconds(1);

        ConnectionCounter = 0;
        GraphNode graphNode = gameObject.GetComponent<GraphNode>();
        GraphRenderer graphComponents = GameObject.FindGameObjectWithTag("Graph").GetComponent<GraphRenderer>();
        foreach(GraphEdge edges in graphComponents.GraphEdges)
        {
            if(graphNode.Node.Id == edges.FirstNode.Node.Id || graphNode.Node.Id == edges.SecondNode.Node.Id)
            {
                Animator EdgeAnim = edges.GetComponent<Animator>();
                EdgeAnim.SetBool("Selected", false); 

                if(edges.edge.Type == "SUBCAT_OF")
                {
                    edges.GetComponent<Renderer>().material.SetColor ("_Color", new Color(0.47F,0,0,1));
                    edges.GetComponent<Renderer>().material.SetColor ("_EmissionColor", new Color(0,0,0,1));

                }
                else if(edges.edge.Type == "IN_CATEGORY")
                {
                    edges.GetComponent<Renderer>().material.SetColor ("_Color", new Color(0.47F,0.47F,0,1));
                    edges.GetComponent<Renderer>().material.SetColor ("_EmissionColor", new Color(0,0,0,1));
                }
            }
        }
        
    }

}
