using HurricaneVR.Framework.Core;
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

    private GrabbableByCharacter currentHandGrabbable;
    private GrabbableByCharacter currentHeadGrabbable;

    //private Collider mostRecentCollider;
    //private HashSet<Collider> collidedObjects = new HashSet<Collider>();


    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<GrabbableByCharacter>() && other.GetComponent<GrabbableByCharacter>().canSnap)
        {
            GrabbableByCharacter prop = other.GetComponent<GrabbableByCharacter>();
            prop.SetCurrentCharacter(this);
            prop.canSnap = false;
            DisablePhysicsGrabbing(prop);
            if (prop.forWhatSocket == SocketType.Head)
            {
                if (currentHeadGrabbable != null)
                {
                    EnablePhysicsGrabbing(currentHeadGrabbable);
                }
                SnapToHead(prop.parentTransform);
                currentHeadGrabbable = prop;
            }
            else if (prop.forWhatSocket == SocketType.Hand)
            {
                if (currentHandGrabbable != null)
                {
                    EnablePhysicsGrabbing(currentHandGrabbable);
                }
                SnapToHand(prop.parentTransform);
                currentHandGrabbable = prop;
            }

            //mostRecentCollider = other;
        }

        /*GrabbableByCharacter prop = null;

        if (other.GetComponent<GrabbableByCharacter>())
        {
            prop = other.GetComponent<GrabbableByCharacter>();
            if (prop.isHeld) return;
            if (!prop.canSnap) return;
            if (prop.hvrGrabbable.IsHandGrabbed) { return; }

            Collider col1 = GetComponent<CapsuleCollider>();

        }
        else
        {
            return;
        }

        if (prop.forWhatSocket == SocketType.Head)
        {
            if (currentHeadGrabbable != null) {
                currentHandGrabbable.isHeld = false;
                currentHandGrabbable.ResetCanSnap();
                EnablePhysicsGrabbing(currentHeadGrabbable);
            }
            currentHeadGrabbable = prop;
            SnapToHead(currentHeadGrabbable.parentTransform);
            DisablePhysicsGrabbing(currentHeadGrabbable);
            currentHeadGrabbable.isHeld = true;
            currentHeadGrabbable.canSnap = false;
        }
        else if (prop.forWhatSocket == SocketType.Hand)
        {
            if (currentHandGrabbable != null)
            {
                currentHandGrabbable.isHeld = false;
                currentHandGrabbable.ResetCanSnap();
                EnablePhysicsGrabbing(currentHandGrabbable);
            }
            currentHandGrabbable = prop;
            SnapToHand(currentHandGrabbable.parentTransform);
            DisablePhysicsGrabbing(currentHandGrabbable);
            currentHandGrabbable.isHeld = true;
            currentHandGrabbable.canSnap = false;
        }*/
    }

    /*private void ResetSnapping(GrabbableByCharacter gbc)
    {
        *//*Collider col = mostRecentCollider;
        yield return new WaitForSeconds(4);
        if (collidedObjects.Contains(col))
        {
            collidedObjects.Remove(col);
        }*//*
        gbc.ResetCanSnap();

    }*/

    private void EnablePhysicsGrabbing(GrabbableByCharacter obj)
    {
        obj.rb.isKinematic = false;
        obj.rb.AddForce(new Vector3(0f, 1f, 1f) * 2, ForceMode.Impulse);
        obj.ResetCanSnap(); 
        //obj.hvrGrabbable.CanBeGrabbed = true;
    }

    private void DisablePhysicsGrabbing(GrabbableByCharacter obj)
    {
        obj.rb.isKinematic = true;
        //obj.hvrGrabbable.CanBeGrabbed = false;
        //obj.canSnap = false;
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

    public void ResetCurrentHead()
    {
        currentHeadGrabbable = null;
    }

    public void ResetCurrentHand()
    {
        currentHandGrabbable = null;
    }

    
}
