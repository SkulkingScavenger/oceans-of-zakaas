using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour{
	Vector2 windowPosition;
	GameObject canvas;
	GameObject text;
	GameObject windowImage;
	GameObject gameManager;
	GameObject skrizzikMenu;
	public Sprite[] itemIcons;

	void Start(){
		gameManager = GameObject.FindGameObjectWithTag("GameController");
		canvas = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/UI/Canvas"));

		text = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/UI/Text"));
		text.transform.SetParent(canvas.transform);
		text.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);

		CreateBaseMenu();

		itemIcons = Resources.LoadAll<Sprite>("Images/base_icons.png");
	}

	void Update(){

		
	}


	void CreateBaseMenu(){
		skrizzikMenu = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/UI/SkrizzikMenu"));
		skrizzikMenu.transform.SetParent(canvas.transform);
		skrizzikMenu.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);
	}

}
