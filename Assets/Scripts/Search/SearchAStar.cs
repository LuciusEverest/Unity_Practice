using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Priority_Queue;

public static class SearchAStar
{
    public static bool Search(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
        
        bool found = false;

        SimplePriorityQueue<GraphNode> nodes = new SimplePriorityQueue<GraphNode>();

        source.Cost = 0;
        // <set source heuristic to the distance of the source to the destination>
        source.Heuristic = Vector3.Distance(source.transform.position, destination.transform.position);
        nodes.Enqueue(source, source.Cost + source.Heuristic);

        // set the current number of steps
        int steps = 0;
        while (!found && nodes.Count > 0 && steps++ < maxSteps)
        {
            // <dequeue node>
            GraphNode node = nodes.Dequeue();
            if (node == destination)
            {
                // <set found to true>
                found = true;
                // <continue, do not execute the rest of this loop>
                continue;
            }
            foreach (GraphNode.Edge edge in node.Edges)
            {
                // calculate cost to nodeB = node cost + edge distance (nodeA to nodeB)
                float cost = node.Cost + Vector3.Distance(edge.nodeA.transform.position, edge.nodeB.transform.position);
                // if cost < nodeB cost, add to priority queue
                if (cost < edge.nodeB.Cost)
                {
                    // <set nodeB cost to cost>
                    edge.nodeB.Cost = cost;
                    // <set nodeB parent to node>
                    edge.nodeB.Parent = node;
                    // calculate heuristic = Vector3.Distance (nodeB <-> destination)
                    edge.nodeB.Heuristic = Vector3.Distance(edge.nodeB.transform.position, destination.transform.position);
                    // <enqueue without duplicates nodeB with nodeB cost + nodeB heuristics as priority>
                    nodes.Enqueue(edge.nodeB, edge.nodeB.Cost + edge.nodeB.Heuristic);
                }
            }
        }

        // create a list of graph nodes (path)
        path = new List<GraphNode>();
        // if found is true
        if (found)
        {
            GraphNode node = destination;
            // while node not null
            while (node != null)
            {
                path.Add(node);
                node = node.Parent;
            }
            path.Reverse();
        }
        else
        {
            path = nodes.ToList();
        }
        return found;
    }
}
                
