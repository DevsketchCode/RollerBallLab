using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointFlag : MonoBehaviour
{

    [SerializeField]
    private bool checkPointEnabled = false;

    [SerializeField]
    private Transform spawnPoint = null;

    [SerializeField]
    private CheckPointManager playerData = null; // Access to the player data (need to add this CheckPointManager script to the Player data object

    private void OnTriggerEnter(Collider other)
    {
        // The player has reached the checkpoint flag
        if (!checkPointEnabled && other.CompareTag("Player"))
        {
            checkPointEnabled = true;

            // Turn on any visual ques
            SpinXYZ spinScript = gameObject.GetComponentInChildren<SpinXYZ>();
            // gameObject is the same thing as "this" in other programming languages.

            if (spinScript != null) // make sure the spinScript exists there
            {
                spinScript.enabled = true;
            }


            PingPongXYZ moveScript = gameObject.GetComponentInChildren<PingPongXYZ>();
            if (moveScript != null)
            {
                moveScript.enabled = true;
            }


            ParticleSystem particles = gameObject.GetComponentInChildren<ParticleSystem>();
            if (particles != null)
            {
                particles.Play();
            }

            // Place setLastFlag here if checkpoint can only be reached once. 
            //playerData.setLastFlag(this);

        }

        // Place setLastFlag here to allow repeat checkpoints at this site. 
        playerData.setLastFlag(this);
    }

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }


}
