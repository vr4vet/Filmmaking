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

    private CharacterGrabber currentCharacter;

    public bool canSnap;

    public void SetCurrentCharacter(CharacterGrabber cb)
    {
        currentCharacter = cb;
    }

    public void ResetCanSnap()
    {
        StartCoroutine(CanSnap());
        if (currentCharacter == null) return;
        if (forWhatSocket == SocketType.Head)
        {
            currentCharacter.ResetCurrentHead();
        }
        else if (forWhatSocket == SocketType.Hand)
        {
            currentCharacter.ResetCurrentHand();
        }
        currentCharacter = null;
    }

    private IEnumerator CanSnap()
    {
        yield return new WaitForSeconds(3);
        canSnap = true;
    }
}
