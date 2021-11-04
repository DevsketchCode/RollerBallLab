using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {

	[SerializeField]
	private TMPro.TextMeshProUGUI textTimeCounter;

	private float runningTime;
	private bool run;


	// Use this for initialization
	void Start () {

		runningTime = 0;
		run = false;

	}
	
	// Update is called once per frame
	void Update () {
		if(run)
        {
			runningTime += Time.deltaTime;
			//textTimeCounter.text = runningTime.ToString("f0");
		}
	}

	public void timerStart()
	{
		run = true;
	}

	public void timerStop()
	{
		run = false;
	}

	public void timerReset()
	{
		runningTime = 0;
	}

	public float getTimerTime()
	{
		return runningTime;
	}

	public bool isRunning()
	{
		return run;
	}
}
