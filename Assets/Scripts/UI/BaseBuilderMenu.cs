using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseBuilderMenu : MonoBehaviour{
	List<GameObject> buttons = new List<GameObject>();
	GameObject contextMenu;
	public SkrizzikFloorplan plan = new SkrizzikFloorplan();
	public int currentFloor;
	public int xOffset = 0;
	public int yOffset = 0;

	// Start is called before the first frame update
	void Start(){
		currentFloor = 50;
		int width = 10;
		int height = 10;
		contextMenu = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/UI/StructureOptionMenu"));
		contextMenu.GetComponent<StructureOptionMenu>().root = this;
		contextMenu.transform.SetParent(transform);
		Transform container = transform.Find("CellContainer");
		container.localPosition = new Vector3(0f,300f,0f);
		GameObject button;
		BaseBuilderButton b;
		for(int i=0;i<height;i++){
			for(int j=0;j<width;j++){
				button = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/UI/BaseBuilderButton"));
				button.transform.SetParent(container);
				b = button.GetComponent<BaseBuilderButton>();
				b.root = this;
				b.x = j;
				b.y = i;
				buttons.Add(button);
				plan.AddCellAt(j, i, 50);
			}
		}
		transform.Find("UpButton").gameObject.GetComponent<Button>().onClick.AddListener(delegate { changeFloor(true); });
		transform.Find("DownButton").gameObject.GetComponent<Button>().onClick.AddListener(delegate { changeFloor(false); });
		transform.Find("InstantiateButton").gameObject.GetComponent<Button>().onClick.AddListener(delegate { Instantiate(); });
	}

	// Update is called once per frame
	void Update(){
		
	}

	public void UpdateAll(){
		for(int i=0;i<buttons.Count;i++){
			buttons[i].GetComponent<BaseBuilderButton>().UpdateDisplay();
		}
	}

	void changeFloor(bool goingUp){
		if(goingUp){
			currentFloor += 1;
		}else{
			currentFloor -= 1;
		}
		float yAdjust = (currentFloor % 2f)*24f;
		transform.Find("CellContainer").localPosition = new Vector3(0f,300f+yAdjust,0f);
		contextMenu.SetActive(false);
		UpdateAll();

	}

	public GameObject GetContextMenu(){
		return contextMenu;
	}


	public void Instantiate(){
		GameObject skrizzik = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skrizzik/Skrizzik"));
		TriangleCell cell;
		GameObject hub;
		GameObject tunnel;
		float xOffset = 0f;
		float yOffset = 0f;
		float xScale = 0.432977f;
		float yScale = 0.5f;
		float zScale = 0.3f;
		float yOdd = 0.249965f;
		yScale += yOdd;
		float x,y,z;
		for(int i=0;i<plan.tunnels.Count;i++){
			if(plan.tunnels[i] != null && !plan.tunnels[i].IsEmpty()){
				cell = plan.tunnels[i];
				if(cell.isMajorHub){
					hub = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skrizzik/HubPrimary"));
				}else{
					hub = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skrizzik/HubTertiary"));
				}
				hub.transform.SetParent(skrizzik.transform);
				x = cell.x*xScale + xOffset;
				y = cell.y*yScale + yOffset;
				if(cell.z % 2 == 0){
					if(cell.y % 2 == 0){
						y += (yOdd*(cell.x % 2));
					}else{
						y += (yOdd*((cell.x +1) % 2));
					}
				}else{
					if(cell.y % 2 == 1){
						y += (yOdd*(cell.x % 2));
					}else{
						y += (yOdd*((cell.x +1) % 2));
					}
				}
				y -= (yOdd*(cell.z % 2)); //odd floors shift up
				z = cell.z*zScale;
				hub.transform.localPosition = new Vector3(x, y, z);
				if(!cell.pointsUp){
					hub.transform.eulerAngles = new Vector3(0f,0f,hub.transform.eulerAngles.z+180f);
				}
				if(cell.isMajorHub){
					for(int j=0;j<cell.basicOpenings.Length;j++){
						if(cell.basicOpenings[j]){
							tunnel = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skrizzik/OpeningPrimary"));
						}else{
							tunnel = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skrizzik/CapPrimary"));
						}
						tunnel.transform.SetParent(hub.transform);
						tunnel.transform.localPosition = Vector3.zero;
						z = tunnel.transform.eulerAngles.z+j*120f;
						if(!cell.pointsUp){
							z -= 180f;//180 -> 300
						}
						tunnel.transform.eulerAngles = new Vector3(0f,0f,z);
					}
					for(int j=0;j<cell.advancedOpenings.Length;j++){
						tunnel = null;
						switch(cell.advancedOpenings[j].type){
							case 0:
							tunnel = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skrizzik/CapSecondary"));
							break;
							case 1:
							tunnel = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skrizzik/OpeningSecondaryUp"));
							break;
							case 2:
							tunnel = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skrizzik/OpeningSecondaryDown"));
							break;
						}
						tunnel.transform.SetParent(hub.transform);
						tunnel.transform.localPosition = Vector3.zero;
						z = tunnel.transform.eulerAngles.z + j*120f;
						if(cell.pointsUp){
							z -= 120f;//180 -> 300
						}else{
							z += 60f;
						}
						tunnel.transform.eulerAngles = new Vector3(0f,0f,z);
					}
				}else{
					for(int j=0;j<cell.basicOpenings.Length;j++){
						if(cell.basicOpenings[j]){
							tunnel = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skrizzik/OpeningTertiary"));
						}else{
							tunnel = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skrizzik/CapTertiary"));
						}
						tunnel.transform.SetParent(hub.transform);
						tunnel.transform.localPosition = Vector3.zero;
						z = tunnel.transform.eulerAngles.z + j*120f;
						if(!cell.pointsUp){
							z += 120f;
						}else{
							z -= 60f;
						}
						tunnel.transform.eulerAngles = new Vector3(0f,0f,z);
					}
				}
			}
		}
		skrizzik.transform.eulerAngles = new Vector3(-90f,0f,0f);
		skrizzik.transform.localScale = new Vector3(20f,20f,20f);
		skrizzik.transform.position = new Vector3(0f,-300f,0f);
		GameObject.Destroy(gameObject);
	}
}
