/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ForceDirectedGraph : MonoBehaviour
{
    public List<Node> nodes;

     void Start()
    {
        nodes = new List<Node>();
        for (int i = 0; i < 20; i++)
        {
            nodes.Add(new Node() { children = nodes.Where(node => Random.value > 0.5f).ToList(), position = Random.insideUnitSphere * 10, velocity = Vector3.zero });
        }
        
    }

    void Update()
    {
        foreach (var node in nodes)
        {
            node.position += node.velocity * Time.deltaTime;
        }
    }
    void onDrawGizmos()
    {
        foreach (var node in nodes)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(node.position, 0.125f);
            Gizmos.color = Color.green;
            foreach (var connectedNode in node.children)
            {
                Gizmos.DrawLine(node.position, connectedNode.position);
            }
        }
    }

}

public class Node
{
    public Vector3 position;
    public Vector3 velocity;
    public List<Node> children;

}
*/