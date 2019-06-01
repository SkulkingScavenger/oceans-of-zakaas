using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    public Submarine structure;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        if(isHovered()){
        	mr.material.color = new Color(0,1,0);
        }else{
        	mr.material.color = new Color(1,1,1);
        }
    }


    bool isHovered(){
		RaycastHit hit;
		Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit)){
			Transform objectHit = hit.transform;
			Rigidbody rb = hit.rigidbody;
			return (rb != null && rb.gameObject == gameObject);
		}else{
			return false;
		}
	}
}
