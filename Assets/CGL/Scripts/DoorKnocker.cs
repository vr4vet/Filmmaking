using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKnocker : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private float deliveryCooldown;
    private bool isKnocking = false;
    private float elapsedTime = 0;


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
        animator.SetTrigger("arrive");
    }

    public void ConfirmDelivery()
    {
        animator.SetTrigger("depart");
        isKnocking = false;
    }
}
