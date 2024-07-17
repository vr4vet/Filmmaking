using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core;
public class ExtenadableMic : MonoBehaviour
{
    [SerializeField]
    int grabber = 0;
    [SerializeField]
    bool topGrabed;
    [SerializeField]
    bool BottonGrabed;
    [SerializeField]
    HVRHandGrabber hand1;
    [SerializeField]
    HVRHandGrabber hand2;
    [SerializeField]
    float distance;

    public Transform midPoint;
    public GameObject Top;
    public void OnGrabe(HVRGrabberBase Gbase, HVRGrabbable gabbale)
    {
        HVRHandGrabber tmp = Gbase as HVRHandGrabber;
        if (tmp == null) return;
        grabber++;
     
        if (hand1 == null)
            hand1 = tmp;
        else { 
            hand2 = tmp;
            distance = Vector3.Distance(hand1.transform.position, hand2.transform.position);
        }
        if (transform.InverseTransformPoint(tmp.gameObject.transform.position).y > midPoint.localPosition.y)
        {
            topGrabed = true;
        }
        else
        {
            BottonGrabed = true;

        }
    }
    public void OnUnGrabe(HVRGrabberBase Gbase, HVRGrabbable gabbale)
    {
        HVRHandGrabber tmp = Gbase as HVRHandGrabber;
        if (tmp == null) return;
        grabber--;
        if (transform.InverseTransformPoint(hand1.gameObject.transform.position).y > midPoint.localPosition.y)
        {
            topGrabed = false;
        }
        else
        {
            BottonGrabed = false;
        }
        if (hand2 != null)
            hand2 = null;
        else hand1 = null;
    }

    private void Update()
    {
        if (grabber < 2) return;

        if(!topGrabed||!BottonGrabed)return;
        
        

        float diffrance = Vector3.Distance(hand1.transform.position, hand2.transform.position)- distance ;
        distance = Vector3.Distance(hand1.transform.position, hand2.CachedWorldPosition);
        Top.transform.localPosition = new Vector3( Top.transform.localPosition.x, Top.transform.localPosition.y + diffrance, Top.transform.localPosition.z)  ;



    }
}
