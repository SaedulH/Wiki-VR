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
    private StringSO SO;

    private readonly IDriver _driver;
    public bool rootFetched = false;
    public bool isRendered = false;

    
    // Aura queries use an encrypted connection using the "neo4j+s" protocol
    private string Aurauri = "neo4j+s://a71ad590.databases.neo4j.io";
    private string uri = "bolt://localhost:7687";
    private string user = "neo4j";
    private string Aurapassword = "y7oD564NL-mx9l_VSlDqYHFJXE0XdGg-ZSNaV9m-7x4";
    private string password = "wiki"; 
    void Awake()
    {   

        StartCoroutine(GraphDatafirst());
        
        //StartCoroutine(SearchPage());
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
        Query(SO.Cat, network, SO.Limiter, uri, user, password);
        //SampleData.MakeSampleGraphData(network);
        
        //waits for 1 second.
        yield return new WaitForSeconds(1);
        graphRenderer.Initialize(network, SO.initialNum);

        graphLayout.InitializeForces();
        Debug.Log("starting!");
        isRendered = true;
        
        
    }

    public static async void Query(string Cat, Graph.DataStructure.GraphNetwork graph, float Limiter, string uri, string user, string password) 
    {
        IDriver driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));;
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

    public static async void searchCatQuery(string SearchValue, Graph.SearchResults results, string uri, string user, string password)
    {
        IDriver driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));;
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


            // foreach(var item in results.results1)
            // {
            //     Debug.Log(item);
            // }

        }

        finally
        {
            await session.CloseAsync();
        } 
            await driver.CloseAsync(); 
    } 

    public static async void searchPageQuery(string SearchValue, Graph.SearchResults results, string uri, string user, string password)
    {
        IDriver driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));;
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


            // foreach(var item in results.results1)
            // {
            //     Debug.Log(item);
            // }

        }

        finally
        {
            await session.CloseAsync();
        } 
            await driver.CloseAsync(); 
    }


}
 