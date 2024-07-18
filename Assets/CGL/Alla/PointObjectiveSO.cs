using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Point System",order =1,fileName = "Point Objective")]
public class PointObjectiveSO : ScriptableObject
{
    public int points = 10;
    public bool required=false;
    public bool oneTime;
}
