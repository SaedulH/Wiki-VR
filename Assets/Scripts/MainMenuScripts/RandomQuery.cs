using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neo4j.Driver;
using TMPro;

public class RandomQuery : MonoBehaviour
{

    [SerializeField]
    private StringSO SO;

    [SerializeField]
    private GameObject RandomConfirm;

    [SerializeField]
    private GameObject RandomOption;
    
    [SerializeField]
    private TextMeshProUGUI Loadfor; 

    [SerializeField]
    private TextMeshProUGUI Title; 

    private MenuManager menu;
    // Start is called before the first frame update

    private bool randomReady = false;
    public void RandomConfirmtoOptions()
    {
        RandomConfirm.SetActive(false);
        RandomOption.SetActive(true);
       
    } 

    public async void RandomNeoQuery(string RandomType)
    {
        IDriver driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "wiki"));;
        IAsyncSession session = driver.AsyncSession(o => o.WithDatabase("neo4j"));

        var catQuery = 
        @"MATCH (a:"+RandomType+") RETURN a ORDER BY rand() Limit 1";
        
        try 
        {
            IResultCursor cursor = await session.RunAsync(catQuery);
            var result = await cursor.SingleAsync();    

            var record = result["a"].As<INode>();     

            
            if(RandomType == "Category")
            {
                string Title = record.Properties["catName"].ToString();
                SO.Cat = Title; 
            }
            else if(RandomType == "Page")
            {
                string Title = record.Properties["pageTitle"].ToString();
                SO.PageName = Title; 
            } 

        }

        finally
        {
            await session.CloseAsync();
        } 
            await driver.CloseAsync();
    }

    public void InitialRandomQuery()
    {
        RandomNeoQuery("Category");
        RandomNeoQuery("Page");
    }
    public void DoRandomCatQuery()
    {
        RandomNeoQuery("Category");

        RandomOption.SetActive(false);
        RandomConfirm.SetActive(true);

        Loadfor.text = "Load Graph for";
        Title.text = SO.Cat; 


    }    

    public void DoRandomPageQuery()
    {
        //WebData.WebCall webCall = new WebData.WebCall();
        //startl.getrandomURl();
        RandomNeoQuery("Page");

        RandomOption.SetActive(false);
        RandomConfirm.SetActive(true);

        Loadfor.text = "Load page for";
        Title.text = SO.PageName; 
    } 

    public void RandomConfirmed()
    {
        if(Loadfor.text == "Load Graph for")
        {
            MenuManager.current.StartTransitionCat();
        }
        else
        {
            MenuManager.current.StartTransitionPage();
        }
    }
}
