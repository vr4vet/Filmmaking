using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  

public class DoorKnocker : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private float deliveryCooldown;
    private bool isKnocking = false;
    private float elapsedTime = 0;
    public Transform window;
    public Transform clipBoard;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isKnocking)
            elapsedTime += Time.deltaTime;

        if (elapsedTime >= deliveryCooldown)
        {
            isKnocking = true;
            StartKnocking();
            elapsedTime = 0;
        }
    }
    
    private void StartKnocking()
    {
        //animator.SetTrigger("arrive");
        window.DOMoveY(window.position.y + 0.13f, 2).SetEase(Ease.OutBounce).OnComplete(()=>  clipBoard.DOLocalMoveZ(clipBoard.localPosition.z + 0.4f, 2) );
       
    }

    public void ConfirmDelivery()
    {
        //animator.SetTrigger("depart");
        isKnocking = false;
        clipBoard.DOLocalMoveZ(clipBoard.localPosition.z - 0.4f, 2).OnComplete(() =>window.DOMoveY(window.position.y - 0.13f, 2).SetEase(Ease.OutBounce) );
        
    }
}
