using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainguy : MonoBehaviour
{
	GameObject cam;
	Vector3 positionPrevious;

	// Start is called before the first frame update
	void Start()
	{
		cam = GameObject.FindGameObjectWithTag("MainCamera");
		//positionPrevious = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		//positiodnPrevious = new Vector3(transform.position.x,transform.position.y,transform.position.z);
		if(Input.GetAxis("Horizontal") > 0){
			transform.eulerAngles = new Vector3(0f,transform.eulerAngles.y+5,0f);
		}
		if(Input.GetAxis("Horizontal") < 0){
			transform.eulerAngles = new Vector3(0f,transform.eulerAngles.y-5,0f);
		}

		if(Input.GetAxis("Vertical") > 0){
			Rigidbody rb = gameObject.GetComponent<Rigidbody>();
			rb.AddForce(transform.forward, ForceMode.Impulse);
		}

		if(Input.GetAxis("Vertical") < 0){
			Rigidbody rb = gameObject.GetComponent<Rigidbody>();
			rb.AddForce(transform.forward * -1f, ForceMode.Impulse);
		}

		if(Input.GetAxis("Jump") > 0){
			Rigidbody rb = gameObject.GetComponent<Rigidbody>();
			rb.AddForce(transform.up * 1f, ForceMode.Impulse);
		}
	}

}
