using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Neo4j.Driver;
using Graph.DataStructure;
using Graph; 
using System.Linq;

public class NeoQuery : MonoBehaviour
{

    /// <summary>
    /// The graph displaying the network.
    /// </summary>
    [SerializeField]
    [Tooltip("The graph displaying the network.")]
    private GraphRenderer graphRenderer;

    [SerializeField]
    private ForceDirectedLayout graphLayout;


    [SerializeField]
    private StringSO catSelector;

    public bool rootFetched = false;
    
    void Awake()
    {   

        StartCoroutine(GraphDatafirst());

    }

    IEnumerator GraphDatafirst()
    {

        Graph.DataStructure.GraphNetwork network = new Graph.DataStructure.GraphNetwork();


        //Query(catSelector.Cat, network);
        //Query("Databases", network);
        SampleData.MakeSampleGraphData(network);
        
        //waits for 1 second.
        yield return new WaitForSeconds(1);
        graphRenderer.Initialize(network);

        graphLayout.InitializeForces();
        StartCoroutine(ForceEnum());
    }

    IEnumerator ForceEnum()
    {
        yield return new WaitForSeconds(0.5f);        
        graphLayout.DoIterations();
    }

    public static async void Query(string Cat, Graph.DataStructure.GraphNetwork graph) 
    {
        IDriver driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "test"));;
        IAsyncSession session = driver.AsyncSession(o => o.WithDatabase("neo4j"));

        var cypherQuery =
        @"MATCH x= (p:Category {catName: '" + Cat + "'})<-[*..2]-(s) WITH *, relationships(x) as r RETURN p, r, s LIMIT 400";

        try 
        {
            IResultCursor cursor = await session.RunAsync(cypherQuery);
            var result = await cursor.ToListAsync();

            
            

            List<Edges> edges2 = new List<Edges>();
            bool rootFetched = false;

        foreach(var record in result)
        {
            

            var rootnode = record["p"].As<INode>();
            if(!rootFetched)
            {
                Graph.DataStructure.Nodes node = new Nodes(rootnode.Id, rootnode.Labels[0], rootnode.Properties["catName"].ToString());
                graph.nodes1.Add(node);
                rootFetched = true;
            }

            var anedge = record["r"].As<List<IRelationship>>();
            for(int i = 0; i<anedge.Count ;)
            {   
                if(anedge.Count == 1)
                {
                long StartNodeID = anedge[0].StartNodeId;
                long EndNodeID = anedge[0].EndNodeId;
                string Type = anedge[0].Type;
                
                Graph.DataStructure.Edges edgy0 = new Edges(Type, StartNodeID, EndNodeID); 
                graph.edges1.Add(edgy0);

                break;
                }
                else
                {
            
                long StartNodeID1 = anedge[anedge.Count-1].StartNodeId;
                long EndNodeID1 = anedge[anedge.Count-1].EndNodeId;
                string Type1 = anedge[anedge.Count-1].Type;

                Graph.DataStructure.Edges edgy1 = new Edges(Type1, StartNodeID1, EndNodeID1); 
                graph.edges1.Add(edgy1); 

                break;  
                }
            }

            var anode = record["s"].As<INode>();

            if (anode.Labels[0] == "Category")
            {

                Graph.DataStructure.Nodes nodey = new Nodes(anode.Id, anode.Labels[0], anode.Properties["catName"].ToString());
                graph.nodes1.Add(nodey);
            } 
            else if (anode.Labels[0] == "Page")
            {

                Graph.DataStructure.Nodes nodey = new Nodes(anode.Id, anode.Labels[0], anode.Properties["pageTitle"].ToString());           
                graph.nodes1.Add(nodey);
            }

        }

        foreach(Edges edge in graph.edges1)
        {
            Debug.Log(edge.StartNodeID + " -> " + edge.EndNodeID);
        }

      }
   
      finally
      {
      await session.CloseAsync();
      } 
      await driver.CloseAsync();     


    } 

}

/*
      MATCH (p:Person)-[r]->(m:Movie) RETURN p, r, m
      MATCH p= (m: Category {catName: 'Databases in the United Kingdom'})<-[*..3]-(n) RETURN m, n limit 250
      MATCH (m: Category {catName: 'Databases'})<-[]-(s)<-[]-(n)  RETURN  m, s, n limit 250"            
   */   