using HurricaneVR.Framework.Core.Grabbers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CableUnpluger : MonoBehaviour
{
    [SerializeField] HVRSocket socket;
    [SerializeField] float timerMin;
    [SerializeField] float timerMax;

    [SerializeField] float impulseForce = 3f;

    [SerializeField] private Rigidbody cableHeadRb;
    [SerializeField] private ParticleSystem sparksParticles;

    [SerializeField] private List<Light> lights  =  new List<Light>();
    private float[] intencities;

    private void Start()
    {
        intencities = new float[lights.Count];
        for (int i = 0; i < lights.Count; i++)
        {
            intencities[i] = lights[i].intensity;
        }
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
        Vector3 localUpBackDirection = cableHeadRb.transform.up + -cableHeadRb.transform.right;
        cableHeadRb.AddForce(localUpBackDirection, ForceMode.Impulse);
    }

    public void ShutDownLight()
    {
        foreach (var light in lights)
        {
            light.intensity = 0;
        }
    }

    public void EnableLight()
    {

        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].intensity = intencities[i];
        }
    }
    
}
