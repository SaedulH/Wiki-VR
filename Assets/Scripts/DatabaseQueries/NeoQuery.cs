using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Neo4j.Driver;
using Graph.DataStructure;
using Graph; 
using System.Linq;

public class NeoQuery : MonoBehaviour
{

    // The graph data for displaying the network.
    [SerializeField]
    private GraphRenderer graphRenderer;

    // The Algorithm for the graph layout.
    [SerializeField]
    private ForceDirectedLayout graphLayout;

    [SerializeField]
    private StringSO SO;

    private readonly IDriver _driver;
    public bool rootFetched = false;
    public bool isRendered = false;

    private string Aurauri = "neo4j+s://a71ad590.databases.neo4j.io";
    private string Aurapassword = "y7oD564NL-mx9l_VSlDqYHFJXE0XdGg-ZSNaV9m-7x4";
    
    void Awake()
    {   
        StartCoroutine(GraphDatafirst());
    }

    void Update()
    {
        if(isRendered)
        {
            graphLayout.DoIterations();
        }
    }

    IEnumerator GraphDatafirst()
    {

        Graph.DataStructure.GraphNetwork network = new Graph.DataStructure.GraphNetwork();
        //NeoAuraQueries queries = new NeoAuraQueries(Aurauri, user, Aurapassword);

        //queries.AuraQuery(SO.Cat, network, SO.Limiter);
        //SampleData.MakeSampleGraphData(network);
        Query(SO.Cat, network, SO.Limiter);
                
        //waits for data to be retrieved and processed.
        yield return new WaitForSeconds(1);
        graphRenderer.Initialize(network, SO.initialNum);

        graphLayout.InitializeForces();
        Debug.Log("starting!");
        isRendered = true;
        
        
    }

    // Main Cypher query to get graph data
    public static async void Query(string Cat, Graph.DataStructure.GraphNetwork graph, float Limiter) 
    {
        IDriver driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "wiki"));;
        IAsyncSession session = driver.AsyncSession(o => o.WithDatabase("neo4j"));
        var cypherQuery =
        @"MATCH x= (p:Category {catName: '" + Cat + "'})<-[*..2]-(s) WITH *, relationships(x) as r RETURN p, r, s LIMIT " + Limiter;
        Debug.Log(cypherQuery);
        try 
        {
            IResultCursor cursor = await session.RunAsync(cypherQuery);
            var result = await cursor.ToListAsync();
            bool rootFetched = false;

            foreach(var record in result)
            {    
                //Add the root category to Nodes list            
                var rootnode = record["p"].As<INode>();
                if(!rootFetched)
                {
                    Graph.DataStructure.Nodes node = new Nodes(rootnode.Id, rootnode.Labels[0], rootnode.Properties["catName"].ToString());
                    graph.nodes1.Add(node);
                    rootFetched = true;
                }

                //Add every relationship in query result to Edges list
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
                //Add every node in query result to Nodes list
                var anode = record["s"].As<INode>();     
                if (anode.Labels[0] == "Category")
                {
                    Graph.DataStructure.Nodes nodey = new Nodes(anode.Id, anode.Labels[0], anode.Properties["catName"].ToString());
                    if(graph.nodes1.Any(Nodes => Nodes.Title == nodey.Title))
                    {
                        //do nothing
                    }
                    else
                    {   
                        graph.nodes1.Add(nodey);
                    }                    
                } 
                else if (anode.Labels[0] == "Page")
                {
                    Graph.DataStructure.Nodes nodey = new Nodes(anode.Id, anode.Labels[0], anode.Properties["pageTitle"].ToString());           
                    if(graph.nodes1.Any(Nodes => Nodes.Title == nodey.Title))
                    {
                        //do nothing
                    }
                    else
                    {
                        graph.nodes1.Add(nodey);
                    }
                }
            }
        }
        finally
        {
            await session.CloseAsync();
        } 
            await driver.CloseAsync();     
    }

    //Cypher query to get specified Category nodes that start with the input string
    public static async void searchCatQuery(string SearchValue, Graph.SearchResults results)
    {
        IDriver driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "wiki"));;
        IAsyncSession session = driver.AsyncSession(o => o.WithDatabase("neo4j"));

        var catQuery = 
        @"MATCH (c:Category) WHERE c.catName STARTS WITH '"+SearchValue+"' Return c.catName AS s LIMIT 20";
        
        try 
        {
            IResultCursor cursor = await session.RunAsync(catQuery);
            var result = await cursor.ToListAsync();    

            foreach(var record in result)
            {
                var search = record["s"].As<string>();
                
                results.results1.Add(search);
            }
        }

        finally
        {
            await session.CloseAsync();
        } 
            await driver.CloseAsync(); 
    } 
    //Cypher query to get specified Page nodes that start with the input string
    public static async void searchPageQuery(string SearchValue, Graph.SearchResults results)
    {
        IDriver driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "wiki"));;
        IAsyncSession session = driver.AsyncSession(o => o.WithDatabase("neo4j"));

        var catQuery = 
        @"MATCH (p:Page) WHERE p.pageTitle STARTS WITH '"+SearchValue+"' Return p.pageTitle AS s LIMIT 20";
        
        try 
        {
            IResultCursor cursor = await session.RunAsync(catQuery);
            var result = await cursor.ToListAsync();    

            foreach(var record in result)
            {
                var search = record["s"].As<string>();
                
                results.results1.Add(search);
            }
        }

        finally
        {
            await session.CloseAsync();
        } 
            await driver.CloseAsync(); 
    }


}
 