using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesHandler
{
    List<GameObject> resourcesList;

    public ResourcesHandler()
    {
        resourcesList = new List<GameObject>();
        resourcesList.AddRange(GameObject.FindGameObjectsWithTag("Resource"));
    }

    public void AddResourceNode(GameObject resourceNode)
    {
        resourcesList.Add(resourceNode);
    }

    public void RemoveResourceNode(GameObject resourceNode)
    {
        resourcesList.Remove(resourceNode);
    }

    public void RenewResources()
    {
        foreach (GameObject node in resourcesList)
        {
            node.GetComponent<ResourceNode>().RenewResource();
        }
    }
}
