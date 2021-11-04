using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointPortal : MonoBehaviour
{
	// Link to the player data to let it know that the player is out of bounds and should be reset
    [SerializeField]
    private CheckPointManager playerData;

	// Forward the message to the player data to reset player
    private void OnTriggerEnter(Collider other)
    {
        playerData.resetToLastFlag(other.gameObject);
    }
}
