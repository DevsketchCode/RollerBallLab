using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class RollerBallAudioPlayer : MonoBehaviour
{

    public enum InteractionTypes { Concrete, Glass, Ground, Metal, Rocks, Sand, Water, Wood, Custom };

    [Tooltip("Array size should be 5, to match the Interaction Types enumeration")]
    [SerializeField]
    private AudioClip[] impactAudioClips = null; // clips for possible impact sounds (depending on the surface)

    [Tooltip("Array size should be 5, to match the Interaction Types enumeration")]
    [SerializeField]
    private AudioClip[] rollAudioClips = null; // clips for possible roll sounds (depending on the surface)

    [SerializeField]
    private float lowSpeedMagnitude = 0; // volume when slow

    [SerializeField]
    private float highSpeedMagnitude = 0; // volume when fast

    private AudioSource audioImpactPlayer; // the sound source for impact sounds
    private AudioSource audioRollPlayer; // the sound source for rolling sound
    private RollerSoundHolder defaultFX; // Default FX Sound (can be used for majority of objects, then only specify the different ones in code.
    private Rigidbody rb;

    private void Start()
    {
        AudioSource[] audioPlayers = GetComponents<AudioSource>(); // gets all the sources, if the object has more than one audio source
        // verify that both audio components have been found
        if (audioPlayers.Length > 1)
        {
            audioImpactPlayer = audioPlayers[0];
            audioImpactPlayer.playOnAwake = false; // makes sure the sound doesn't play on its own.
            audioImpactPlayer.loop = false;

            audioRollPlayer = audioPlayers[1];
            audioRollPlayer.playOnAwake = false; // makes sure the sound doesn't play on its own.
            audioRollPlayer.loop = true;
        }
        else
        {
            Debug.Log("Audio Source for the roller ball is not setup correctly.  Only 1 or 0 audio sources found, expected to find more than one.");
        }

        if (highSpeedMagnitude <= lowSpeedMagnitude) // Make sure the 2 fields are in correct order
        {
            highSpeedMagnitude = lowSpeedMagnitude + 1f;
        }

        rb = GetComponent<Rigidbody>();
        defaultFX = GetComponent<RollerSoundHolder>();
    }

    private RollerSoundHolder findSounds(Collision collision)
    {

        if (collision.gameObject.GetComponent<RollerSoundHolder>() != null)
        {
            // found script on the child object
            return collision.gameObject.GetComponent<RollerSoundHolder>();
        }
        else if (collision.gameObject.GetComponentInParent<RollerSoundHolder>() != null)
        {
            // found script on the parent object
            return collision.gameObject.GetComponentInParent<RollerSoundHolder>();
        }

        return defaultFX;
    }

    private AudioClip selectClipByType(InteractionTypes interType, AudioClip[] clipList)
    {
        // Get the audio clip list
        return clipList[(int)interType]; 
    }

    private AudioClip selectImpactClipByType(RollerSoundHolder soundHolder)
    {
        return selectClipByType(soundHolder.getInteractionType(), impactAudioClips);
    }

    private AudioClip selectRollClipByType(RollerSoundHolder soundHolder)
    {
        return selectClipByType(soundHolder.getInteractionType(), rollAudioClips);
    }

    private float calcAudioVolume(float magnitude)
    {
        return (magnitude - lowSpeedMagnitude) / (highSpeedMagnitude - lowSpeedMagnitude);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Two objects must first make contact, play the impact sound
        if (!this.enabled) return; // check if the object is active, only play the sound clips on active objects.

        // Play the impact sound
        audioImpactPlayer.PlayOneShot(selectImpactClipByType(findSounds(collision)), calcAudioVolume(collision.impulse.magnitude * 4)); // impulse tells us how much force was needed to keep them from going through each other.
        // multiply by 4 making the impuls values to be boosted a little  bit since the volume was quite low.

    }

    private void OnCollisionStay(Collision collision)
    {
        // Called multiple times, like Update()
        // play rolling sounds
        if (!this.enabled) return;

        audioRollPlayer.volume = calcAudioVolume(rb.velocity.magnitude); // magnitude is all of the values togetehr, so that you can tell the overall speed in any direction.

        if (!audioRollPlayer.isPlaying)
        {
            audioRollPlayer.clip = selectRollClipByType(findSounds(collision));
            audioRollPlayer.Play();
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        // No more contact, stop playing the rolling sound
        if (!this.enabled) return; // check if object is active, only play the sound clips when active

        audioRollPlayer.Stop();
    }

}
