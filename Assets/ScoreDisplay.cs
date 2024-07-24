using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;

public class ScoreDisplay : MonoBehaviour
{

    public TextMeshProUGUI title;
    public TextMeshProUGUI TotalScore;
    public TextMeshProUGUI Objective;
    public List<Image> stars;
    private void Awake()
    {
        
        for (int i = 0; i < stars.Count; i++)
        {
            stars[i].enabled = false;
        
        }
    }
    private void Start()
    {

      
     
    }
    TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> t;
    int starCount;
   
    public void DisplayScore()
    {
        //foreach (var item in PointManager.instance.GetUniqueObjectives())
        //{
        //    GameObject tmp = Instantiate(Objective.gameObject,Objective.transform.parent);

        //    tmp.GetComponent<TextMeshProUGUI>().text = item.Key.displayTitle + " : " + item.Value.Value;
        //    tmp.SetActive(true);
        //}
       
        starCount =(int) PointManager.instance.CalculateTotalPoints()/10000;
        starCount = 3;
        stars[0].enabled = true;
        stars[0].rectTransform.localScale = Vector3.zero;
        stars[0].rectTransform.DOScale(Vector3.one, 1f).SetEase(Ease.InOutBounce).OnComplete(()=>AnimateStar(1));
 
      
    }
    public void AnimateStar(int i) 
    {
        if (i > starCount - 1) return;
         stars[i ].enabled = true;
        stars[i].rectTransform.localScale = Vector3.zero;
        stars[i].rectTransform.DOScale(Vector3.one, 1f).SetEase(Ease.InOutBounce).OnComplete(() => AnimateStar(i+1));

    }

}
