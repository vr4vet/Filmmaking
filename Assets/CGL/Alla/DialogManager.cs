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
    int i = -1;
    bool playing;
    EventInstance lastInstance;
    private void Update()
    {
        FMOD.Studio.PLAYBACK_STATE state;
        lastInstance.getPlaybackState(out state);
        if (state==FMOD.Studio.PLAYBACK_STATE.STOPPED)
            playing = false;
        if (!playing) { PlayNext(); }
    }
    public void PlayNext()
    {
        print("next");
        i++;
        lastInstance.release();
        Dialog dialog = dialogsEvents[i];
        lastInstance = FMODUnity.RuntimeManager.CreateInstance(dialog.dialogeEvent);
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

}

   
