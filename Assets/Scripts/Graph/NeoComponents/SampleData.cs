using Graph.DataStructure;

public class SampleData
{
    //Initial Graph nodes and edges used for testing network functionality without the need for Neo4j
    public static void MakeSampleGraphData(Graph.DataStructure.GraphNetwork graph1)
    {
        //Test graph using manually created nodes and edges//
        Nodes node1 = new Nodes(1, "Category", "Item 1");
        Nodes node2 = new Nodes(2, "Page", "Item 2");
        Nodes node3 = new Nodes(3, "Category", "Item 3");
        Nodes node4 = new Nodes(4, "Category", "Item 4");
        Nodes node5 = new Nodes(5, "Category", "Item 5");
        Nodes node6 = new Nodes(6, "Page", "Item 6");
        Nodes node7 = new Nodes(7, "Page", "Item 7");
        Nodes node8 = new Nodes(8, "Category", "Item 8");
        Nodes node9 = new Nodes(9, "Page", "Item 9");
        Nodes node10 = new Nodes(10, "Page", "Item 10");
        Nodes node11 = new Nodes(11, "Category", "Item 11");
        Nodes node12 = new Nodes(12, "Page", "Item 12");
        Nodes node13 = new Nodes(13, "Page", "Item 13");
        Nodes node14 = new Nodes(14, "Page", "Item 14");
        Nodes node15 = new Nodes(15, "Page", "Item 15");
        Nodes node16 = new Nodes(16, "Page", "Item 16");

        graph1.nodes1.Add(node1);
        graph1.nodes1.Add(node2);
        graph1.nodes1.Add(node3);
        graph1.nodes1.Add(node4);
        graph1.nodes1.Add(node5);
        graph1.nodes1.Add(node6);
        graph1.nodes1.Add(node7);
        graph1.nodes1.Add(node8);
        graph1.nodes1.Add(node9);
        graph1.nodes1.Add(node10);
        graph1.nodes1.Add(node11);
        graph1.nodes1.Add(node12);
        graph1.nodes1.Add(node13);
        graph1.nodes1.Add(node14);
        graph1.nodes1.Add(node15);
        graph1.nodes1.Add(node16);

        graph1.edges1.Add(new Edges("rel", 1, 2)); // Item 1 -> Item 2
        graph1.edges1.Add(new Edges("rel", 1, 3)); // Item 1 -> Item 3
        graph1.edges1.Add(new Edges("rel", 1, 4)); // Item 1 -> Item 4
        graph1.edges1.Add(new Edges("rel", 2, 4)); // Item 2 -> Item 4
        graph1.edges1.Add(new Edges("rel", 4, 5)); // Item 4 -> Item 5
        graph1.edges1.Add(new Edges("rel", 5, 6)); // Item 5 -> Item 6
        graph1.edges1.Add(new Edges("rel", 5, 7)); // Item 5 -> Item 7
        graph1.edges1.Add(new Edges("rel", 2, 7)); // Item 2 -> Item 7
        graph1.edges1.Add(new Edges("rel", 6, 7)); // Item 6 -> Item 7
        graph1.edges1.Add(new Edges("rel", 3, 6)); // Item 3 -> Item 6
        graph1.edges1.Add(new Edges("rel", 6, 8)); // Item 6 -> Item 8
        graph1.edges1.Add(new Edges("rel", 8, 9)); // Item 8 -> Item 9
        graph1.edges1.Add(new Edges("rel", 8, 10)); // Item 8 -> Item 10
        graph1.edges1.Add(new Edges("rel", 8, 11)); // Item 8 -> Item 11
        graph1.edges1.Add(new Edges("rel", 10, 9)); // Item 10 -> Item 9
        graph1.edges1.Add(new Edges("rel", 11, 12)); // Item 11 -> Item 12
        graph1.edges1.Add(new Edges("rel", 11, 13)); // Item 11 -> Item 13
        graph1.edges1.Add(new Edges("rel", 12, 13)); // Item 12 -> Item 13
        graph1.edges1.Add(new Edges("rel", 11, 14)); // Item 11 -> Item 14
        graph1.edges1.Add(new Edges("rel", 11, 15)); // Item 11 -> Item 15
        graph1.edges1.Add(new Edges("rel", 11, 16)); // Item 12 -> Item 16
    }

}
