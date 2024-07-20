using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public List<PointObjectiveSO> objectives;
    public List<PointObjectiveSO> solvedObjectives;
    public static PointManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(gameObject);
    }
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
    public List<PointObjectiveSO> GetUniqueObjectives() 
    {
        List<PointObjectiveSO> returnList = new List<PointObjectiveSO>();   
        foreach (var item in solvedObjectives)
        {
            if (!returnList.Contains(item))
            {
                returnList.Add(item);
            }
            else
            {
                returnList[returnList.IndexOf(item)].points+=item.points;
            }
        }
    
    return returnList;
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
