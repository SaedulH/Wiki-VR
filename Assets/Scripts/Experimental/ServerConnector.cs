/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Neo4j.Driver;

public class ServerConnector : MonoBehaviour
{
    //private bool _disposed = false;
    private readonly IDriver _driver;
    public static string uri = "bolt://localhost:7687";
    public static string user = "neo4j";
    public static string password = "test";

    public string Entity1_Str;
    public string Entity2_Str;
    
   //~ServerConnector() => Dispose(false);

    void Start()
    {
        PrintPeople();
    }
    public ServerConnector(string uri, string user, string password)
    {
        _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
    }
        //IAsyncSession session = driver.AsyncSession(o => o.WithDatabase("neo4j"));
    public async void PrintPeople()
    {

        List<string> result = null;
        var session = _driver.AsyncSession();
    
        try
        {
            result = await session.ReadTransactionAsync(async tx =>
            {
                var products = new List<string>();

                var reader = await tx.RunAsync
                ("MATCH (n:" + Entity1_Str + ") -[r]- (b:" + Entity2_Str + ") return r,n,b");

            // Loop through the records asynchronously
            while (await reader.FetchAsync())
            {
                // Each current read in buffer can be reached via Current
                products.Add(reader.Current[0].ToString());
            }

            return products;
        });           
        }
        finally
        {
            await session.CloseAsync();            
        }
        return;        

    }

    public void Dispose()
    {
        _driver.Dispose();
    }
}

*/