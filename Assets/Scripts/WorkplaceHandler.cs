using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkplaceHandler
{
    List<GameObject> workplaceList;

    public WorkplaceHandler()
    {
        workplaceList = new List<GameObject>();
        workplaceList.AddRange(GameObject.FindGameObjectsWithTag("Workplace"));
    }

    public void AddWorkplaceNode(GameObject workplaceNode)
    {
        workplaceList.Add(workplaceNode);
    }

    public void RemoveWorkplaceNode(GameObject workplaceNode)
    {
        workplaceList.Remove(workplaceNode);
    }

    public void RunProduction()
    {
        foreach (GameObject node in workplaceList)
        {
            node.GetComponent<WorkplaceNode>().Production();
        }
    }

    public GameObject GetFreeWorkplace()
    {
        GameObject freeWorkplace = null;
        for (int i = 0; i < workplaceList.Count; i++)
        {
            if (workplaceList[i].GetComponent<WorkplaceNode>().IsFreeJob())
            {
                freeWorkplace = workplaceList[i];
                break;
            }
        }
        return freeWorkplace;
    }

}
