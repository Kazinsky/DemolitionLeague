using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : PlayerController {

	readonly WeaponObject weapon;
	Transform cannon;
	Player player;
	Plane[] planes;
	Plane currentPlane;
	//might not need to use
	public enum State
	{
		CHASING, TARGETING
	}
	public State state;

	public void Start(){
		planes = GameObject.FindGameObjectWithTag("ground").GetComponents<Plane>();
	}

	public AIController(GameObject character) : base(character)
	{
		player = character.GetComponent<Player>();

		maxMoveSpeed = player.PlayerStats.MovementSpeed;
		maxTurnSpeed = 5;
		cannon = character.transform.GetChild(1);
		weapon = Object.Instantiate(GameObject.Find("Armory"), cannon).GetComponent<WeaponObject>();
	}

	//uses findTarget to go to target
	public override void moveInput ()
	{
		//go to target
	}

	//uses approximateDistance to find closest target, and state too
	//do a raycast to the direction of all players, if one is in line of sight, chose that target (or closest one)
	//otherwise, if a target is one the same plane as you, chose that one
	//otherwise, chose closest using approximate Distance
	public Player findTarget()
	{
		Player[] targets = GameObject.FindObjectsOfType<Player> ();


		Player[] visible = new Player[4];
		bool seen = false;
		//visibleplayers
		foreach(Player player in targets){
			int i = 0;
			if (Physics.Linecast (character.transform.position, player.transform.position)) {
				seen = true;
				visible [i] = player;
			}
			i++;
		}
		//if you have at least one player in line of sight
		if (seen) {
			//find closest visible player
			float minDist = 10000;
			Player closestSeen = visible[0];
			foreach (Player seenP in visible) {
				if ((character.transform.position - seenP.transform.position).magnitude <= minDist) {
					minDist = (character.transform.position - seenP.transform.position).magnitude;
					closestSeen = seenP;
				}
			}
			//Debug.Log ("Chose "+ closestSeen);
			return closestSeen;
		}
		//if no visible players, check if on same plane

		//same plane
		findPlane ();
		foreach (Player player in targets) {
			if (currentPlane.GetDistanceToPoint (player.transform.position) <= 5) {
				Debug.Log ("Chose " + player);
				return player;
			}
		}
		//otherwise use A*
		//distance
		int closestD = approximateDistance(targets[0]);
		Player closestP = targets [0];
		foreach(Player target in targets){
			if (approximateDistance(target) < closestD){
				closestD = approximateDistance (target);
				closestP = target;
			}
		}
		Debug.Log ("Chose " + closestP.name);
		return closestP;
	}


	//uses A* to find closest target
	public int approximateDistance(Player target)
	{
		return 0;
	}

	//use this to see what plane object is on
	public void findPlane(){
		foreach(Plane plane in planes){
			if (plane.GetDistanceToPoint (character.transform.position) <= 5) {
				currentPlane = plane;
				break;
			}
		}
	}
}
