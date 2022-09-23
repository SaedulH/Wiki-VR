using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Graph.DataStructure;
using Graph; 


public class NeoAuraQueries : IDisposable
{
    private bool _disposed = false;
    private readonly IDriver _driver;

    ~NeoAuraQueries() => Dispose(false);

    public NeoAuraQueries(string uri, string user, string password)
    {
        _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
    }

    public async void AuraQuery(string Cat, Graph.DataStructure.GraphNetwork graph, float Limiter)
    {
        // To learn more about the Cypher syntax, see https://neo4j.com/docs/cypher-manual/current/
        // The Reference Card is also a good resource for keywords https://neo4j.com/docs/cypher-refcard/current/
        var cypherQuery =         
        @"MATCH x= (p:Category {catName: '" + Cat + "'})<-[*..2]-(s) WITH *, relationships(x) as r RETURN p, r, s LIMIT " + Limiter;
        Debug.Log(cypherQuery);
        IAsyncSession session = _driver.AsyncSession(o => o.WithDatabase("neo4j"));
        //await using var session = _driver.AsyncSession(configBuilder => configBuilder.WithDatabase("neo4j"));
        try
        {
            // Write transactions allow the driver to handle retries and transient error
            // var writeResults = await session.WriteTransactionAsync(async tx =>
            // {
                //var result = await tx.RunAsync(cypherQuery, new { person1Name, person2Name });
                IResultCursor cursor = await session.RunAsync(cypherQuery);
                //var result = await .ToListAsync();
                var result = await cursor.ToListAsync();
                
                //return await cursor.ToListAsync();
            
            bool rootFetched = false;
            foreach (var record in result)
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
            await _driver.CloseAsync();  
        // Capture any errors along with the query and data for traceability
        // catch (Neo4jException ex)
        // {
        //     Debug.Log($"{cypherQuery} - {ex}");
        //     throw;
        // }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _driver?.Dispose();
        }

        _disposed = true;
    }

}