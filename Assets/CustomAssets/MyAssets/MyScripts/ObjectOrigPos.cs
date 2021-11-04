using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOrigPos : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;
    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3();
            GetComponent<Rigidbody>().angularVelocity = new Vector3();
            transform.rotation = startRot;
            transform.position = startPos;
        } 
            
    }
}
