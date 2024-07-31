using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundboardSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    public void PlaySound(AudioClip clip)
    {
        if (!source.isPlaying)
        {
            source.PlayOneShot(clip);
        }
    }
}
