using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerLevels : MonoBehaviour
{

    public AudioMixer masterMixer;

    public void BackgroundLvl(float bgLvl)
    {
        masterMixer.SetFloat("BackgroundMusic", bgLvl);
    }
    public void SetAmbientLvl(float ambientLvl)
    {
        masterMixer.SetFloat("AmbientSounds", ambientLvl);
    }

    public void SetSfxLvl(float sfxLvl)
    {
        masterMixer.SetFloat("SoundEffects", sfxLvl);
    }


}
