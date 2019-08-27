using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPathfinding : MonoBehaviour
{
    public Transform seeker, target;
    NodeGridAI nGrid;

    private void Awake()
    {
        nGrid = GetComponent<NodeGridAI>();
    }

    private void Update()
    {
        FindPath(seeker.position, target.position);
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        NodeAI startNode = nGrid.NodeFromWorldPoint(startPos);
        NodeAI targetNode = nGrid.NodeFromWorldPoint(targetPos);

        Heap<NodeAI> openSet = new Heap<NodeAI>(nGrid.MaxSize);
        HashSet<NodeAI> closedSet = new HashSet<NodeAI>();
        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            NodeAI currentNode = openSet.RemoveFirst();

            closedSet.Add(currentNode);
            if(currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (NodeAI neighbour in nGrid.GetNeighbours(currentNode))
            {
                if (!neighbour.isWalkable || closedSet.Contains(neighbour))
                    continue;

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if(newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    void RetracePath(NodeAI startNode, NodeAI endNode)
    {
        List<NodeAI> path = new List<NodeAI>();
        NodeAI currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();

        nGrid.path = path;
    }

    int GetDistance(NodeAI nodeA, NodeAI nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
