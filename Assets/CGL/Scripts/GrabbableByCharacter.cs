using HurricaneVR.Framework.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SocketType
{
    Head,
    Hand
}

public class GrabbableByCharacter : MonoBehaviour
{
    public SocketType forWhatSocket;
    public Rigidbody rb;
    public HVRGrabbable hvrGrabbable;
    public GameObject parentTransform;

    public bool canSnap;

    public bool isHeld;

    public void SetCanSnap(bool value)
    {
        canSnap = value;
    }
}
