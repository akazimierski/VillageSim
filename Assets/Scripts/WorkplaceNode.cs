using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkplaceNode : MonoBehaviour
{
    private uint resourceAmount;
    private uint resourceAmountMax;

    // Start is called before the first frame update
    void Start()
    {
        resourceAmount = 0;
        resourceAmountMax = 3;
        InvokeRepeating("Production", 15.0f, 15.0f);
    }

    public uint LeaveResource()
    {
        uint value = 0;
        if (GetResourceAmount() < GetResourceAmountMax())
        {
            resourceAmount += 1;
            value = 1;
            //Debug.Log("WorkplaceNode: " + GetResourceAmount().ToString());
        }
        return value;
    }

    public uint GetResourceAmount()
    {
        return resourceAmount;
    }

    public uint GetResourceAmountMax()
    {
        return resourceAmountMax;
    }

    public bool IsStorageFree()
    {
        bool value = false;
        if (GetResourceAmount() < GetResourceAmountMax())
        {
            value = true;
        }
        return value;
    }

    void Production()
    {
        if (GetResourceAmount() > 0)
        {
            resourceAmount -= 1;
            //Debug.Log("Workplace: Production");
        }
    }
}
