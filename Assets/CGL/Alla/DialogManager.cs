using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;
using FMOD.Studio;
using static UnityEngine.InputManagerEntry;
using FMOD;
using static UnityEngine.ParticleSystem;
using static DialogManager;
using UnityEngine.Events;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;
    public List<EventReference> eventsToUnpause = new List<EventReference>();
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
    public UnityEvent OnspeakingEnded;
    public CharacterAnimation princess;
    public CharacterAnimation knight;
    int i = -1;
    bool playing;
    EventInstance lastInstance;
    public bool pause = true;
    public bool speaking;
    public float pitch = 1f;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;    
        else Destroy(this);
    }
    private void Update()
    {
        if (!FilmingGameManager.instance.startedFilming||pause) return;
        FMOD.Studio.PLAYBACK_STATE state;
        lastInstance.getPlaybackState(out state);
        if (state==FMOD.Studio.PLAYBACK_STATE.STOPPED)
            playing = false;

       

        if (!playing && i < dialogsEvents.Count) 
        {
            if (i < 0)
            {
                PlayNext();
                return;
            }
            else
            {
                if (eventsToUnpause[0].Path == dialogsEvents[i].dialogeEvent.Path)
                {
                  
                    eventsToUnpause.RemoveAt(0);
                    pause = true;
                    knight.ChangeState(CharacterAnimation.anim.Idle);
                    princess.ChangeState(CharacterAnimation.anim.Idle);
                    OnspeakingEnded.Invoke();
                    return;
                }
            }
          
           
            PlayNext(); 
        
        }
    }
    public void PlayNext()
    {
       
        print("next dialog");
        i++;
        if (i >= dialogsEvents.Count) return;
        lastInstance.release();
        Dialog dialog = dialogsEvents[i];
        lastInstance = FMODUnity.RuntimeManager.CreateInstance(dialog.dialogeEvent);
        lastInstance.setPitch(pitch);
        currentSpeaker = dialog.speaker;
        if (dialog.speaker == speaker.knight)
        {
            knightSoundSource.audioSource = lastInstance;
            knightSoundSource.speaking = true;
            princessSoundSource.speaking = false;
            princess.ChangeState(CharacterAnimation.anim.Idle);
            knight.ChangeState(CharacterAnimation.anim.Talk);
        }
        else
        {
            princessSoundSource.audioSource = lastInstance;
            knightSoundSource.speaking = false;
            princessSoundSource.speaking = true;
            knight.ChangeState(CharacterAnimation.anim.Idle);
            princess.ChangeState(CharacterAnimation.anim.Talk);
        }
        lastInstance.start();
        playing = true;
        pause = false;
    }
    public void UnPause()
    {
        pause = false;
       
    }
    public void Pause()
    {
        pause=true; 
    }
    public float GetAccuracyFromCurrentSpeaker()
    {
        return (currentSpeaker == speaker.princess) ? princessSoundSource.lastAudioLevel:knightSoundSource.lastAudioLevel;
    }

}

   
