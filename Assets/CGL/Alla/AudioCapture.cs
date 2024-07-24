using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioCapture : MonoBehaviour
{
    [SerializeField]private List<float> audioData;
    private int channels;
    private bool isRecording;

    void Start()
    {
        audioData = new List<float>();
        isRecording = false;

    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (isRecording)
        {
            // Save the audio data
            audioData.AddRange(data);
            this.channels = channels;
        }
    }

    public void StartRecording()
    {
        isRecording = true;
    }

    public void StopRecording()
    {
        isRecording = false;
    }

    public float[] GetAudioData()
    {
        return audioData.ToArray();
    }

    public int GetChannels()
    {
        return channels;
    }
}
