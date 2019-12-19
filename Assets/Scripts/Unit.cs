using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject resource;
    public GameObject workplace;

    private ResourceNode rsrcNode;
    private WorkplaceNode wpNode;
    private State state;
    private uint resourceAmount;

    enum State
    {
        Idle,
        GoToResource,
        GoToWorkplace
    }


    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        resourceAmount = 0;
        rsrcNode = resource.GetComponent<ResourceNode>();
        wpNode = workplace.GetComponent<WorkplaceNode>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                if (wpNode.IsStorageFree() && rsrcNode.isResourceAvailable())
                {
                    state = State.GoToResource;
                }
                break;
            case State.GoToResource:
                if (transform.position != resource.transform.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, resource.transform.position, 0.2f);
                }
                else
                {
                    resourceAmount += rsrcNode.TakeResource();
                    if (resourceAmount > 0)
                    {
                        //Debug.Log("Unit: Resource taken");
                        state = State.GoToWorkplace;
                    }
                    else
                    {
                        //Debug.Log("Unit: Resource not available");
                        state = State.Idle;
                    }
                }
                break;
            case State.GoToWorkplace:
                if (transform.position != workplace.transform.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, workplace.transform.position, 0.2f);
                }
                else
                {
                    resourceAmount -= wpNode.LeaveResource();
                    if (resourceAmount == 0)
                    {
                        //Debug.Log("Unit: Resource left");
                        state = State.Idle;
                    }
                    else
                    {
                        //Debug.Log("Unit: Workplace full");
                        state = State.Idle;
                    }
                }
                break;
        }
        //Debug.Log(state);
    }
}
