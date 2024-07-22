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

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<GrabbableByCharacter>() && 
            other.GetComponent<GrabbableByCharacter>().canSnap)
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
        }
    }

    private void EnablePhysicsGrabbing(GrabbableByCharacter obj)
    {
        obj.rb.isKinematic = false;
        obj.rb.AddForce(new Vector3(0f, 1f, 1f) * 2, ForceMode.Impulse);
        obj.ResetCanSnap(); 
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

    public void ResetCurrentHead()
    {
        currentHeadGrabbable = null;
    }

    public void ResetCurrentHand()
    {
        currentHandGrabbable = null;
    }

    
}
