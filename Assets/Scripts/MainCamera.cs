using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
	float yaw;
	float pitch;
	public Transform target = null;
	float distance = 1f;
	float height = 11f;
	float damping = 2.0f;
	bool smoothRotation = true;
	float rotationDamping = 3.0f;

	Vector3 targetLookAtOffset = new Vector3(0.0f,0.0f,0.0f);
	float bumperDistanceCheck = 2.5f;
	float bumperCameraHeight = 1.0f;
	Vector3 bumperRayOffset = new Vector3(0.0f,0.0f,0.0f);

	bool firstPersonEnabled = false;

	// Start is called before the first frame update
	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
		transform.parent = target.transform;
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetButtonDown("ToggleCameraMode")){
			firstPersonEnabled = !firstPersonEnabled;
		}

		if(firstPersonEnabled){
			//FirstPersonView();
		}else{
			//ThirdPersonView();
		}
	}

	void ThirdPersonView(){
		Vector3 dir = new Vector3(target.forward.x,target.forward.y,target.forward.z);
		dir.Normalize();

		transform.position = target.position + (dir * distance);
		
		yaw += 0.5f * Input.GetAxis("Mouse X");
		//pitch += -0.5f * Input.GetAxis("Mouse Y");
		pitch = -6f*(Input.mousePosition.y/Screen.height) + target.position.y;
		transform.position = new Vector3(transform.position.x, pitch, transform.position.z);
	}

	void FirstPersonView(){
		Vector3 dir = new Vector3(target.forward.x,target.forward.y,target.forward.z);
		dir.Normalize();

		transform.position = transform.position + (dir * distance);
		
		yaw += 0.5f * Input.GetAxis("Mouse X");
		//pitch += -0.5f * Input.GetAxis("Mouse Y");
		pitch = -6f*(Input.mousePosition.y/Screen.height) + target.position.y;
		transform.position = new Vector3(transform.position.x, pitch, transform.position.z);
		transform.LookAt(target.position);
	}
}
