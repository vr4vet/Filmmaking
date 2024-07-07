using HurricaneVR.Framework.Core.Grabbers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableUnpluger : MonoBehaviour
{
    [SerializeField] HVRSocket socket;
    [SerializeField] float timerMin;
    [SerializeField] float timerMax;

    [SerializeField] float impulseForce = 3f;

    [SerializeField] private Rigidbody cableHeadRb;
    [SerializeField] private ParticleSystem sparksParticles;

    private void Start()
    {
        Vector3 localUpBackDirection = new Vector3(0, 1, -1).normalized;
        Vector3 worldDirection = cableHeadRb.transform.TransformDirection(localUpBackDirection);
    }
    public void StartUnplugTimer()
    {
        StartCoroutine(DetachSocketed());
    }

    private IEnumerator DetachSocketed()
    {
        yield return new WaitForSeconds(Random.Range(timerMin, timerMax));
        socket.Detach();
        sparksParticles.Play();
        AddImpulseForce();
    }

    void AddImpulseForce()
    {
        // Define the local up-back direction
        Vector3 localUpBackDirection = cableHeadRb.transform.up + -cableHeadRb.transform.right;

        // Add impulse force to the Rigidbody in the transformed direction
        cableHeadRb.AddForce(localUpBackDirection, ForceMode.Impulse);
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position, worldDirection, Color.red);
    }
}
