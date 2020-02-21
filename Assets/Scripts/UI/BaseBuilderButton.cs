using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseBuilderButton : MonoBehaviour{

	public BaseBuilderMenu root = null;
	public int x;
	public int y;
	Sprite[] icons;
	GameObject hub;
	GameObject[] connections = {null,null,null,null,null,null};

	void Start(){
		icons = Resources.LoadAll<Sprite>("Images/UI/triangle_icons");
		Transform container = transform.Find("SubIcons");
		hub = container.Find("Hub").gameObject;
		for(int i=0;i<6;i++){
			connections[i] = container.Find("Connection" + i.ToString()).gameObject;
		}
		gameObject.GetComponent<Button>().onClick.AddListener(delegate { OpenContextMenu(); });
		UpdateDisplay();
	}

	void Update(){
		
	}

	public void OpenContextMenu(){
		GameObject cm = root.GetContextMenu();
		cm.SetActive(true);
		cm.transform.position = transform.position;
		cm.transform.Translate(new Vector3(0f,0f,-1f));
		StructureOptionMenu som = cm.GetComponent<StructureOptionMenu>();
		som.cell = GetCell();
		som.Display();
	}

	TriangleCell GetCell(){
		return root.plan.GetCellAt(x+root.xOffset, y+root.yOffset, root.currentFloor);
	}

	public void UpdateDisplay(){
		gameObject.GetComponent<RectTransform>().localPosition = new Vector3(x*46,y*-80,0);
		TriangleCell cell = GetCell();
		Image spr = gameObject.GetComponent<Image>();
		Transform container = transform.Find("SubIcons");
		if(cell == null){
			cell = root.plan.AddCellAt(x+root.xOffset, y+root.yOffset, root.currentFloor);
		}
		if(cell.pointsUp){
			transform.eulerAngles = new Vector3(0f,0f,0f);
		}else{
			transform.position = new Vector3(transform.position.x+1f, transform.position.y + 27f, transform.position.z);
			transform.eulerAngles = new Vector3(0f,0f,180f);
		}
		spr = hub.GetComponent<Image>();
		if(cell.IsEmpty()){
			spr.color = new Color(1f,1f,1f,0f);
		}else if(cell.isMajorHub){
			spr.color = new Color(1f,1f,1f,1f);
			spr.sprite = icons[1];
		}else{
			spr.color = new Color(1f,1f,1f,1f);
			spr.sprite = icons[6];
		}
		bool isBasicOpening;
		for(int i=0; i<connections.Length; i++){
			spr = connections[i].GetComponent<Image>();
			isBasicOpening = i % 2 == 0;
			if(isBasicOpening){
				if(!cell.basicOpenings[i/2]){
					spr.color = new Color(1f,1f,1f,0f);
				}else if(cell.isMajorHub){
					spr.color = new Color(1f,1f,1f,1f);
					spr.sprite = icons[2];
				}else{
					spr.color = new Color(1f,1f,1f,1f);
					spr.sprite = icons[0];
				}
			}else{
				if(!cell.isMajorHub || cell.advancedOpenings[i/2].type == 0){
					spr.color = new Color(1f,1f,1f,0f);
				}else if(cell.advancedOpenings[i/2].type == 1){
					spr.color = new Color(1f,1f,1f,1f);
					spr.sprite = icons[5];
				}else if(cell.advancedOpenings[i/2].type == 2){
					spr.color = new Color(1f,1f,1f,1f);
					spr.sprite = icons[3];
				}
			}
		}
	}
}
