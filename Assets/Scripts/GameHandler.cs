using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    enum State
    {
        Idle,
        PlaceWorkplace
    }

    NodeGrid grid;
    Plane plane;
    Vector3 hitPoint;
    State gameState;

    UnitsHandler unitsHandler;
    ResourcesHandler resourcesHandler;
    WorkplaceHandler workplaceHandler;

    GameObject currentGameObjectPrefab;
    GameObject createdGameObjectToPlace;
    float timeSpan = 0;
    float sendRate = 1.0f;

    void Start()
    {
        gameState = State.Idle;
        grid = new NodeGrid(11, 11);
        plane = new Plane(Vector3.up, Vector3.zero);
        unitsHandler = new UnitsHandler();
        resourcesHandler = new ResourcesHandler();
        workplaceHandler = new WorkplaceHandler();

    }

    void Update()
    {
        switch (gameState)
        {
            case State.Idle:
                //Debug.Log("Idle");
                break;
            case State.PlaceWorkplace:
                //Debug.Log("Place Workplace");
                SnapObjectToActiveNode(createdGameObjectToPlace);
                if (Input.GetMouseButton(0))
                {
                    workplaceHandler.AddWorkplaceNode(createdGameObjectToPlace);
                    createdGameObjectToPlace = null;
                    gameState = State.Idle;
                }
                if (Input.GetMouseButton(1))
                {
                    Destroy(createdGameObjectToPlace);
                    gameState = State.Idle;
                }
                break;
        }
        timeSpan += Time.deltaTime;
        if (timeSpan > sendRate)
        {
            timeSpan -= sendRate;
            //Action
            unitsHandler.EmployUnits(workplaceHandler);
            resourcesHandler.RenewResources();
            workplaceHandler.RunProduction();
        }
    }

    void OnDrawGizmosSelected()
    {
        Node[,] nodes = grid.GetNodes();
        foreach (Node node in nodes)
        {
            Gizmos.color = node.GetActivation() ?  Color.yellow : Color.red;
            Gizmos.DrawSphere(node.GetWorldPos(), 1);
        }
    }
    
    public void PlaceWorkplace()
    {
        currentGameObjectPrefab = Resources.Load("Prefabs/Workplace") as GameObject;
        createdGameObjectToPlace = Instantiate(currentGameObjectPrefab, Vector3.zero, Quaternion.identity);
        gameState = State.PlaceWorkplace;
    }

    void SnapObjectToActiveNode(GameObject objectToSnap)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float enter))
        {
            hitPoint = ray.GetPoint(enter);
            grid.ActivateClosestNodeToHitPoint(hitPoint);
            objectToSnap.transform.position = grid.GetActiveNodePosition();
        }
    }
}
