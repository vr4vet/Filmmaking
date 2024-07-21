using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreDisplay : MonoBehaviour
{

    public TextMeshProUGUI title;
    public TextMeshProUGUI TotalScore;
    public TextMeshProUGUI Objective;
    private void Awake()
    {
        Objective.gameObject.SetActive(false);
    }
    private void Start()
    {

        FilmingGameManager.instance.OnStopFilming += () => { DisplayScore(); };
     
    }
    public void DisplayScore()
    {
        foreach (var item in PointManager.instance.GetUniqueObjectives())
        {
            GameObject tmp = Instantiate(Objective.gameObject);
            tmp.GetComponent<TextMeshProUGUI>().text = item.Key.displayTitle + " : " + item.Value.Value;
            tmp.SetActive(true);
        }
    }
}
