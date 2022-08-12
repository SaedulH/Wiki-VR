using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Graph;


public class NodeInteraction : MonoBehaviour
{

    [SerializeField]
    private Material PageMaterial;

    [SerializeField]
    private Material CatMaterial; 

    [SerializeField]
    private Material PageSelectedMaterial;

    [SerializeField]
    private Material CatSelectedMaterial; 

    [SerializeField]
    private GraphNode graphNode;

    public Animator Hover;
    
    void start()
    {
        graphNode = gameObject.GetComponent<GraphNode>();
    }

    public void enlargeNode()
    {
        transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
    }

        public void enlargeMenuNode()
    {
        transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
    }

    public void normalsizeNode()
    {
        transform.localScale = new Vector3(2f, 2f, 2f);
    }

        public void normalMenuNode()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void brightNode()
    {   
        if(GraphNode.thisNode.Node.Label == ("Category"))
        {
            GetComponent<MeshRenderer>().material = PageSelectedMaterial;
        }
        else
        {
            GetComponent<MeshRenderer>().material = CatSelectedMaterial;
        }        
    }

    public void dimNode()
    {
        if(GraphNode.thisNode.Node.Label == ("Category"))
        {
            GetComponentInChildren<MeshRenderer>().material = PageMaterial;
        }   
        else
        {
            GetComponentInChildren<MeshRenderer>().material = CatMaterial;
        }
        
    }

    public void OnHover()
    {
        Hover.SetBool("IsHovering", true);
    }

    public void OffHover()
    {
        Hover.SetBool("IsHovering", false);
    }
}
