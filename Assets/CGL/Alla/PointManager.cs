using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public List<PointObjectiveSO> objectives;
    public List<PointObjectiveSO> solvedObjectives;

    public void SolveObjective(PointObjectiveSO objective)
    {
        if (solvedObjectives.Contains(objective)&&objective.oneTime) return;
        solvedObjectives.Add(objective);
    }
    public void UnSolveObjective(PointObjectiveSO objective)
    {
        if (!solvedObjectives.Contains(objective)) return;
        solvedObjectives.Remove(objective);
    }
    public int CalculatePoints()
    {
        int points = 0;
        foreach (var objective in solvedObjectives)
        {
            points += objective.points;
        }

        return points;
    }

}
