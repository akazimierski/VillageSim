using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{

    private uint amount;
    private bool isRenewable;
    private uint resourceAmountMax;

    // Start is called before the first frame update
    void Start()
    {
        amount = 10;
        resourceAmountMax = 10;
        isRenewable = true;
        if (isRenewable)
        {
            InvokeRepeating("RenewResource", 15.0f, 15.0f);
        }
     }

    public uint GetResourceAmount()
    {
        return amount;
    }

    public uint TakeResource()
    {
        uint value = 0;
        if (GetResourceAmount() > 0)
        {
            amount -= 1;
            value = 1;
            //Debug.Log("ResourceNode: " + GetResourceAmount().ToString());
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

    private void RenewResource()
    {
        if ( amount < resourceAmountMax)
        {
            amount += 1;
            //Debug.Log("ResourceNode: Renew");
        }
    }
}
