using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
	float yaw;
	float pitch;
	public Transform target = null;
	float distance = 3.0f;
	float height = 1.0f;
	float damping = 5.0f;
	bool smoothRotation = true;
	float rotationDamping = 10.0f;
	Vector3 targetLookAtOffset;
	float bumperDistanceCheck = 5f;
	float bumperCameraHeight = 1.0f;
	Vector3 bumperRayOffset;

	bool firstPersonEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
    	target = GameObject.FindGameObjectWithTag("player").transform;
        transform.parent = target.transform;
    }

    // Update is called once per frame
    void Update()
    {
    	if(Input.GetButtonDown("ToggleCameraMode")){
			firstPersonEnabled = !firstPersonEnabled;
		}

		if(firstPersonEnabled){
			FirstPersonView();
		}else{
			ThirdPersonView();
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
