using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    private uint resourceCount;
    private uint resourceCountMax;
    //private bool isRenewable;

    // Start is called before the first frame update
    void Start()
    {
        resourceCount = 10;
        resourceCountMax = 10;
        //isRenewable = true;
     }

    public uint GetResourceCount()
    {
        return resourceCount;
    }

    public uint TakeResource()
    {
        uint value = 0;
        if (IsResourceAvailable())
        {
            resourceCount -= 1;
            value = 1;
            Debug.Log("ResourceNode: " + GetResourceCount().ToString());
        }
        return value;
    }

    internal bool IsResourceAvailable()
    {
        bool value = false;
        if (GetResourceCount() > 0)
        {
            value = true;
        }
        return value;
    }

    public void RenewResource()
    {
        if ( resourceCount < resourceCountMax)
        {
            resourceCount += 1;
            Debug.Log("ResourceNode: Renew");
        }
    }
}
