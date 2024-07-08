using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AudioLevelUI : MonoBehaviour
{
    public Transform bar;
    public Image barImage;
    public TextMeshProUGUI text;
    public GameObject speakingIcon;
    public Gradient uiColor;
    public void SetValue(float value)
    {
        bar.localScale = new Vector3(1, value, 1);
        text.text = "Bad";
        text.color = uiColor.Evaluate(value);
        barImage.color = uiColor.Evaluate(value);
        if (value>0.3f)
        {
            text.text = "Not Bad";
        }
        if (value > 0.6f) 
        {

            text.text = "Good";
        }
    }
}
