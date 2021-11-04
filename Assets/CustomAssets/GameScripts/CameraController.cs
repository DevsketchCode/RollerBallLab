using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraController : MonoBehaviour {

	[SerializeField]
	private float followRadius = 0;

	[SerializeField]
	private float followHeight = 0;

    [SerializeField]
    private float aimHeightOffset = 0;

    [SerializeField]
	private float turnSpeedHorz = 0;

	[SerializeField]
	private float turnSpeedVert = 0;

	[Tooltip("Speed to reset camera back to target")]
	[SerializeField]
	private float lerpSpeed = 0;

    private RollerBallMover target;
    private Vector3 heightOffset;
	private Vector3 offset;
    private float camRayCastOffset;
    private RaycastHit camRayHit;
    private Vector3 camRayHitPoint;
	
	// Use this for initialization
	void Start () {
		if (target == null)
		{
            findTarget();
		}

		transform.position = target.transform.position;
		transform.rotation = target.transform.rotation;

		transform.Translate(0.0f, followHeight, - followRadius);

		offset = transform.position - target.transform.position;
        camRayCastOffset = 1;

    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (Cursor.visible) return;
        if (!target.enabled)
        {
            findTarget();
        }
        // Update offset based on mouse movements, horizontal only in this step
        offset = Quaternion.AngleAxis(Input.GetAxis("CamSideways") * turnSpeedHorz, Vector3.up) * offset;
		//Debug.Log(Input.GetAxis("Mouse X"));

		// Update offset based on verticle mouse movements
		heightOffset.y += Input.GetAxis("CamUpDown") * turnSpeedVert;
		heightOffset.y = Mathf.Clamp(heightOffset.y, -4, 4);
		heightOffset.y = Mathf.Lerp(heightOffset.y, aimHeightOffset, lerpSpeed * Time.deltaTime);

		// Relocate target, as it may have moved, and apply offset to maintain follow distances
		transform.position = target.transform.position + offset;

        // Check for colliding objects and shift camera forward to avoid
        //Debug.DrawLine(target.transform.position, transform.position);
        if (Physics.Linecast(target.transform.position, transform.position,
            out camRayHit, ~(1 << 11),  QueryTriggerInteraction.Ignore))
        {
            //Debug.Log("Dist to Hit: " + camRayHit.distance);
            //Debug.DrawLine(target.transform.position, camRayHit.point, Color.red, 0.25f);
            camRayHitPoint = camRayHit.point;
            camRayHitPoint.y = (transform.position.y + camRayHitPoint.y)/2f;
            camRayCastOffset = Vector3.Distance(target.transform.position, camRayHitPoint) / Vector3.Distance(target.transform.position, transform.position);
            //Debug.Log("% Dist: " + camRayCastOffset);
            transform.position = camRayHitPoint;
        } else if (camRayCastOffset < 1f)
        {
            camRayHitPoint = Vector3.Lerp(target.transform.position, transform.position, camRayCastOffset);
            camRayHitPoint.y = transform.position.y;
            transform.position = camRayHitPoint;
            camRayCastOffset += .5f * Time.deltaTime;
            //Debug.Log("% Dist: " + camRayCastOffset);
        }

        // Aim camera back at the target to keep it centered on screen
        transform.LookAt(target.transform.position + heightOffset);
	}

    private void findTarget()
    {
        target = null;
        RollerBallMover[] rollers = FindObjectsOfType<RollerBallMover>();
        if (rollers != null && rollers.Length > 0)
        {
            for (int i = 0; i < rollers.Length; i++)
            {
                if (rollers[i].enabled)
                {
                    rollers[i].setCamera(GetComponent<Camera>());
                    target = rollers[i];
                    break;
                }
            }
        }
    }
}
