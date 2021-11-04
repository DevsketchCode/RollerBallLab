using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGlue : MonoBehaviour
{
	[Tooltip("Trigger or Collision activated")]
	[SerializeField]
	private bool triggerActivated;

	// gameObject to move
	private GameObject roller;
	// difference in position between platform and roller
	private Vector3 offsetPos;
	// difference in rotation of platform from previous frame to this frame
	private Vector3 offsetRot;
	// Rotation of platform at previous frame
	private Vector3 prevRot;

	private void Start()
	{
		roller = null;
	}

	private void startTracking()
	{
		// pick up current rotation
		prevRot = transform.rotation.eulerAngles;
	}

	private void trackMovement(GameObject other)
	{
		//Debug.Log(other.gameObject.name);
		// Don't need if statement if you want any object to move with
		if (other.GetComponent<RollerBallMover>())
		{
			roller = other;
			offsetPos = roller.transform.position - transform.position;
			// difference between previous rotation value and current value
			offsetRot = transform.rotation.eulerAngles - prevRot;
			//Debug.Log(offsetRot);
			// Update previous for next frames checks, only need difference per frame
			prevRot = transform.rotation.eulerAngles;
		}
	}

	private void endTracking()
	{
		roller = null;
	}

	private void LateUpdate()
	{
		if (roller != null)
		{
			// If platform has moved adding the offset will maintain the same relative location
			roller.transform.position = transform.position + offsetPos;
			// If the platform rotated a new position is calculated based on the rotation per frame and distance from center of platform
			roller.transform.position = rotatePointAroundPivot(roller.transform.position, transform.position, offsetRot);
		}
	}

	private Vector3 rotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
	{
		// Find and remove the difference in location, rotate direction, then offset back to relative distance
		return Quaternion.Euler(angles) * (point - pivot) + pivot;
	}

	private void OnTriggerStay(Collider other)
	{
		if (triggerActivated)
		{
			trackMovement(other.transform.root.gameObject);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (triggerActivated)
		{
			endTracking();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!triggerActivated)
		{
			startTracking();
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		if (!triggerActivated)
		{
			trackMovement(collision.collider.transform.root.gameObject);
		}
	}


	private void OnCollisionExit(Collision collision)
	{
		if (!triggerActivated)
		{
			endTracking();
		}
	}


	/* Old version for reference, works but some improvement needed.
	[Tooltip ("Trigger or Collision activated")]
	[SerializeField]
	private bool triggerActivated;

	private GameObject positionTracker;

	private void startTracking(Collider other)
	{
		if (other.gameObject.GetComponent<RollerBallMover>())
		{
			positionTracker = new GameObject();
			positionTracker.transform.position = transform.position;
			positionTracker.transform.rotation = transform.rotation;
			other.gameObject.transform.parent = positionTracker.transform;
		}
	}

	private void endTracking(Collider other)
	{
		if (other.gameObject.GetComponent<RollerBallMover>())
		{
			other.gameObject.transform.parent = null;
			Destroy(positionTracker, 0.25f);
			positionTracker = null;
		}
	}

	private void Update()
	{
		if (positionTracker != null)
		{
			positionTracker.transform.position = transform.position;
			positionTracker.transform.rotation = transform.rotation;
		}

	}

	private void OnTriggerEnter(Collider other)
	{
		if (triggerActivated)
		{
			startTracking(other);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (triggerActivated)
		{
			endTracking(other);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!triggerActivated)
		{
			startTracking(collision.collider);
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (!triggerActivated)
		{
			endTracking(collision.collider);
		}
	}*/
}
