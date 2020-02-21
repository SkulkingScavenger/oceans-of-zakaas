using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacterController : MonoBehaviour{
	float radius = 0.5f;
	bool contact;
	Vector3 positionPrevious;
	SphereCollider[] colliders = new SphereCollider[3];

	// Start is called before the first frame update
	void Start(){
		colliders[0] = transform.Find("head").gameObject.GetComponent<SphereCollider>();
		colliders[1] = transform.Find("torso").gameObject.GetComponent<SphereCollider>();
		colliders[2] = transform.Find("feet").gameObject.GetComponent<SphereCollider>();
	}

	// Update is called once per frame
	void Update(){
		
	}

	void FixedUpdate(){
		

	}

	public void Move(Vector3 move){
		transform.position += move;
		AttemptMovement(transform.position + move);
	}

	void AttemptMovement(Vector3 move){
		positionPrevious = transform.position;
		
		CheckCollision();
	}

	void CheckCollision(){
		Vector3 center;
		float radius;
		for(int i=0;i<colliders.Length;i++){
			center = colliders[i].center;
			radius = colliders[i].radius;
			Collider[] hitColliders = Physics.OverlapSphere(center, radius);
		}
	}

	void ResolveCollision(){

	}



	// public void DebugGround(bool primary, bool near, bool far, bool flush, bool step){
	// 	if (primary && primaryGround != null){
	// 		DebugDraw.DrawVector(primaryGround.point, primaryGround.normal, 2.0f, 1.0f, Color.yellow, 0, false);
	// 	}

	// 	if (near && nearGround != null){
	// 		DebugDraw.DrawVector(nearGround.point, nearGround.normal, 2.0f, 1.0f, Color.blue, 0, false);
	// 	}

	// 	if (far && farGround != null){
	// 		DebugDraw.DrawVector(farGround.point, farGround.normal, 2.0f, 1.0f, Color.red, 0, false);
	// 	}

	// 	if (flush && flushGround != null){
	// 		DebugDraw.DrawVector(flushGround.point, flushGround.normal, 2.0f, 1.0f, Color.cyan, 0, false);
	// 	}

	// 	if (step && stepGround != null){
	// 		DebugDraw.DrawVector(stepGround.point, stepGround.normal, 2.0f, 1.0f, Color.green, 0, false);
	// 	}
	// }


}
