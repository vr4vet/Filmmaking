using System.Collections;
using System.Collections.Generic;
using System.Drawing.Design;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Animator rootAnimator;
    public Animator characterAnimator;
    public CharacterGrabber grabber;
    public GrabbableByCharacter helmet;
    public Transform head;
    public DoorKnocker door;
    public enum anim
    {
        Idle,Walk,Talk,Hide,Unhide,Attack,Shove,Helmet
    }
    private void Start()
    {
        PauseAnimation();
    }
    public void PauseAnimation()
    {
        rootAnimator.speed = 0;
        print("Pausing Animation");
        
    }
    public void UnPauseAnimation()
    {
        rootAnimator.speed = 1f;
    }
    public void ChangeState(anim nim)
    {
        
        characterAnimator.Play(nim.ToString(), 0); 
               
         
    }
    public void PlayDialog()
    {
        DialogManager.Instance.UnPause();
    }
    public void WearHelmet()
    {
        if (!grabber.currentHandGrabbable == helmet) return;
        helmet.parentTransform.transform.SetParent(head, false);

    }
    public void Knocking()
    {
        door.StartKnocking();
    }
}
