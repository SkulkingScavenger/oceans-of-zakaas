using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Creature : MonoBehaviour{
	
	//Control
	public CreatureControl control = null;


	//Display
	private Animator anim;		// Reference to the player's animator component.
	public Transform display;
	public GameObject healthBar;
	public int speciesId;


	//Movement
	public CharacterController characterController;
	public float jumpForce = 0.03f;			// Amount of force added when the player jumps.
	public float gravityForce = -0.0008f;	// Amount of force added when the player is in the air.
	private bool grounded = true;			// Whether or not the player is grounded.
	public float accelerationX = 100f;		// rate of change per second in x velocity while moving
	public float accelerationY = 50f;		// rate of change per second in Y velocity while moving
	public float speedX = 0f;				// The current velocity in the x axis.
	public float speedY = 0f;				// The current velocity in the y axis.
	public float maxSpeedX = 6f;			// The fastest the player can travel in the x axis.
	public float maxSpeedY = 3f;			// The fastest the player can travel in the x axis.
	private float frictionForceX = 20f;		// rate of change per second in x velocity due to friction
	private float frictionForceY = 10f;		// rate of change per second in y velocity due to friction

	bool isInteracting = false;

	void Awake(){
		// display = transform.Find("Display");
		// if(isLocalPlayer){
		// 	//mainCamera.GetComponent<CameraObject>().root = getPlayer().creatureObj.transform;
		// }
	}

	// Start is called before the first frame update
	void Start() {
		characterController = gameObject.GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update() {
		// if(control == null){
		// 	if(isInteracting){return;}
		// 	//control = ClientScene.FindLocalObject(new NetworkInstanceId(controlID)).GetComponent<CreatureControl>();
		// 	return;
		// }
		// if(control.isHudCommand){return;}
		// Movement();
		// Jump();

		// switch(control.interfaceMode){
		// case "combat":
		// 	//CombatMain();
		// 	break;
		// case "exploration":
		// 	//ExplorationMain();
		// 	break;
		// }

		// display.transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y + 0.55f,0);
	}

	void Movement(){

	}

	void Jump(){}
}
