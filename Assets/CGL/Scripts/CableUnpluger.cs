using HurricaneVR.Framework.Core.Grabbers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableUnpluger : MonoBehaviour
{
    [SerializeField] HVRSocket socket;
    [SerializeField] float timerMin;
    [SerializeField] float timerMax;
    public void StartUnplugTimer()
    {
        StartCoroutine(DetachSocketed());
    }

    private IEnumerator DetachSocketed()
    {
        yield return new WaitForSeconds(Random.Range(timerMin, timerMax));
        socket.Detach();
    }
}
