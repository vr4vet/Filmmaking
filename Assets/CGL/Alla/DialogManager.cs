using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;
using FMOD.Studio;
using static UnityEngine.InputManagerEntry;
using FMOD;
using static UnityEngine.ParticleSystem;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;
    public enum speaker
    {
        knight, princess
    }
    [Serializable]
    public class Dialog
    {
        public EventReference dialogeEvent;
        public speaker speaker;
    }

    public List<Dialog> dialogsEvents;
    public SoundSource knightSoundSource;
    public SoundSource princessSoundSource;
    public speaker currentSpeaker;


    int i = -1;
    bool playing;
    EventInstance lastInstance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;    
        else Destroy(this);
    }
    private void Update()
    {
        FMOD.Studio.PLAYBACK_STATE state;
        lastInstance.getPlaybackState(out state);
        if (state==FMOD.Studio.PLAYBACK_STATE.STOPPED)
            playing = false;
        if (!playing&& i < dialogsEvents.Count) { PlayNext(); }
    }
    public void PlayNext()
    {
        print("next dialog");
        i++;
        if (i >= dialogsEvents.Count) return;
        lastInstance.release();
        Dialog dialog = dialogsEvents[i];
        lastInstance = FMODUnity.RuntimeManager.CreateInstance(dialog.dialogeEvent);
        currentSpeaker = dialog.speaker;
        if (dialog.speaker == speaker.knight)
        {
            knightSoundSource.audioSource = lastInstance;
        }
        else
        {
            princessSoundSource.audioSource = lastInstance;
        }
        lastInstance.start();
        playing = true;
    }

    public float GetAccuracyFromCurrentSpeaker()
    {
        return (currentSpeaker == speaker.princess) ? princessSoundSource.lastAudioLevel:knightSoundSource.lastAudioLevel;
    }

}

   
