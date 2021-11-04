using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinXYZ : MonoBehaviour
{

	[SerializeField]
	private Vector3 spinSpeed;

	// Update is called once per frame
	void Update()
	{
		if (spinSpeed.magnitude > 0)
		{
			transform.Rotate(spinSpeed.x * Time.deltaTime, spinSpeed.y * Time.deltaTime, spinSpeed.z * Time.deltaTime);
		}
	}
}
