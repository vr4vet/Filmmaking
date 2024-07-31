using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClapperTrigger : MonoBehaviour
{
    bool clapperInView;
    public Material cameraLight;
    public Light redLight;
    private void Awake()
    {
        ClapperBoard.clapped += () => {
            if (clapperInView && !FilmingGameManager.instance.startedFilming)
            {
                FilmingGameManager.instance.StartFilming();

                cameraLight.SetColor("_EmissionColor", Color.red);
                cameraLight.SetColor("_BaseColor", Color.red);
                redLight.enabled = true;
            }
        
            
        
        };
        cameraLight.SetColor("_EmissionColor", Color.white);
        cameraLight.SetColor("_BaseColor", Color.white);
        redLight.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Clapper")) clapperInView = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Clapper")) clapperInView = false;
    }
}
