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

    public bool rootFetched = false;
    public bool isRendered = false;
    
    void Awake()
    {   

        StartCoroutine(GraphDatafirst());
        
        //StartCoroutine(SearchPage());
    }

    void Update()
    {
        if(isRendered)
        {
            //Debug.Log("Doing iterations");
            graphLayout.DoIterations();
        //graphLayout.DoIterations(1);
            //graphLayout.ApplyForce();
        }
        // if(graphReady)
        // {
        //     showScreen();
        // }
    }

    IEnumerator GraphDatafirst()
    {

        Graph.DataStructure.GraphNetwork network = new Graph.DataStructure.GraphNetwork();

        Query(SO.Cat, network, SO.Limiter);
        //Query("Databases", network);
        //SampleData.MakeSampleGraphData(network);
        
        //waits for 1 second.
        yield return new WaitForSeconds(1);
        graphRenderer.Initialize(network, SO.Num);

        graphLayout.InitializeForces();
        isRendered = true;
        
        //StartCoroutine(WaitForRender());
    }

    IEnumerator WaitForRender()
    {
        yield return new WaitForSeconds(0.5f);        
        graphLayout.DoIterations();
    }
    

    public static async void Query(string Cat, Graph.DataStructure.GraphNetwork graph, float Limiter) 
    {
        IDriver driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "test"));;
        IAsyncSession session = driver.AsyncSession(o => o.WithDatabase("neo4j"));

        var cypherQuery =
        @"MATCH x= (p:Category {catName: '" + Cat + "'})<-[*..2]-(s) WITH *, relationships(x) as r RETURN p, r, s LIMIT " + Limiter;

        try 
        {
            IResultCursor cursor = await session.RunAsync(cypherQuery);
            var result = await cursor.ToListAsync();

            //List<Edges> edges2 = new List<Edges>();
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
                        Debug.Log("Did not add: "+ nodey.Title);
                    }
                    else
                    {   
                        Debug.Log("Added: " + nodey.Title);
                        graph.nodes1.Add(nodey);
                    }
                        
                } 
                else if (anode.Labels[0] == "Page")
                {

                    Graph.DataStructure.Nodes nodey = new Nodes(anode.Id, anode.Labels[0], anode.Properties["pageTitle"].ToString());           
                    if(graph.nodes1.Any(Nodes => Nodes.Title == nodey.Title))
                    {
                        //do nothing
                        Debug.Log("Did not add: "+ nodey.Title);
                    }
                    else
                    {
                        Debug.Log("Added: " + nodey.Title); 
                        graph.nodes1.Add(nodey);
                    }
                }

            }

            // foreach(Edges edge in graph.edges1)
            // {
            //     Debug.Log(edge.StartNodeID + " -> " + edge.EndNodeID);
            // }
            //Debug.Log("Neonodes = " + graph.nodes1.Count);

        }
   
        finally
        {
            await session.CloseAsync();
        } 
            await driver.CloseAsync();     


    }

    public static async void searchCatQuery(string SearchValue, Graph.SearchResults results)
    {
        IDriver driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "test"));;
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

    public static async void searchPageQuery(string SearchValue, Graph.SearchResults results)
    {
        IDriver driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "test"));;
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
 