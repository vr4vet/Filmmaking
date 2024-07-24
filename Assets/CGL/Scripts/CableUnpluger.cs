using HurricaneVR.Framework.Core.Grabbers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CableUnpluger : ObjectiveHandler
{
    [SerializeField] HVRSocket socket;
    [SerializeField] float timerMin;
    [SerializeField] float timerMax;

    [SerializeField] float impulseForce = 3f;

    [SerializeField] private Rigidbody cableHeadRb;
    [SerializeField] private ParticleSystem sparksParticles;

    [SerializeField] private List<Light> lights  =  new List<Light>();
    [SerializeField] private List<Material> materials = new List<Material>();
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
        //Detachocket();
    }
    private void Update()
    {
        if (socket.IsGrabbing)
        {
            base.ObjectiveOff();
         

        }
        else
        {
            base.ObjectiveOn();
        }
        base.Update();
    }
    public void StartUnplugTimer()
    {
      //  StartCoroutine(DetachSocketed());
    }

    private IEnumerator DetachSocketed()
    {
        yield return new WaitForSeconds(Random.Range(timerMin, timerMax));
        socket.Detach();
        sparksParticles.Play();
        AddImpulseForce();
        Debug.Log("DO ONLY ONCE");
    }

    public void Detachocket()
    {
        socket.Detach();
        sparksParticles.Play();
        AddImpulseForce();
        ShutDownLight();
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
        foreach (var mat in materials)
        {
            mat.SetColor("_EmissionColor", Color.black);
        }
    }

    public void EnableLight()
    {

        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].intensity = intencities[i];
        }
        foreach (var mat in materials)
        {
            mat.SetColor("_EmissionColor", new Color(2,2,2,2) );
        }
    }
    
}
