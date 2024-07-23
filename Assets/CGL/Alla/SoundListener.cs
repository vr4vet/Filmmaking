using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class SoundListener : ObjectiveHandler
{
    [Range(0,90)]
    public float angel;
    [Range(0, 10)]
    public float distance;

  
  
    new private void Update()
    {
        if (DialogManager.Instance.pause)
        {
            ObjectiveOff();
        }
        else
            ObjectiveOn();
        pointMultiplier=DialogManager.Instance.GetAccuracyFromCurrentSpeaker();
        base.Update();
    }
}
