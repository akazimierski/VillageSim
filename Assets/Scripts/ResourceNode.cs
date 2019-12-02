using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{

    private int amount;

    // Start is called before the first frame update
    void Start()
    {
        amount = 10;
    }

    public int GetResourceAmount()
    {
        return amount;
    }

    public int TakeResource()
    {
        int value = 0;
        if (GetResourceAmount() > 0)
        {
            amount -= 1;
            value = 1;
            Debug.Log("ResourceNode: " + GetResourceAmount().ToString());
        }
        return value;
    }

    internal bool isResourceAvailable()
    {
        bool value = false;
        if (GetResourceAmount() > 0)
        {
            value = true;
        }
        return value;
    }
}
