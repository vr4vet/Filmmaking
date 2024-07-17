using HurricaneVR.Framework.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightManager : MonoBehaviour
{
    public List<MovingLight> lights = new List<MovingLight>();
    public float offsetRange;
    public HVRPhysicsButton slider;

    private void Update()
    {

        SetOffset(slider.GetNormalizedDistance());
    }
    public void SetOffset(float offset)
    {
        foreach (var light in lights)
        {
            light.SetOffeset(offset* offsetRange);
        }
    }
}
