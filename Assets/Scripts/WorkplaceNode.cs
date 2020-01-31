using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkplaceNode : MonoBehaviour
{
    //move to derived
    private uint resourceAmount;
    private uint resourceAmountMax;

    private uint employeesCount;
    private uint employeesMax;

    private bool freeJob;

    private List<GameObject> employeeList;

    // Start is called before the first frame update
    void Start()
    {
        resourceAmount = 0;
        resourceAmountMax = 3;
        employeesCount = 0;
        employeesMax = 1;
        freeJob = true;
        employeeList = new List<GameObject>();
    }

    public uint LeaveResource()
    {
        uint value = 0;
        if (GetResourceAmount() < GetResourceAmountMax())
        {
            resourceAmount += 1;
            value = 1;
            Debug.Log("WorkplaceNode: " + GetResourceAmount().ToString());
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

    public bool IsFreeJob()
    {
        return freeJob;
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

    public void Production()
    {
        if (GetResourceAmount() > 0)
        {
            resourceAmount -= 1;
            Debug.Log("Workplace: Production");
        }
    }

    public void EmployeeJoined(GameObject unit)
    {
        if (freeJob)
        {
            employeesCount++;
            employeeList.Add(unit);
            if (employeesCount == employeesMax)
            {
                freeJob = false;
            }
        }
    }
}
