using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeStackReaction : MonoBehaviour
{

    [SerializeField]
    private string objectTagName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(objectTagName))
        {
            transform.GetComponent<Renderer>().material.color = Color.red;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        transform.GetComponent<Renderer>().material.color = Color.white;
    }
}
