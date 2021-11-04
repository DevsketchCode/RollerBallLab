using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerSoundHolder : MonoBehaviour
{
    // This script would be put on all the assets that would allow you to specify what type of hit sound should be played if that object was hit.
    [Tooltip("Defines which sound profile should be used by the rollerball.  Custom will use the Audio Clip specified below")]
    [SerializeField]
    private RollerBallAudioPlayer.InteractionTypes interactionType = RollerBallAudioPlayer.InteractionTypes.Ground;

    public RollerBallAudioPlayer.InteractionTypes getInteractionType()
    {
        return interactionType;
    }

}
