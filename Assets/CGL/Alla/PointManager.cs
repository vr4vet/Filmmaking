using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public List<PointObjectiveSO> objectives;
    public Dictionary<PointObjectiveSO,int?> solvedObjectives = new Dictionary<PointObjectiveSO, int?>();
    public static PointManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(gameObject);
    }
    public void SolveObjective(PointObjectiveSO objective,int points)
    {
        solvedObjectives.TryGetValue(objective, out var result);
        if (result.HasValue)
            if (objective.oneTime)
                return;
            else
                solvedObjectives[objective] = solvedObjectives[objective].Value + points;
        else
        solvedObjectives.Add(objective,points);
    }
    public void UnSolveObjective(PointObjectiveSO objective)
    {
        //if (!solvedObjectives.Contains(objective)) return;
        //solvedObjectives.Remove(objective);
    }
    public Dictionary<PointObjectiveSO,int?> GetUniqueObjectives() 
    {
        return solvedObjectives;
    

    }
    public int CalculateTotalPoints()
    {
        int points = 0;
        foreach (var objective in solvedObjectives)
        {
            points += objective.Value.Value;
        }

        return points;
    }

}
