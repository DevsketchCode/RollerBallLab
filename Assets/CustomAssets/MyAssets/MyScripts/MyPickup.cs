using UnityEngine;

public class MyPickup : MonoBehaviour
{

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private BoxCollider coll;

    [SerializeField]
    private Transform player, toDestination, cam;

    [SerializeField]
    private float dropForwardForce, dropUpwardForce;

    [SerializeField]
    private float throwForwardForce, throwUpwardForce;

    [SerializeField]
    private bool equipped;

    [SerializeField]
    private static bool slotFull;

    private int pickUpRotation;


    private void Start()
    {

        // Setup
        if (!equipped)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;

        }
        if (equipped)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update()
    {
        // Check if player is in range
        Vector3 distanceToPlayer = player.position - transform.position;
        //if (!equipped && distanceToPlayer.magnitude <= pickupRange && Input.GetMouseButtonDown(0) && !slotFull) PickUp();

        // Drop if equipped
        if (equipped && Input.GetMouseButtonUp(0)) Drop();

        // Throw if equipped
        if (equipped && Input.GetMouseButtonDown(1)) ThrowItem();

        // Rotate if equipped and Scrollwheel used rotate the object being held by the x axis. 
        if (equipped && Input.GetAxis("Mouse ScrollWheel") > 0f)
        {

            rb.transform.Rotate(Vector3.right * 10f, Space.Self);
        }
        if (equipped && Input.GetAxis("Mouse ScrollWheel") < 0f)
        {

            rb.transform.Rotate(Vector3.left * 10f, Space.Self);
        }

    }

    public void PickUp(GameObject raycastedObject)
    {
        if(!slotFull && !equipped)
        {
            equipped = true;
            slotFull = true;

            // Make Rigidbody kinematic and BoxCollider a trigger so it doesn't move anymore
            rb.isKinematic = true;
            coll.isTrigger = true;

            // reset the pickUpRotataion each time an item is picked up again.
            pickUpRotation = -30;

            // Offset the object depending on the length of the object (z axis)
            Vector3 offset = new Vector3(0, 0, (raycastedObject.transform.localScale.z / 2f));

            // Make item a child of the camera and move it to default position
            raycastedObject.transform.SetParent(toDestination);
            raycastedObject.transform.localPosition = new Vector3(0, 0, -3) + offset;
            toDestination.transform.localPosition = toDestination.transform.localPosition;

            raycastedObject.transform.localRotation = Quaternion.Euler(pickUpRotation, 0, 0);
        }
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        // Set parent to null
        transform.SetParent(null);

        // Make Rigidbody kinematic and BoxCollider a trigger so it doesn't move anymore
        rb.isKinematic = false;
        coll.isTrigger = false;

        // Match momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //Add force
        rb.AddForce(cam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(cam.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);
    }

    private void ThrowItem()
    {
        equipped = false;
        slotFull = false;

        // Set parent to null
        transform.SetParent(null);

        // Make Rigidbody kinematic and BoxCollider a trigger so it doesn't move anymore
        rb.isKinematic = false;
        coll.isTrigger = false;

        // Match momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //Add force
        rb.AddForce(cam.forward * throwForwardForce, ForceMode.Impulse);
        rb.AddForce(cam.up * throwUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);
    }
}
