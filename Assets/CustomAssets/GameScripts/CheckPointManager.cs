using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{

    // Keeps track of what the last checkpoint was, and resetting the ball back to that last checkpoint when it's triggered.
    // Start is called before the first frame update

    [SerializeField]
    private CheckPointFlag lastFlag = null;

    [SerializeField]
    private bool stopObject = false;

    public CheckPointFlag getLastFlag()
    {
        return lastFlag;
    }

    public void setLastFlag(CheckPointFlag newLocation)
    {
        lastFlag = newLocation;
    }


    public void resetToLastFlag(GameObject teleportMe)
    {
        if (lastFlag != null)
        {
            // reset the object to the checkpoint
            teleportMe.transform.position = lastFlag.GetSpawnPoint().position;

            if (stopObject && teleportMe.GetComponent<Rigidbody>() != null)
            {
                teleportMe.GetComponent<Rigidbody>().velocity = new Vector3();
                teleportMe.GetComponent<Rigidbody>().angularVelocity = new Vector3();
            }

        }
    }

}
