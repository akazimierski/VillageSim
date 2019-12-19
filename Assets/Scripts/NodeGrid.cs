using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid
{
    uint sizeX;
    uint sizeZ;
    float gridStep;
    Vector2 gridOrigin;
    private Node[,] nodes;
    private Vector2Int activeNodePos;

    public NodeGrid(uint sizeX, uint sizeZ)
    {
        this.sizeX = sizeX;
        this.sizeZ = sizeZ;
        nodes = new Node[sizeX, sizeZ];
        gridStep = 10.0f;
        gridOrigin = new Vector2(0.0f, 0.0f);
        activeNodePos = new Vector2Int();
        Vector2 gridLeftTopCornerPos = GridLeftTopCornerPosition(gridOrigin, sizeX, sizeZ);
        for (uint i = 0; i < sizeX; i++)
        {
            for (uint j = 0; j < sizeZ; j++)
            {
                nodes[i, j] = new Node(i, j, gridLeftTopCornerPos, gridStep);
            }
        }
    }

    public Vector3 GetActiveNodePosition()
    {
        return nodes[activeNodePos.x, activeNodePos.y].GetWorldPos();
    }

    private Vector2 GridLeftTopCornerPosition(Vector2 gridOrigin, uint sizeX, uint sizeZ)
    {
        float x = gridOrigin.x - (Mathf.Floor(sizeX/2) * gridStep);
        float y = gridOrigin.y + (Mathf.Floor(sizeZ/2) * gridStep);
        return new Vector2(x, y);
    }

    public Node[,] GetNodes()
    {
        return nodes;
    }

    public void ActivateClosestNodeToHitPoint(Vector3 hitPoint)
    {
        float closestDist = Mathf.Infinity;
        Vector2Int closestNodePos = new Vector2Int();
        foreach (var node in nodes)
        {
            var currentDist = Vector3.Distance(node.GetWorldPos(), hitPoint);
            if (currentDist < closestDist)
            {
                closestDist = currentDist;
                closestNodePos = node.GetNodeArrayPos();
            }
        }
        if (closestNodePos != activeNodePos)
        {
            nodes[activeNodePos.x, activeNodePos.y].SetActivate(false);
            nodes[closestNodePos.x, closestNodePos.y].SetActivate(true);
            activeNodePos = closestNodePos;
        }
        
    }
}
