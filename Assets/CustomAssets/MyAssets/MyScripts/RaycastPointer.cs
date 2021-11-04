using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastPointer : MonoBehaviour
{

    //ray cast is basically a laser looking at a direction and can determine if an object is ahead.
    private GameObject raycastedObj;

    [SerializeField]
    private int rayLength = 10;

    [SerializeField]
    private LayerMask layerMaskInteract; // able to specify a layer on specific objects

    [SerializeField]
    private Image uiCrosshair;

    float zRotation = 90;

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
        {
            if (hit.collider.CompareTag("InteractingObject")) // look in the layer for a specific tag
            {
                // Change crosshair color to active
                CrosshairActive();

                raycastedObj = hit.collider.gameObject; // assign that object to the raycasted object

                if (Input.GetMouseButtonDown(0))
                {
                    raycastedObj.GetComponent<MyPickup>().PickUp(raycastedObj.gameObject);
                }
            }
            else if(hit.collider.CompareTag("CodeCylinder"))
            {
                // Change crosshair color to active
                CrosshairActive();
                raycastedObj = hit.collider.gameObject; // assign that object to the raycasted object
                if (Input.GetMouseButtonUp(0))
                {
                    raycastedObj.transform.Rotate(new Vector3(0,0,-72f));
                    ValidateCode validated = new ValidateCode();
                    validated.CheckSingleCode(raycastedObj);
                }

            } 
            else
            {
                // set crosshair color normal
                CrosshairNormal();
            }
        }
        else
        {
            uiCrosshair.color = Color.white;
        }
    }

    void CrosshairActive()
    {
        uiCrosshair.color = Color.red;
    }

    void CrosshairNormal()
    {
        uiCrosshair.color = Color.white;
    }

}
