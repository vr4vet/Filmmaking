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

        DisplayScore();
    }
    public void DisplayScore()
    {
        foreach (var item in PointManager.instance.GetUniqueObjectives())
        {
            GameObject tmp = Instantiate(Objective.gameObject);
            tmp.GetComponent<TextMeshProUGUI>().text = item.displayTitle + " : " + item.points;
            tmp.SetActive(true);
        }
    }
}
