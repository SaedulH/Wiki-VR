using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EdgeInfoManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI EdgeStart;    

    [SerializeField]
    private TextMeshProUGUI EdgeEnd;

    [SerializeField]
    private TextMeshProUGUI EdgeType;

    [SerializeField]
    private UIcheckerSO UIcheckerSO;

    public Animator EdgeInfoUI;

    public void InitializeEdgeCanvas(Graph.GraphEdge graphEdge)
    {
        EdgeStart.text = graphEdge.FirstNode.Node.Title;
        EdgeEnd.text = graphEdge.SecondNode.Node.Title;

        if(graphEdge.edge.Type == "SUBCAT_OF")
        {
            EdgeType.text = "Subcategory of:";
        }
        else if(graphEdge.edge.Type == "IN_CATEGORY")
        {
            EdgeType.text = "In category:";
        }
    }

    public void StartNodePressed()
    {
        //teleport to node
    }

    public void EndNodePressed()
    {
        //teleport to node  
    }

    public void ExitPressed()
    {
        UIcheckerSO.showingUI = false;
        StartCoroutine(ExitEdgeInfo());
    }

    IEnumerator ExitEdgeInfo()
    {
        EdgeInfoUI.SetTrigger("EdgeInfoOff");

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}
