using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectCamera : MonoBehaviour {
	private Transform target;
	private bool invert = false;
	float currentRollAngle = 0f;
	Vector2 currentMouseLook;
	float sensitivity = 5f;
	Vector2 verticalLookLimits = new Vector2(-70f, 80f);
	Vector2 horizontalLookLimits = new Vector2(-70f, 70f);
	Vector2 lookAngles;
	float rollAngle = 0f;//10f;
	float rollSpeed = 3f;

	// Start is called before the first frame update
	void Start() {
		target = GameObject.FindGameObjectWithTag("Player").transform;//transform.parent;
	}

	// Update is called once per frame
	void Update() {
		currentMouseLook = new Vector2(Input.GetAxis("Mouse Y"),Input.GetAxis("Mouse X"));
		lookAngles.x += currentMouseLook.x * sensitivity * (invert ? 1f : -1f);
		lookAngles.y += currentMouseLook.y * sensitivity;

		lookAngles.x = Mathf.Clamp(lookAngles.x, verticalLookLimits.x, verticalLookLimits.y);
		lookAngles.y = Mathf.Clamp(lookAngles.y, horizontalLookLimits.x, horizontalLookLimits.y);

		currentRollAngle = Mathf.Lerp(currentRollAngle, Input.GetAxisRaw("Mouse X") * rollAngle, Time.deltaTime * rollSpeed);

		transform.localRotation = Quaternion.Euler(lookAngles.x, lookAngles.y, currentRollAngle);
	}
}
