using System.Collections.Generic;
using UnityEngine;

public class RecordingScript : MonoBehaviour
{
    //circular buffer recording
    //as in, you always record (if the camera is on) and you can press "finish" to save the clipboard, so to say.



    public AudioSource speaker;
    public AudioListener microphone;
    public Camera recordingCamera;
    int maxSeconds;
    int framesPerSecond = 30; //a smooth 30FPS
    int startIndex = 0; //this starts at 0 and loops around with the latest recorded clip. so the thing overwrites itself.

    private int sampleRate = 44100;
    private int audioClipLength = 1; // in seconds. once per second, so every 30 frames
    private List<Texture2D> capturedFrames;
    private List<(float[], float[])> capturedAudioDataStereo;
    public Material m_Render;
    public Material m_black;
    public Renderer screenRenderer;


    byte[][] frames;

    enum replay_state
    {
        Replaying,
        Recording,
        Paused
    }
    replay_state replayState = replay_state.Recording;

    public void ToggleCam()
    {




        switch (replayState)
        {
            case replay_state.Replaying:
                return;
            case replay_state.Recording:
                screenRenderer.material = m_Render;
                break;
            case replay_state.Paused:
                screenRenderer.material = m_black;
                break;
            default:
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //capturedFrames = new List<Texture2D>();
        ////capturedAudioData = new List<(float[], float[])>();

        //if (Microphone.devices.Length > 0)
        //{
        //    string deviceName = Microphone.devices[0];
        //    speaker.clip = Microphone.Start(deviceName, true, audioClipLength, sampleRate);
        //    speaker.loop = true;


        //}
    }


    void Replay()
    {


    }


    void Record()
    {
        if (!recordingCamera.isActiveAndEnabled)
        {

        }
        //GetVideoData

    }

    Texture2D GetVideoData()
    {
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        //switch (replayState)
        //{
        //    case replay_state.Replaying:
        //        Record();

        //        break;
        //    case replay_state.Recording:
        //        Replay();
        //        break;
        //    case replay_state.Paused:
        //        break;
        //    default:
        //        break;
        //}
    }
}
