﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGridAI : MonoBehaviour
{
    public Vector2 gridWorldSize;
    public float nodeRadius;
    NodeAI[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    public bool displayGrid;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    public int MaxSize { get { return gridSizeX * gridSizeY; } }

    void CreateGrid()
    {
        grid = new NodeAI[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for(int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius));
                grid[x, y] = new NodeAI(walkable, worldPoint, x, y);
            }
        }
    }

    public NodeAI NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        percentX = Mathf.Clamp01(percentX);

        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x,y];
    }

    public List<NodeAI> GetNeighbours(NodeAI node)
    {
        List<NodeAI> neighbours = new List<NodeAI>();

        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <=1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    neighbours.Add(grid[checkX, checkY]);
            }
        }

        return neighbours;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 3, gridWorldSize.y));

        if(grid != null && displayGrid)
        {
            foreach (NodeAI n in grid)
            {
                Gizmos.color = (n.isWalkable) ? Color.white : Color.red;
                Gizmos.DrawWireCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    } 
}
