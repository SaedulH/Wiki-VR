using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Graph;
using TMPro;

public class EdgeInteractions : MonoBehaviour
{   
    private GameObject player;

    [SerializeField]
    private GraphEdge graphEdge;

    [SerializeField]
    private GameObject EdgeInfo;

    [SerializeField]
    private UIcheckerSO UIcheckerSO;

    void Start()
    {
        graphEdge = GetComponent<GraphEdge>();
    }

    public void EdgePressed()
    {   
        UIcheckerSO.showingUI = true;
        GameObject EdgeInfoCanvas = Instantiate(EdgeInfo, new Vector3(0, 0, 0), Quaternion.identity);

        EdgeInfoCanvas.transform.position = Camera.main.transform.position + Camera.main.transform.forward*2f;
        EdgeInfoCanvas.transform.rotation = Camera.main.transform.rotation;
        Debug.Log("showing Edge info");

        EdgeInfoManager Edgescript = EdgeInfoCanvas.GetComponent<EdgeInfoManager>();

        Edgescript.InitializeEdgeCanvas(graphEdge);

    }
    public void EdgeSelected()
    {   
        if(graphEdge.edge.Type == "SUBCAT_OF")
        {
            gameObject.GetComponent<Renderer>().material.SetColor ("_Color", new Color(1,0,0,1));
            gameObject.GetComponent<Renderer>().material.SetColor ("_EmissionColor", new Color(1,0,0,1) * 2F);
        }
        else if(graphEdge.edge.Type == "IN_CATEGORY")
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.78F,1,0,1));
            gameObject.GetComponent<Renderer>().material.SetColor ("_EmissionColor", new Color(0.78F,1,0,1) * 2F);
        }      
    }

    public void EdgeDeselected()
    {   
        if(UIcheckerSO.showingUI == false)
        {
            if(graphEdge.edge.Type == "SUBCAT_OF")
            {
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.47F,0,0,1));
                gameObject.GetComponent<Renderer>().material.SetColor ("_EmissionColor", new Color(0,0,0,1));
            }
            else if(graphEdge.edge.Type == "IN_CATEGORY")
            {
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.47F,0.47F,0,1));
                gameObject.GetComponent<Renderer>().material.SetColor ("_EmissionColor", new Color(0,0,0,1));
            }
        }
        else
        {
            //wait until canvas is gone
        }
         
    }

}
