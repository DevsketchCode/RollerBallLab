using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    private void OnCollisionExit(Collision collision)
    {
        // gets reference to ourself, the game object that we're on
        //gameObject.GetComponent<Renderer>().material.color = new Color(0.8f, 0.3f, 0.3f); // change color to a red shade

        gameObject.GetComponent<Renderer>().material.color = new Color(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f)); // random range for red, green, blue
    }
}
