using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonFX : MonoBehaviour
{
    [SerializeField]
    AudioClip hoverSound;

    [SerializeField]
    AudioClip clickSound;

    [SerializeField]
    AudioClip playLevelSound;

    private AudioSource myFx { get { return GetComponent<AudioSource>(); } }

    public void onHoverSound()
    {
        myFx.PlayOneShot(hoverSound);
    }

    public void onClickSound()
    {
        myFx.PlayOneShot(clickSound);
    }

    public void onPlayLevelSound()
    {
        myFx.PlayOneShot(playLevelSound);
    }


}
