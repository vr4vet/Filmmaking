using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class SoundListener : MonoBehaviour
{
    [Range(0,90)]
    public float angel;
    [Range(0, 10)]
    public float distance;
    public float scoreSampleFrequncy=4;
    public PointObjectiveSO objective;
    bool start;
    float lastsample=0;
    float accscore;
    int numberOfSamples;
    private void Start()
    {
        FilmingGameManager.instance.OnStartFilming += () => { start = true; lastsample = Time.time; };
        FilmingGameManager.instance.OnStopFilming += () => { start = false; };
    }
    private void Update()
    {
        if (!start) return;
        
        if(Time.time- lastsample > scoreSampleFrequncy)
        {
            objective.points = (int)(accscore/scoreSampleFrequncy);
            PointManager.instance.SolveObjective(objective, (int)accscore);
            accscore = 0;
            lastsample = Time.time;
        }
        accscore += DialogManager.Instance.GetAccuracyFromCurrentSpeaker();

    }
}
