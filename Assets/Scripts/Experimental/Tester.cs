/*using Neo4j.Driver;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains three examples of using this plugin that should get you going.
/// This example is based on the "Movies" example dataset by Neo4j (https://neo4j.com/developer/example-data/).
/// Comments are based on the Neo4j .NET Driver v4.1 documentation (https://github.com/neo4j/neo4j-dotnet-driver).
/// </summary>
public class Tester : MonoBehaviour
{
    void Start()
    {
        //SingleReturnTest();
        SingleNodeLabelsTest();
        //MultipleReturnsTest();
        //MultiNodeTest();
    }


    // ############
    // RETURN SINGLE LIST<STRING>
    // ############

    public async void SingleReturnTest()
    {
        // Each IDriver instance maintains a pool of connections inside, as a result, it is recommended to only use one driver per application.        
        // The driver is thread-safe, while the session or the transaction is not thread-safe.
        IDriver driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "test"));
        IAsyncSession session = driver.AsyncSession(o => o.WithDatabase("neo4j"));
        try
        {
            IResultCursor cursor = await session.RunAsync("MATCH (a:Person) RETURN a.name as name");
            // The recommended way to access these result records is to make use of methods provided by ResultCursorExtensions such as SingleAsync, 
            // ToListAsync, and ForEachAsync.
            List<string> people = await cursor.ToListAsync(record => record["name"].As<string>());
            await cursor.ConsumeAsync();

            Debug.Log(people.Count + " single returns");
        }
        finally
        {
            await session.CloseAsync();
        }
        await driver.CloseAsync();
    }


    // ############
    // RETURN SINGLE NODE
    // ############

    public async void SingleNodeLabelsTest()
    {
        // Each IDriver instance maintains a pool of connections inside, as a result, it is recommended to only use one driver per application.        
        // The driver is thread-safe, while the session or the transaction is not thread-safe.
        IDriver driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "test"));
        IAsyncSession session = driver.AsyncSession(o => o.WithDatabase("neo4j"));
        try
        {
            IResultCursor cursor = await session.RunAsync("MATCH (a: Person)-[r]->(b: Movie)  RETURN a,r,b");
            // The recommended way to access these result records is to make use of methods provided by ResultCursorExtensions such as SingleAsync, 
            // ToListAsync, and ForEachAsync.
            
            //List<IEntity> nodes1 = await cursor.ToListAsync(record => record["a"].As<IEntity>());
            //await cursor.ConsumeAsync();   
            //foreach (var item in nodes1)
            //{
                //Debug.Log("ID:" + item.Id);
            //}

            List<IRelationship> edges = await cursor.ToListAsync(record => record["r"].As<IRelationship>());                    

            foreach (var thing in edges)
            {
                Debug.Log("From: " + thing.StartNodeId + ", To:" + thing.EndNodeId);
            }
        }
        finally
        {
            await session.CloseAsync();
        }
        await driver.CloseAsync();
    }


    // ############
    // RETURN TWO LIST<STRING>
    // ############

    public async void MultipleReturnsTest()
    {
        // Each IDriver instance maintains a pool of connections inside, as a result, it is recommended to only use one driver per application.        
        // The driver is thread-safe, while the session or the transaction is not thread-safe.
        IDriver driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "test"));
        IAsyncSession session = driver.AsyncSession(o => o.WithDatabase("neo4j"));
        try
        {
            IResultCursor cursor = await session.RunAsync("MATCH (a:Person) RETURN a.name, a.born");
            // A record is accessible once it is received by the client. It is not needed for the whole result set to be received before it can be visited.
            // Each record can only be visited (a.k.a.consumed) once!
            // The recommended way to access these result records is to make use of methods provided by ResultCursorExtensions such as SingleAsync, 
            // ToListAsync, and ForEachAsync.
            List< DataHolder> people = await cursor.ToListAsync(record => new DataHolder(record["a.name"].As<string>(), record["a.born"].As<string>()));
            await cursor.ConsumeAsync();

            foreach (var item in people)
            {
                Debug.Log(item.ToString());
            }
        }
        finally
        {
            await session.CloseAsync();
        }
        await driver.CloseAsync();
    }

    public async void MultiNodeTest()
    {
        // Each IDriver instance maintains a pool of connections inside, as a result, it is recommended to only use one driver per application.        
        // The driver is thread-safe, while the session or the transaction is not thread-safe.
        List<string> result = null;

        IDriver driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "test"));
        var session = driver.AsyncSession(o => o.WithDatabase("neo4j"));
        try
        {               
            result = await session.ReadTransactionAsync(async tx =>
            {
                var products = new List<string>();

                var reader = await tx.RunAsync("MATCH (n:Movie) -[r]->(m:Person) return m");

            // Loop through the records asynchronously
            while (await reader.FetchAsync())
            {
                products.Add(reader.Current[0].ToString());
                Debug.Log(products[0]);
            }       
            return products;         
            });
        }
        finally
        {
            await session.CloseAsync();
        }
        await driver.CloseAsync();
        }
    

   /* 
    MATCH (n:"+ Entity1_Str + ") -[r]- (b:" + Entity2_Str + ") return r,n,b

    using(var session = Driver.Session())
    {
    var statement = $"MATCH (n:{label} {{id:{id}}})-[r]-(m) RETURN r,m";
    var result = session.Run(statement);
    foreach(var record in result)
    {
        var node = record["m"].As<INode>();
        var relation = record["r"].As<IRelationship>();
        // TODO: Process result
    }
}




    // Helper Class
    public class DataHolder
    {
        public string A;
        public string B;

        public DataHolder (string a, string b)
        {
            A = a;
            B = b;
        }

        public override string ToString ()
        {
            return A + " | " + B;
        }
    }

    public class NodeHolder
        {
        INode a;
        INode b;
        IRelationship c;
           
        }

    public class EdgeHolder
        {
        public List<IRelationship> relationships;
        }
}

*/