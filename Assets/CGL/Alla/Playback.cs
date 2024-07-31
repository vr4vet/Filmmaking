using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Playback : MonoBehaviour
{
    public RawImage rawImage;
    public AudioSource audioSource;
    public float playbackSpeed = 0.1f;

    private List<Texture2D> frames;
    private float[] audioData;
    private int audioChannels;
    public UnityEvent OnFinishedPlaying;
    public void SetFramesAndAudio(List<Texture2D> capturedFrames, float[] capturedAudioData, int channels)
    {
        frames = capturedFrames;
        audioData = capturedAudioData;
        audioChannels = channels;
    }

    public void StartPlayback()
    {
        if (frames != null && frames.Count > 0 && audioData != null)
        {
            StartCoroutine(PlayFramesAndAudio());
        }
    }
    
    IEnumerator PlayFramesAndAudio()
    {
        int sampleRate = AudioSettings.outputSampleRate;
        //audioSource.clip = AudioClip.Create("PlaybackAudio", audioData.Length / audioChannels, audioChannels, sampleRate, false);
        //audioSource.clip.SetData(audioData, 0);
        //audioSource.Play();

        foreach (Texture2D frame in frames)
        {
            rawImage.texture = frame;
            yield return new WaitForSeconds(playbackSpeed);
        }
        OnFinishedPlaying?.Invoke();

    }
}
