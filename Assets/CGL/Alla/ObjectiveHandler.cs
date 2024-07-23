using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public abstract class ObjectiveHandler : MonoBehaviour
{
    public PointObjectiveSO objective;
    float lastsample = 0;
    float accscore;
    [HideInInspector]private bool objectiveOn;
    int maxPoints;
    public float pointMultiplier=1;
    public void Start()
    {

        FilmingGameManager.instance.OnStartFilming += () => {  lastsample = Time.time; };
    }
    public void Update()
    {

        if (!FilmingGameManager.instance||!FilmingGameManager.instance.startedFilming)
            return;
        if (!objectiveOn)
            return;
        if (Time.time - lastsample > objective.frequency)
        {
            
            PointManager.instance.SolveObjective(objective, (int)(accscore / objective.frequency), 0);
            accscore = 0;
            lastsample = Time.time;
        }
        accscore += objective.points* pointMultiplier;

    }
    private void LateUpdate()
    {
        
    }
    public void ObjectiveOn() {
        if (objectiveOn) return; 
        lastsample = Time.time;
        objectiveOn = true; 
    
    }
    public void ObjectiveOff() { objectiveOn = false; }
}
