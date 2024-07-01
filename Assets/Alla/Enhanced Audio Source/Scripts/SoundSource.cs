// Enhanced Audio Source
// Copyright (C) 2022 Nick Harrison

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

[ExecuteInEditMode]
public class SoundSource : MonoBehaviour
{
    public AudioClip audioClip;
    [Tooltip("Set whether a sound should play through an Audio Mixer first or directly to the Audio Listener")]
    public AudioMixerGroup output;
    [Tooltip("Add the Audio Listener here to enable directional audio")]
    public SoundListener listener;

    [Range(0, 1)]
    [Tooltip("0 for 2D sound, 1 for 3D sound")]
    public float spatialBlend = 1f;
    [Range(0, 180)]
    [Tooltip("Only applicable for 3D sound. 0 for point source sound, 180 Enveloping sound (e.g. no panning when character turns away from audiosource)")]
    public float pointSourceOrEnveloping = 0f;

    public bool mute = false;
    [Tooltip("Play the sound when the scene loads")]
    public bool playOnStart = true;
    [Tooltip("Loop the audio clip")]
    public bool loop = false;

    [Range(0, 1)]
    [Tooltip("Volume of the sound, also represented in the visual transparency of the audio sphere during editing")]
    public float volume = 1;
    [Range(-3, 5)]
    [Tooltip("Pitch of 1 plays at normal speed, 2 is double speed, 0 stops playback speed, -1 reverses playback.")]
    public float pitch = 1;
    [Range(0, 5)]
    [Tooltip("Amount pitched changes dependant on the relative velocity between the Sound Source and Listener")]
    public float doppler = 0;

            [Space(20)]
    
    [Range(0, 98)]
    [Tooltip("Max distance sets the point that audio will stop attenuating")]
    public float maxDistance = 1;
    [Range(0, 50)]
    [Tooltip("Inside the min distance audio will be loudest, outside of the min distance the audio will attenuate till the max distance")]
    public float minDistance = 10;

    [Range(0, 360)]
    [Tooltip("The angle in which the sound will have no filtering")]
    public float onAxisAngle;
    [Range(0, 360)]
    [Tooltip("The angle in which the sound will have maximum low pass filtering")]
    public float offAxisAngle;

    [Tooltip("View the horizontal perspective of the on & off Axis angles. Viewing horizontal or vertical does not affect the audio at all")]
    public bool viewHorizontal = true;
    [Tooltip("View the vertical perspective of the on & off Axis angles. Viewing horizontal or vertical does not affect the audio at all")]
    public bool viewVertical = false;

    [HideInInspector]
    public bool drawLine = false;
    [Tooltip("If listener is within audible distance of this sound source, draw a line to the Listener")]
    public bool drawLineToListener = true;

            [Space(20)]

    [Range(0, 1f)]
    [Tooltip("The highest frequency (in Hz) when the player is off axis to the AudioSource (e.g. the red off axis area)")]
    public float offAxisVolume = 0f;
    [Tooltip("The frequency curve between on axis and off axis")]
    [SerializeField] public AnimationCurve onAxisToOffAxisCurve;
    [Tooltip("")]
    [SerializeField] public AnimationCurve distanceToVolumeCurve;
    [HideInInspector]
    public AudioSource audioSource;
    [HideInInspector]
    public AudioLowPassFilter lowPassFilter;
    [Tooltip("Angle in degrees between the player and the Sound Source")]
    public float angleToPlayer;

  
    [Range(0, 10)]
    [Tooltip("Thickness of the lines, for your viewing comfort - only available in Unity 2020.2 and newer")]
    public float lineThickness = 1f;
    [Range(0, 1)]
    [Tooltip("Make standard spherical gizmo & handles visible")]
    public float handlesVisibility = 1f;

    float angle;
    float angleToFreq = 20000f;
    float lpfFreqMulti = 80f;
    float evaluatedVolume;
    float newAngle;

    Vector3 vectorAngle;
    Vector3 trackAngle;
    Vector3 targetDir;
    [Range(0, 0.35f)]
    [Tooltip("Color transparency for the on & off axis indicators (green and red sections)")]
    public float OnOffAxisFill = 0.1f;
    [Range(0, 0.6f)]
    [Tooltip("Color transparency for the maximum attenuation range)")]
    public float maxSphereFill = 0.1f;
    public Color colorMaxRadius = Color.white;
    [Range(0, 0.6f)]
    [Tooltip("Color transparency for the minimum attenuation range)")]
    public float minSphereFill = 0.1f;
    public Color colorMinRadius = Color.yellow;

    [HideInInspector]
    public Color depthBehindColor = Color.grey;
    [HideInInspector]
    public Color colorMinRadiusFill = Color.yellow;
    [HideInInspector]
    public Color colorMaxRadiusFill = Color.white;
    [HideInInspector]
    public Color colorOnAxisFill = Color.green;
    [HideInInspector]
    public Color colorOffAxisFill = Color.red;
    [HideInInspector]
    public Color colorMinWireSphere = Color.white;
    [HideInInspector]
    public Color colorMaxWireSphere = Color.white;
    [HideInInspector]
    public Color onAxisColor = Color.green;
    [HideInInspector]
    public Color offAxisColor = Color.red;


    private void Awake(){

        audioSource = GetComponentInChildren<>();
        audioSource.clip = audioClip;
        lowPassFilter = GetComponentInChildren<AudioLowPassFilter>();
    }

    private void Start(){
        audioSource.outputAudioMixerGroup = output;
        audioSource.spatialBlend = spatialBlend;
        audioSource.mute = mute;
        audioSource.loop = loop;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.dopplerLevel = doppler;
        audioSource.minDistance = minDistance;
        audioSource.maxDistance = maxDistance;
    }

    public void Update(){

        audioSource.playOnAwake = playOnStart;
        audioSource.spatialBlend = spatialBlend;
        audioSource.spread = pointSourceOrEnveloping;
        audioSource.mute = mute;
        audioSource.volume = volume;
        audioSource.minDistance = minDistance;
        audioSource.maxDistance = maxDistance;
        Vector3 directionToListener = transform.position - listener.transform.position ;
        float angel = Vector3.Angle(directionToListener, listener.transform.forward);
        float distance = Vector3.Distance(transform.position, listener.transform.position);
        if (listener)
        {

            float distToPlayer = Vector3.Distance(listener.transform.position, transform.position);

              if (distToPlayer <= maxDistance){
                TrackAngle();
            } else 
            drawLine = false;
        }

        if (angle >= 180 && angle < 361){
            newAngle = angle - 360f;
            angleToPlayer = Mathf.Abs(-newAngle);
        }

        if (angleToPlayer >= 0 && angleToPlayer <= 180){

           
            //angleToFreq = angleToPlayer * lpfFreqMulti;
            audioSource.volume= evaluatedVolume * Mathf.Clamp(1 - (angel / listener.angel), 0 , 1 ) * distanceToVolumeCurve.Evaluate(Mathf.Clamp(1 - (distance / listener.distance), 0, 1));
        }
   
        evaluatedVolume = onAxisToOffAxisCurve.Evaluate(angleToPlayer );
        Keyframe[] keyframes = onAxisToOffAxisCurve.keys;
        keyframes[0].time = onAxisAngle / 2;
        keyframes[0].value = 1;
        keyframes[1].time = offAxisAngle / 2;
        keyframes[1].value = offAxisVolume;
        keyframes[1].weightedMode = WeightedMode.Both;
        onAxisToOffAxisCurve.keys = keyframes;
    }


    public Vector3 Direction(float angleInDegrees){
        Vector3 yAxis = new Vector3(-Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        return yAxis;
    }

    public Vector3 DirectionVert(float angleInDegrees)
    {
        Vector3 xAxis = new Vector3(0, -Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        return xAxis;
    }

    void TrackAngle(){
        Vector3 targetDir = listener.transform.position - transform.position;
        angle = Vector3.Angle(targetDir, transform.forward);
        angle = Mathf.Round(angle);
        angleToPlayer = angle;

        if (drawLineToListener == true)
            drawLine = true;
        else
            drawLine = false;
    }
}