using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailFollower : MonoBehaviour {

	RequireComponent TrailRenderer;

	private RollerBallMover targetObject;

	// Use this for initialization
	void Start () {
		StartCoroutine("EnableTrail");
	}
	
	// Update is called once per frame
	void Update () {
		if (targetObject == null || !targetObject.enabled)
		{
			findTarget();
		}
		transform.position = targetObject.transform.position;
	}

	IEnumerator EnableTrail()
	{
		yield return new WaitForSeconds(1f);
		GetComponent<TrailRenderer>().emitting = true;
	}

	private void findTarget()
	{
		targetObject = null;
		RollerBallMover[] rollers = FindObjectsOfType<RollerBallMover>();
		if (rollers != null && rollers.Length > 0)
		{
			for (int i = 0; i < rollers.Length; i++)
			{
				if (rollers[i].enabled)
				{
					targetObject = rollers[i];
					break;
				}
			}
		}
	}
}
