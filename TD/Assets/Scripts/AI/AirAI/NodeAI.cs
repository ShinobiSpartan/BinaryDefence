﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeAI : IHeapItem<NodeAI>
{
    public bool isWalkable;
    public Vector3 worldPosition;

    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;

    public int FCost { get { return gCost + hCost; } }

    public NodeAI parent;

    int heapIndex;

    public NodeAI(bool walkable, Vector3 worldPos, int _gridX, int _gridY)
    {
        isWalkable = walkable;
        worldPosition = worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int HeapIndex { get { return heapIndex; } set { heapIndex = value; } }

    public int CompareTo(NodeAI nodeToCompare)
    {
        int compare = FCost.CompareTo(nodeToCompare.FCost);
        if (compare == 0)
            compare = hCost.CompareTo(nodeToCompare.hCost);
        return -compare;
    }
}