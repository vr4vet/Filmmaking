using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;

public class CharacterGrabber : MonoBehaviour
{
    [SerializeField] private Transform headTransform;
    [SerializeField] private Transform handTransform;

    private bool isHandBuzy;
    private bool isHeadBuzy;

    public GrabbableByCharacter currentHandGrabbable{get;private set;}
    public GrabbableByCharacter currentHeadGrabbable { get; private set; }

    private Transform cachTransform;

    private void OnTriggerStay(Collider other)
    {
        Grabbing(other);
    }

    private void Grabbing(Collider other)
    {
        GrabbableByCharacter prop = other.GetComponent<GrabbableByCharacter>();

        if (prop &&
            prop.canSnap && !prop.hvrGrabbable.IsHandGrabbed )
        {
            prop.SetCurrentCharacter(this);
            prop.canSnap = false;
            DisablePhysicsGrabbing(prop);
            if (prop.forWhatSocket == SocketType.Head)
            {
                if (currentHeadGrabbable != null)
                {
                    currentHeadGrabbable.hvrGrabbable.HandGrabbed.RemoveListener(ResetCurrentHead);
                    EnablePhysicsGrabbing(currentHeadGrabbable);
                }
                cachTransform = prop.parentTransform.transform.parent;
                SnapToHead(prop.parentTransform);
                currentHeadGrabbable = prop;
                currentHeadGrabbable.hvrGrabbable.HandGrabbed.AddListener(ResetCurrentHead);
            }
            else if (prop.forWhatSocket == SocketType.Hand)
            {
                if (currentHandGrabbable != null)
                {
                    currentHandGrabbable.hvrGrabbable.HandGrabbed.RemoveListener(ResetCurrentHand);
                    EnablePhysicsGrabbing(currentHandGrabbable);
                }
                cachTransform = prop.parentTransform.transform.parent;
                SnapToHand(prop.parentTransform);
                currentHandGrabbable = prop;
                currentHandGrabbable.hvrGrabbable.HandGrabbed.AddListener(ResetCurrentHand);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Grabbing(other);
    }

    private void EnablePhysicsGrabbing(GrabbableByCharacter obj)
    {
        obj.parentTransform.transform.SetParent(cachTransform);
        obj.rb.isKinematic = false;
        obj.rb.AddForce(new Vector3(0f, 1f, 1f) * 2, ForceMode.Impulse);
        //obj.ResetCanSnap(); 
    }

    private void DisablePhysicsGrabbing(GrabbableByCharacter obj)
    {
        obj.rb.isKinematic = true;
        obj.hvrGrabbable.ForceRelease();
    }

    private void SnapToHead(GameObject obj)
    {
        obj.transform.SetParent(headTransform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    private void SnapToHand(GameObject obj)
    {
        obj.transform.SetParent(handTransform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public void ResetCurrentHead(HVRHandGrabber x, HVRGrabbable y)
    {
        currentHeadGrabbable.parentTransform.transform.SetParent(cachTransform);
        currentHeadGrabbable.hvrGrabbable.HandGrabbed.RemoveListener(ResetCurrentHead);
        currentHeadGrabbable = null;
    }

    public void ResetCurrentHand(HVRHandGrabber x, HVRGrabbable y)
    {
        currentHandGrabbable.parentTransform.transform.SetParent(cachTransform);
        currentHandGrabbable.hvrGrabbable.HandGrabbed.RemoveListener(ResetCurrentHand);
        currentHandGrabbable = null;
    }

    
}
