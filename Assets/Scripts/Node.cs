using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    uint arrayPosX;
    uint arrayPosY;
    Vector3 worldPos;
    bool active;

    public Node(uint posX, uint posY, Vector2 gridLeftTopCornerPos, float gridStep)
    {
        arrayPosX = posX;
        arrayPosY = posY;
        active = false;

        float x = gridLeftTopCornerPos.x + (arrayPosX * gridStep);
        float y = gridLeftTopCornerPos.y - (arrayPosY * gridStep);
        worldPos = new Vector3(x, 0.0f, y);
        //Debug.Log(worldPos);
    }

    public Vector3 GetWorldPos() => worldPos;

    public bool GetActivation() => active;

    public Vector2Int GetNodeArrayPos()
    {
        return new Vector2Int((int)arrayPosX, (int)arrayPosY);
    }

    public void SetActivate(bool status)
    {
        if (this.active != status)
        {
            this.active = status;
            //var tmp = (status) ? "Active" : "Deactive";
            //Debug.Log(tmp);
        }
    }
}
