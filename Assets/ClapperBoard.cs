using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;   
public class ClapperBoard : MonoBehaviour
{
    public AudioSource AudioSource;
    public float minVelocity;
    public ParticleSystem Ps;
    public static UnityAction clapped;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Clapper")&& collision.relativeVelocity.magnitude > minVelocity)
        {
            Ps.Play();
            clapped.Invoke();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Clapper") && other.attachedRigidbody.velocity.magnitude > minVelocity)
        {
            Ps.Play();
            clapped.Invoke();
        }
    }
}
