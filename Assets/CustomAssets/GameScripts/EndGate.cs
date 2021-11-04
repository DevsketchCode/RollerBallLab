using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGate : MonoBehaviour {

	[SerializeField]
	private Timer timer = null;

	[SerializeField]
	private Transform captureTarget = null;

	[SerializeField]
	private float captureForce = 0;

	[SerializeField]
	private float floatForce = 0;

	// GUI updates
	[Header("GUI References")]
	[SerializeField]
	private RectTransform HUDTimerPanel = null;

	[SerializeField]
	private RectTransform winPanel = null;
	[SerializeField]
	private Text winTimeText = null;

	private GameObject rollerT;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<RollerBallMover>())
		{
			// Save roller gameObject for reference
			rollerT = other.gameObject;
		} else if (other.gameObject.transform.root.GetComponent<RollerBallMover>())
		{
			rollerT = other.gameObject.transform.root.GetComponent<RollerBallMover>().gameObject;
		}

		if (rollerT != null) { 
			timer.timerStop();

			// Disable input controls
			rollerT.GetComponent<RollerBallMover>().setMaxMoveForce(0.1f);
			Rigidbody rb = rollerT.GetComponent<Rigidbody>();
			rb.angularDrag = 5.0f;
			rb.mass = 1.0f;
			rollerT.GetComponent<Rigidbody>().useGravity = false;
			rb.velocity = new Vector3();
			// Start routine to capture roller to end gate position
			StartCoroutine("captureRoller");

			// Disable trigger
			GetComponent<SphereCollider>().enabled = false;

			// Update GUI
			showWinPanel();

			// Save Level Stats
			// Add here
		}
	}

	private IEnumerator captureRoller()
	{

		// Move target towards the center point of end gate
		while (Vector3.Distance(rollerT.transform.position, captureTarget.position) > 0.1f)
		{
			// Move roller towards target
			rollerT.transform.position = Vector3.MoveTowards(rollerT.transform.position, captureTarget.position, captureForce * Time.deltaTime);
			yield return new WaitForSeconds(0.01f);
		}
		rollerT.GetComponent<Rigidbody>().useGravity = true;

		// Lock both x and z movements
		rollerT.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

		// Float the roller until user ends game
		while (true)
		{
			if (rollerT.transform.position.y - captureTarget.position.y < 1.0f)
			{
				rollerT.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, floatForce, 0.0f));
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	private void showWinPanel()
	{
		if (!Cursor.visible)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		if (HUDTimerPanel != null) HUDTimerPanel.gameObject.SetActive(false);
		winPanel.gameObject.SetActive(true);

		// Show time, format mm : ss.mmm
		float roundedTime = Mathf.Round(timer.getTimerTime() * 1000) / 1000.0f;
		if (roundedTime > 60)
		{
			int minutes = Mathf.FloorToInt(roundedTime / 60.0f);
			float seconds = roundedTime - (minutes * 60);
			winTimeText.text = string.Format("{0:00}", minutes) + ":" + string.Format("{0:00}", (Mathf.RoundToInt(seconds)));
		} else
		{
			winTimeText.text = "00:" + string.Format("{0:00}", roundedTime);
		}
		
	}
}
