using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{

    [SerializeField]
    private float popUpForce;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<RollerBallMover>()) // Limit it even more by only interacting with the Rollerball
        {
            if (collision.gameObject.GetComponent<Rigidbody>()) // Do so there isn't an error if something without a RigidBody hits the box, and ignore that object.
            {


                // collision is the parameter passed into the OnCollisionEnter method, to gain access to what hit the box.
                // gameObject is variable of that object that hit the box, and references to the object in the scene.
                // GetComponent() lets us access any component added to a gameObject (script, audio, sound, collider, transform, etc)
                // Rigidbody is the class the roller bal contains.  Allows us to modify it by adding force to shoot the ball up.
                // Add force will apply a push ot the object given a specified direction.
                // Use the box to determine it's position in the world
                Vector3 localRotation = transform.up.normalized; // normalized makes it only care about the vector to be in ranges from 0 to 1,
                                                                 // and strips any scaling or other modifications as that aren't needed.
                                                                 // We're only interested in getting which direction it's pointing.

                // multiply the localRotation by popUpForce and make sure the specify a value in unity around 600-900. 
                collision.gameObject.GetComponent<Rigidbody>().AddForce(localRotation * popUpForce);
            }
        }

    }
}
