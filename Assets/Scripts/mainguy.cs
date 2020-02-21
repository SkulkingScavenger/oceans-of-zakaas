using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainguy : MonoBehaviour
{
	GameObject mainCamera;
	GameObject firstPersonCamera;
	Creature creature;

	Vector3 positionPrevious;
	public float speed = 16.0f;
	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;
	public Vector3 moveDirection = Vector3.zero;
	public Vector3 faceDirection = Vector3.zero;

	public Vector3 baseCameraDirection = Vector3.zero;

	// Start is called before the first frame update
	void Start()
	{
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		mainCamera.transform.position = transform.position;
		mainCamera.transform.Translate(transform.forward * -3);
		mainCamera.transform.Translate(transform.up * 1.2f);
		creature = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/SimpleCharacter")).GetComponent<Creature>();
		creature.transform.position = transform.position;
	}

	// Update is called once per frame
	void Update(){
		Movement();
	}

	void Movement(){
		//move the creature
		CreatureMovement();
		//if the creature is out of range, move the camera
		CameraMovement();
	}

	
	void CameraMovement(){

		cameraOrbit();

		
		//if creature goes to far away along the camera axis (off into the horizon) follow it. 
		//also back off if it gets too close

		//vector magic
		Vector3 distance = transform.position - creature.transform.position;
		Vector3 relativeCreatureDirection = creature.transform.position - transform.position;
		relativeCreatureDirection.y = 0;
		relativeCreatureDirection.Normalize();
		Vector2 a = new Vector2(transform.forward.x, transform.forward.z);
		Vector2 b = new Vector2(relativeCreatureDirection.x, relativeCreatureDirection.z);
		float d;

		d = Vector2.Dot(b,a); //just trust me, this gets the cos of the thing
		Vector2 c = new Vector2(distance.x, distance.z);
		d = d * c.magnitude; //that's right
		
		
		if(d > 1f){
			transform.Translate(transform.forward * (d - 1));
		}
		if(d < -1f){
			transform.Translate(transform.forward * (d + 1));
		}

		//if the character starts to go off the screen in one direction or the other, scoot around to face it
		a = new Vector2(transform.right.x, transform.right.z);
		float w = Vector2.Dot(b,a);
		w = w * c.magnitude;
		if(w > 2f){
			transform.Translate(transform.right * (w - 2));
		}
		if(w < -2f){
			transform.Translate(transform.right * (w + 2));
		}
	}

	void cameraOrbit(){
		//orbit around if mouse at camera edge
		Camera camera = mainCamera.GetComponent<Camera>();
		Vector3 mousePosition = camera.ScreenToViewportPoint(Input.mousePosition);
		float theta = 0;
		if(mousePosition.x < 0.1f){
			theta = 30;
		}
		if(mousePosition.x > 0.9f){
			theta = -30;
		}
		transform.RotateAround(creature.transform.position, transform.up, theta*Time.deltaTime);
		// float mouseX = Input.GetAxis("Mouse X");
		// Debug.Log(mouseX);
		// if (mouseX < 10){
		// 	transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y + 10*Time.deltaTime,transform.eulerAngles.z);
		// }
	}

	void CreatureMovement(){
		float speed = 0f;
		// if(Input.GetAxis("Horizontal") > 0){
		// 	transform.eulerAngles = new Vector3(0f,transform.eulerAngles.y+1,0f);
		// }
		// if(Input.GetAxis("Horizontal") < 0){
		// 	transform.eulerAngles = new Vector3(0f,transform.eulerAngles.y-1,0f);
		// }
		
		if(Input.GetAxis("Vertical") > 0){
			speed = 5.0f;
		}else if(Input.GetAxis("Vertical") < 0){
			speed = -5.0f;
		}else{
			speed = 0f;
		}
		

		// Transform groundChecker = creature.transform.Find("groundSensor");
		// float groundDistance = 1;
		// bool isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, 8, QueryTriggerInteraction.Ignore);
		// float gravity = 1f;
		// float yVelocity = 0;
		// yVelocity += gravity * Time.deltaTime;
		// if (isGrounded && yVelocity < 0){
		// 	yVelocity = 0f;
		// }

		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		if(move != Vector3.zero){
			Debug.Log(Vector2.SignedAngle(new Vector2(move.x,move.z),new Vector2(transform.forward.x,transform.forward.z)));
			//Debug.Log(move);
			move = alignVectorToCameraAxis(move);
			//Debug.Log(move);
		}
		
		creature.characterController.Move(move * Time.deltaTime * 1);

		if (move != Vector3.zero){
			creature.transform.forward = move;
		}

		this.transform.position = creature.transform.position;

	}

	Vector3 alignVectorToCameraAxis(Vector3 vec){
		//we assume z axis of vec is global forward. we want to rotate it so that it aligns with camera axis instead
		//to do this we get the angle difference then rotate

		Vector2 a = new Vector2(transform.forward.x, transform.forward.z);
		Vector2 b = new Vector2(vec.x, vec.z);
		Vector2 c = new Vector2(0f,1f);
		float angleAdjustAmount;
		float finalAngle;
		float x,z;

		float cameraAngle = Vector2.SignedAngle(a,c);
		float worldAngle = 0f;
		float vecAngle = Vector2.SignedAngle(b,c);
		Debug.Log("cameraAngle:" + cameraAngle.ToString());
		Debug.Log("worldAngle:" + worldAngle.ToString());
		Debug.Log("vecAngle:" + vecAngle.ToString());

		angleAdjustAmount = Vector2.SignedAngle(a,c);
		Debug.Log("angleAdjustAmount:" + angleAdjustAmount.ToString());
		Debug.Log(Vector2.SignedAngle(b,c));
		finalAngle = -1*(Vector2.SignedAngle(b,c) + angleAdjustAmount) + 90;
		Debug.Log("finalAngle:" + finalAngle.ToString());
		z = Mathf.Sin(degToRad(finalAngle));
		x = Mathf.Cos(degToRad(finalAngle));
		Vector3 output = new Vector3(x, 0f, z);
		output.Normalize();

		return (output * vec.magnitude);
	}

	float radToDeg(float rad){
		return (rad*180)/Mathf.PI;
	}

	float degToRad(float deg){
		return (deg/180)*Mathf.PI;
	}

}
