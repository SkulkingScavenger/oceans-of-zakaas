using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

	private Transform target;
	private bool invert = false;
	bool canUnlock = false;
	float sensitivity = 5f;

	int smoothSteps = 10;
	float smoothWeight = 0.4f;

	float rollAngle = 0f;//10f;
	float rollSpeed = 3f;
	float currentRollAngle = 0f;

	Vector2 verticalLookLimits = new Vector2(-70f, 80f);
	Vector2 horizontalLookLimits = new Vector2(-70f, 70f);

	Vector2 lookAngles;

	Vector2 currentMouseLook;
	Vector2 smoothMove;
	int lastLookFrame;

    void Start()
    {
     //Cursor.lockState = CursorLockMode.Locked;   
    	target = transform.parent;
    }

    void Update()
    {
        //ToggleCursorLock();
        FirstPersonCamera();
    }

    void ToggleCursorLock(){
    	if(Input.GetKeyDown(KeyCode.Escape)){
    		if(Cursor.lockState == CursorLockMode.Locked){
    			Cursor.lockState = CursorLockMode.None;
    		}else{
    			Cursor.lockState = CursorLockMode.Locked;
    			Cursor.visible = false;
    		}
    	}
    }

    void FirstPersonCamera(){
    	currentMouseLook = new Vector2(Input.GetAxis("Mouse Y"),Input.GetAxis("Mouse X"));
    	lookAngles.x += currentMouseLook.x * sensitivity * (invert ? 1f : -1f);
    	lookAngles.y += currentMouseLook.y * sensitivity;
    	lookAngles.x = Mathf.Clamp(lookAngles.x, verticalLookLimits.x, verticalLookLimits.y);
    	lookAngles.y = Mathf.Clamp(lookAngles.y, horizontalLookLimits.x, horizontalLookLimits.y);

    	currentRollAngle = Mathf.Lerp(currentRollAngle, Input.GetAxisRaw("Mouse X") * rollAngle, Time.deltaTime * rollSpeed);

    	transform.localRotation = Quaternion.Euler(lookAngles.x, lookAngles.y, currentRollAngle);
    	//target.localRotation = Quaternion.Euler(0f, lookAngles.y, 0f);

    }
}
