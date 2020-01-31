using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsHandler
{
    List<GameObject> unitsList;

    public UnitsHandler()
    {
        unitsList = new List<GameObject>();
        unitsList.AddRange(GameObject.FindGameObjectsWithTag("Unit"));
    }

    public void EmployUnits(WorkplaceHandler workplaceHandler)
    {
        foreach (var unit in unitsList)
        {
            if (unit.GetComponent<Unit>().IsUnemployed())
            {
                AssignJobToUnit(workplaceHandler, unit);
            }
        }
    }

    private void AssignJobToUnit(WorkplaceHandler workplaceHandler, GameObject unit)
    {
        var wp = workplaceHandler.GetFreeWorkplace();
        if (wp != null)
        {
            unit.GetComponent<Unit>().AssignWorkplace(wp);
            wp.GetComponent<WorkplaceNode>().EmployeeJoined(unit);
        }
    }
}
