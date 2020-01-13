using UnityEngine;
using UnityEngine.Networking;

public class CreatureControl : MonoBehaviour {
	public bool isPlayerControlled = false;
	public int playerId = -1;
	public bool isHudCommand = false;
	public string interfaceMode = "combat";
	public bool moveCommand = false;
	public bool attackCommand = false;

	public float commandX = 0;
	public float commandY = 0;

	public bool[] actionModifier = {false,false,false,false};
	public bool[] stanceModifier = {false,false,false,false};

	public bool shift = false;
	public bool ctrl = false;
	public bool jump = false;


}