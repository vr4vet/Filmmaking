using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClapperTrigger : MonoBehaviour
{
    bool clapperInView;
    public Material cameraLight;
    private void Awake()
    {
        ClapperBoard.clapped += () => {
            if (clapperInView)
            {
                FilmingGameManager.instance.StartFilming();

                cameraLight.SetColor("_EmissionColor", Color.red);
                cameraLight.SetColor("_BaseColor", Color.red);
            }
        
            
        
        };
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
