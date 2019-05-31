using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPreview : MonoBehaviour
{
	MeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
        mr = gameObject.GetComponent<MeshRenderer>();
        mr.material.color = new Color(0.5f,0.5f,1f,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        SetPosition();
        if(Input.GetMouseButtonDown(0)){
        	GameObject block = Instantiate(Resources.Load<GameObject>("Prefabs/BasicBlock"));
        	block.transform.position = transform.position;
        }

    }

    void SetPosition(){
		RaycastHit hit;
		Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		int layermask = 1 << 2;
		layermask = ~layermask;
		if(Physics.Raycast(ray, out hit, Mathf.Infinity, layermask)){
			Transform objectHit = hit.transform;
			Rigidbody rb = hit.rigidbody;
			transform.position = PositionFromOffset(hit.transform.position, hit.point, 1);
			transform.localPosition = SnapToGrid(transform.localPosition);
			mr.material.color = new Color(0.5f,0.5f,1f,0.5f);
		}else{
			mr.material.color = new Color(0.5f,0.5f,1f,0.0f);
		}
	}

	Vector3 PositionFromOffset(Vector3 startPosition, Vector3 secondPosition, float distance){
		Vector3 direction = (secondPosition - startPosition).normalized;
		direction = SnapVector(direction, 90);
		return startPosition + (direction*distance);
	}

	Vector3 SnapVector(Vector3 v, float snapAngle){
		v = v.normalized;
		if(Mathf.Abs(v.x) >= Mathf.Abs(v.y) && Mathf.Abs(v.x) >= Mathf.Abs(v.z)){
			return new Vector3(Mathf.Sign(v.x),0f,0f);
		}else if(Mathf.Abs(v.y) >= Mathf.Abs(v.x) && Mathf.Abs(v.y) >= Mathf.Abs(v.z)){
			return new Vector3(0f,Mathf.Sign(v.y),0f);
		}else{
			return new Vector3(0f,0f,Mathf.Sign(v.z));
		}
	}

	Vector3 SnapToGrid(Vector3 input){
		float x = Mathf.Round(input.x);
		float y = Mathf.Round(input.y);
		float z = Mathf.Round(input.z);
		Vector3 output = new Vector3(x, y, z);
		return output;
	}
}
