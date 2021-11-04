using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTriggerZone : MonoBehaviour
{
    [SerializeField]
    private CheckPointManager playerData = null;

    [SerializeField]
    private float respawnDelay = 0;

    private float respawnTimer;

    private void OnTriggerEnter(Collider other)
    {
        // Only if you want immediate respawn
        //playerData.resetToLastFlag(other.gameObject);
        respawnTimer = respawnDelay;

    }

    private void OnTriggerStay(Collider other)
    {
        if (respawnTimer <= 0)
        {
            // Respawn only the player the the checkpoint.
            if (other.CompareTag("Player"))
            {
                playerData.resetToLastFlag(other.gameObject);
            }
        }
        else
        {
            respawnTimer -= Time.deltaTime;
        }


    }
}
