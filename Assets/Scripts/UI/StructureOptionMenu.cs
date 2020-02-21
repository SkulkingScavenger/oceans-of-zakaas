using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructureOptionMenu : MonoBehaviour{
	public GameObject[] basicToggles = {null,null,null};
	public GameObject[] advancedToggles = {null,null,null};
	public TriangleCell cell;
	public BaseBuilderMenu root;
	public BaseBuilderButton button;

	// Start is called before the first frame update
	void Start(){
		for(int i=0; i<basicToggles.Length; i++){
			int id = i;
			basicToggles[i] = transform.Find("BasicToggle"+i.ToString()).gameObject;
			basicToggles[i].GetComponent<Button>().onClick.AddListener(delegate { ToggleBasic(id); });
		}
		for(int i=0; i<advancedToggles.Length; i++){
			advancedToggles[i] = transform.Find("AdvancedToggle"+i.ToString()).gameObject;
			int id = i;
			advancedToggles[i].GetComponent<Button>().onClick.AddListener(delegate { ToggleAdvanced(id); });
		}

		gameObject.SetActive(false);
	}

	// Update is called once per frame
	public void Update(){
		if(Input.GetAxis("Mouse Right") > 0){
			gameObject.SetActive(false);
		}
	}

	public void Display(){
		for(int i=0; i<advancedToggles.Length; i++){
			advancedToggles[i].SetActive(cell.isMajorHub);
		}if(cell.pointsUp){
			transform.eulerAngles = new Vector3(0f,0f,0f);
			for(int i=0; i<basicToggles.Length; i++){
				basicToggles[i].transform.Find("Text").eulerAngles = new Vector3(0f,0f,0f);
			}
			for(int i=0; i<advancedToggles.Length; i++){
				advancedToggles[i].transform.Find("Text").eulerAngles = new Vector3(0f,0f,0f);
			}
		}else{
			transform.eulerAngles = new Vector3(0f,0f,180f);
			for(int i=0; i<basicToggles.Length; i++){
				basicToggles[i].transform.Find("Text").eulerAngles = new Vector3(0f,0f,0f);
			}
			for(int i=0; i<advancedToggles.Length; i++){
				advancedToggles[i].transform.Find("Text").eulerAngles = new Vector3(0f,0f,0f);
			}
		}
	}

	public void ToggleBasic(int id){
		TriangleCell neighbor;
		int[] coords = GetBasicCoordinates(id);
		int x = coords[0];
		int y = coords[1];
		int z = coords[2];
		neighbor = root.plan.GetCellAt(x,y,z);
		if (neighbor == null){
			neighbor = root.plan.AddCellAt(x,y,z);
		}
		cell.basicOpenings[id] = !cell.basicOpenings[id];
		neighbor.basicOpenings[id] = cell.basicOpenings[id];
		root.UpdateAll();
	}

	public void ToggleAdvanced(int id){
		TriangleCell neighborUp;
		TriangleCell neighborDown;
		int[] coords = GetAdvancedCoordinates(id);
		int x = coords[0];
		int y = coords[1];
		int z = coords[2];
		neighborUp = root.plan.GetCellAt(x,y,z-1);
		neighborDown = root.plan.GetCellAt(x,y,z+1);
		if (neighborUp == null){
			neighborUp = root.plan.AddCellAt(x,y,z-1);
		}
		if (neighborDown == null){
			neighborDown = root.plan.AddCellAt(x,y,z+1);
		}
		if(cell.advancedOpenings[id].type == 0){
			cell.advancedOpenings[id].type = 1;
			neighborDown.advancedOpenings[id].type = 2;
		}else if(cell.advancedOpenings[id].type == 1){
			cell.advancedOpenings[id].type = 2;
			neighborDown.advancedOpenings[id].type = 0;
			neighborUp.advancedOpenings[id].type = 1;
		}else if(cell.advancedOpenings[id].type == 2){
			cell.advancedOpenings[id].type = 0;
			neighborUp.advancedOpenings[id].type = 0;
		}
		root.UpdateAll();
	}

	bool isConnectionValid(int connectionId, bool isBasic){
		int[] coords;
		if(isBasic){
			coords = GetBasicCoordinates(connectionId);
		}else{
			coords = GetAdvancedCoordinates(connectionId);
		}
		for(int i=0;i<coords.Length;i++){
			if(Mathf.Abs(coords[i]) >= root.plan.max){
				return false;
			}
		}
		return true;
	}

	public int[] GetBasicCoordinates(int connectionId){
		int x = cell.x;
		int y = cell.y;
		int z = cell.z;
		switch(connectionId){
			case 0:
				if(cell.pointsUp){
					y += 1;
				}else{
					y -= 1;
				}
				break;
			case 1:
				if(cell.pointsUp){
					x -= 1;
				}else{
					x += 1;
				}
				break;
			case 2:
				if(cell.pointsUp){
					x += 1;
				}else{
					x -= 1;
				}
				break;
		}
		int[] output = {x,y,z};
		return output;
	}

	public int[] GetAdvancedCoordinates(int connectionId){
		int x = cell.x;
		int y = cell.y;
		int z = cell.z;
		switch(connectionId){
			case 0:
				if(cell.pointsUp){
					y += 1;
					x -= 1;
				}else{
					y -= 1;
					x += 1;
				}
				break;
			case 1:
				break;
			case 2:
				if(cell.pointsUp){
					y += 1;
					x += 1;
				}else{
					y -= 1;
					x -= 1;
				}
				break;
		}
		int[] output = {x,y,z};
		return output;
	}
}
