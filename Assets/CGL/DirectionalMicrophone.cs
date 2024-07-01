using System.Collections.Generic;
using UnityEngine;

public class DirectionalMicrophone : MonoBehaviour
{
    public List<Transform> targets; // The target GameObject (e.g., the player or the sound source)
    public float maxVolumeDistance = 10f; // Maximum distance at which the mic will pick up sound
    public float angleFalloff = 45f; // Angle (in degrees) within which the mic picks up maximum sound

    private AudioSource audioSource;
    private Transform micTransform;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        micTransform = transform;
    }

   void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Listenable") return;
        targets.Add(other.transform);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Listenable") return;
        targets.Remove(other.transform);


    }


    void Update()
    {
        if (targets.Count<1) return;

        // Calculate distance to target
        float distance = Vector3.Distance(micTransform.position, target.position);
        // Calculate direction to target
        Vector3 directionToTarget = (target.position - micTransform.position).normalized;
        // Calculate angle between mic forward direction and target direction
        float angle = Vector3.Angle(micTransform.forward, directionToTarget);

        // Calculate volume based on distance and angle
        float volume = CalculateVolume(distance, angle);

        // Set the volume of the audio source
        audioSource.volume = volume;
    }

    float CalculateVolume(float distance, float angle)
    {
        // Linear falloff based on distance
        float distanceFactor = Mathf.Clamp01(1 - (distance / maxVolumeDistance));

        // Angle falloff
        float angleFactor = Mathf.Clamp01(1 - (angle / angleFalloff));

        // Combine factors (you can tweak the combination formula as needed)
        return distanceFactor * angleFactor;
    }
}
