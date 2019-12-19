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
    GameObject currentGameObjectPrefab;
    GameObject createdGameObjectToPlace;

    void Start()
    {
        gameState = State.Idle;
        grid = new NodeGrid(11, 11);
        plane = new Plane(Vector3.up, Vector3.zero);
    }

    void Update()
    {
        switch (gameState)
        {
            case State.Idle:
                Debug.Log("Idle");
                break;
            case State.PlaceWorkplace:
                Debug.Log("Place Workplace");
                snapObjectToActiveNode(createdGameObjectToPlace);
                if (Input.GetMouseButton(0))
                {
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

    void snapObjectToActiveNode(GameObject objectToSnap)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter = 0.0f;
        if (plane.Raycast(ray, out enter))
        {
            hitPoint = ray.GetPoint(enter);
            grid.ActivateClosestNodeToHitPoint(hitPoint);
            objectToSnap.transform.position = grid.GetActiveNodePosition();
        }
    }
}
