using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainguy : MonoBehaviour
{
	GameObject mainCamera;
	GameObject firstPersonCamera;
	CharacterController characterController;
	Vector3 positionPrevious;
	public float speed = 6.0f;
	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;
	public Vector3 moveDirection = Vector3.zero;
	public Vector3 faceDirection = Vector3.zero;

	// Start is called before the first frame update
	void Start()
	{
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		characterController = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update(){
		Movement();
	}

	void Movement(){
		//positionPrevious = new Vector3(transform.position.x,transform.position.y,transform.position.z);
		if(Input.GetAxis("Horizontal") > 0){
			transform.eulerAngles = new Vector3(0f,transform.eulerAngles.y+1,0f);
		}
		if(Input.GetAxis("Horizontal") < 0){
			transform.eulerAngles = new Vector3(0f,transform.eulerAngles.y-1,0f);
		}

		if(Input.GetAxis("Vertical") > 0){
			
		}

		if(Input.GetAxis("Vertical") < 0){
			
		}

		if (characterController.isGrounded){
			moveDirection = transform.forward;//new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
			//moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			//transform.LookAt(moveDirection);

			if (Input.GetButton("Jump")){
				moveDirection.y = jumpSpeed;
			}
		}

		// Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
		// when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
		// as an acceleration (ms^-2)
		moveDirection.y -= gravity * Time.deltaTime;

		// Move the controller
		characterController.Move(moveDirection * speed * Input.GetAxis("Vertical") * Time.deltaTime);
	}

}
