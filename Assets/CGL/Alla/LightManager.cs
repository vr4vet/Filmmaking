using HurricaneVR.Framework.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightManager : MonoBehaviour
{
    public List<MovingLight> lights = new List<MovingLight>();
    public float offsetRange;
    [Serializable]
    public class Slider
    {
        public HVRPhysicsButton sliderBtn;
        public float offset;
    }
    public List<Slider> sliders;

    private void Update()
    {
        for (int i = 0; i < sliders.Count; i++) {

            SetOffset(i, sliders[i].sliderBtn.GetNormalizedDistance()- sliders[i].offset);
        }
       
    }
    public void SetOffset(int index, float offset)
    {

        lights[index].SetOffeset(offset* offsetRange);
        
    }

    private void OnDrawGizmos()
    {
        foreach (var slider in sliders)
        {
            Gizmos.DrawCube(
                Vector3.Lerp(slider.sliderBtn.transform.parent.TransformPoint( slider.sliderBtn.StartPosition),
                slider.sliderBtn.transform.parent.TransformPoint(slider.sliderBtn.EndPosition), slider.offset)
                , new Vector3(0.01f, 0.01f, 0.01f));
        }
    }
}
