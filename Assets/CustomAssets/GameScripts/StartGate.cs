using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGate : MonoBehaviour
{
	[SerializeField]
	private Timer timer = null;

	[SerializeField]
	private Transform startTarget = null;

	private GameObject rollerBall;

	void Start()
	{
		if (rollerBall == null)
		{
			RollerBallMover[] rollers = FindObjectsOfType<RollerBallMover>();
			if (rollers != null && rollers.Length > 0)
			{
				for (int i = 0; i < rollers.Length; i++)
				{
					if (rollers[i].enabled)
					{
						rollerBall = rollers[i].gameObject;
					}
					else if (rollerBall != null)
					{
                        rollers[i].enabled = false;
					}
				}
			}
		}

		// Align roller ball with start gate postion
		rollerBall.transform.position = startTarget.position;
		rollerBall.transform.rotation = startTarget.rotation;
	}

	private void OnTriggerExit(Collider other)
	{
		timer.timerStart();
	}
}
