using System.Collections.Generic;
using UnityEngine;

public class CameraCapture : MonoBehaviour
{
    public RenderTexture renderTexture;
    private List<Texture2D> frames;
    private bool isRecording;
    float lastFrame;
    void Start()
    {
       
        frames = new List<Texture2D>();
        isRecording = false;
    }

    void Update()
    {
        if (isRecording && Time.time-lastFrame>=0.041f)
        {
            CaptureFrame();
            lastFrame=Time.time;
        }
    }

    void CaptureFrame()
    {
        RenderTexture.active = renderTexture;
        Texture2D frame = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        frame.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        frame.Apply();
        frames.Add(frame);
        RenderTexture.active = null;
    }
    public List<Texture2D> GetFrames() { return frames; }
    public void StartRecording()
    {
        lastFrame = Time.time;
        isRecording = true;
    }

    public void StopRecording()
    {
        isRecording = false;
    }
}
