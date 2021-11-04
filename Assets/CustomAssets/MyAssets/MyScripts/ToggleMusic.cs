using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ToggleMusic : MonoBehaviour
{
    [SerializeField]
    Material playMaterial;

    [SerializeField]
    Material pauseMaterial;

    [SerializeField]
    Material daySkyBox;

    [SerializeField]
    Material nightSkyBox;

    [SerializeField]
    private bool toggleDayNight = false;

    private AudioSource musicAudio = null;
    private void OnTriggerEnter(Collider other)
    {

        musicAudio = this.gameObject.GetComponent<AudioSource>();
        if (musicAudio != null && musicAudio.isPlaying)
        {
            musicAudio.Pause();

            // Change musicbox material
            this.gameObject.GetComponent<MeshRenderer>().material = playMaterial;

            if(toggleDayNight)
            {
                // change Daytime Settings
                ToggleDayTime(true);
            }
        }
        else
        {
            musicAudio.Play();

            // Change musicbox material
            this.gameObject.GetComponent<MeshRenderer>().material = pauseMaterial;

            if (toggleDayNight)
            {
                // Change to Nightime settings
                ToggleDayTime(false);
            }
        }
    }

    private void ToggleDayTime(bool daytime)
    {
        if (daytime)
        {
            // Set skybox to daytime
            RenderSettings.skybox = daySkyBox;

            // Make it light again
            GameObject.Find("Sun Light").GetComponent<Light>().intensity = 1.3f;
            RenderSettings.ambientIntensity = 1.0f;
            RenderSettings.reflectionIntensity = 1.0f;


            // turn off lights
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<Light>() != null)
                {
                    transform.GetChild(i).gameObject.SetActive(false); // turn all the children lights off
                }
            }
            RenderSettings.fogColor = new Color32(128, 128, 128, 255); // light grey fog

        }
        else
        {
            // Set skybox to nightime
            RenderSettings.skybox = nightSkyBox;

            // turn on lights
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<Light>() != null)
                {
                    transform.GetChild(i).gameObject.SetActive(true); // turn all the children lights on
                }
            }
            
            RenderSettings.fogColor = new Color32(37, 37, 37, 255); // dark grey fog
        }
    }
}
