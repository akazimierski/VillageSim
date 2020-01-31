using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject resource;
    public GameObject workplace;
    public bool employed;

    private ResourceNode rsrcNode;
    private WorkplaceNode wpNode;
    private State state;
    private uint carryResource;

    enum State
    {
        Idle,
        GoToResource,
        GoToWorkplace
    }

    // Start is called before the first frame update
    void Start()
    {
        employed = false;
        state = State.Idle;
        carryResource = 0;
        rsrcNode = resource.GetComponent<ResourceNode>();
        wpNode = workplace.GetComponent<WorkplaceNode>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                if (wpNode.IsStorageFree() && rsrcNode.IsResourceAvailable())
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
                    carryResource += rsrcNode.TakeResource();
                    if (carryResource > 0)
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
                    carryResource -= wpNode.LeaveResource();
                    if (carryResource == 0)
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

    public void AssignWorkplace(GameObject workplace)
    {
        if (this.workplace == null)
        {
            this.workplace = workplace;
            this.employed = true;
        }
    }

    public void AssignResource(GameObject resource)
    {
        if (this.resource == null)
        {
            this.resource = resource;
        }
    }

    public bool IsUnemployed()
    {
        return !employed;
    }
}
