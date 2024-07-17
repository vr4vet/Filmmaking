using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playOnStart : MonoBehaviour
{

    AudioSource audioSource;

    private void Awake(){
         audioSource = GetComponent<AudioSource>();
    }

    private void Start(){
        if (audioSource.playOnAwake)
        audioSource.Play();
    }
}