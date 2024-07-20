using HurricaneVR.Framework.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGrabber : MonoBehaviour
{
    [SerializeField] private Transform headTransform;
    [SerializeField] private Transform handTransform;

    private bool isHandBuzy;
    private bool isHeadBuzy;

    private GrabbableByCharacter currentHandGrabbable;
    private GrabbableByCharacter currentHeadGrabbable;


    private void OnTriggerEnter(Collider other)
    {
        GrabbableByCharacter prop = null;

        if (other.GetComponent<GrabbableByCharacter>())
        {
            prop = other.GetComponent<GrabbableByCharacter>();
            if (prop.isHeld) return;
            if (!prop.canSnap) return;
        }
        else
        {
            return;
        }

        if (prop.forWhatSocket == SocketType.Head)
        {
            if (currentHeadGrabbable != null) { 
                EnablePhysicsGrabbing(currentHeadGrabbable);
                currentHandGrabbable.isHeld = false;
            }
            currentHeadGrabbable = prop;
            SnapToHead(currentHeadGrabbable.parentTransform);
            DisablePhysicsGrabbing(currentHeadGrabbable);
            currentHeadGrabbable.isHeld = true;
        }
        else if (prop.forWhatSocket == SocketType.Hand)
        {
            if (currentHandGrabbable != null)
            {
                EnablePhysicsGrabbing(currentHandGrabbable);
                currentHandGrabbable.isHeld = false;
            }
            currentHandGrabbable = prop;
            SnapToHand(currentHandGrabbable.parentTransform);
            DisablePhysicsGrabbing(currentHandGrabbable);
            currentHandGrabbable.isHeld = true;
        }
    }

    private void EnablePhysicsGrabbing(GrabbableByCharacter obj)
    {
        obj.rb.isKinematic = false;
        obj.hvrGrabbable.CanBeGrabbed = true;
    }

    private void DisablePhysicsGrabbing(GrabbableByCharacter obj)
    {
        obj.rb.isKinematic = true;
        obj.hvrGrabbable.CanBeGrabbed = false;
        obj.canSnap = false;
    }

    private void SnapToHead(GameObject obj)
    {
        obj.transform.position = headTransform.position;
        obj.transform.rotation = headTransform.rotation;
    }

    private void SnapToHand(GameObject obj)
    {
        obj.transform.position = handTransform.position;
        obj.transform.rotation = handTransform.rotation;
    }
}
