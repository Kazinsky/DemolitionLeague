using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : PlayerController {

	readonly WeaponObject weapon;
	Transform cannon;
	Player player;
	Plane[] planes;
	Plane currentPlane;
	Player currentTarget;
	private float targetReset = 2.5f;
	private float t = 2.0f;
	private float shootTimer = 1.0f;
	private float time = 1.0f;

	float runTime = 2.0f;

	//might not need to use
	public enum State
	{
		CHASING, TARGETING
	}
	public State state;

	public void Start(){
		planes = GameObject.FindGameObjectWithTag("ground").GetComponents<Plane>();
	}

	public void Update(){

	}

	public override void Shoot(){
		int layerMask = 1;
		if (!player.gameFinished) {
			if (currentTarget != null) {
				if (!Physics.Linecast (this.player.transform.position, currentTarget.transform.position, layerMask)) {
					if (player.weaponHasAmmo ()) {
						player.Fire (cannon.forward);
						player.removeWeaponAmmo (1);
					}
				}
			}
		}
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
		if (!player.gameFinished) {
			currentTarget = findTarget ();
			AbilityObject[] abilities = GameObject.FindObjectsOfType<AbilityObject> ();
			WeaponObject[] weapons = GameObject.FindObjectsOfType<WeaponObject> ();

			if ((abilities.Length !=0) && (currentTarget.transform.position - this.player.transform.position).magnitude < (abilities [0].transform.position - this.player.transform.position).magnitude) {
				//if player is further than powerup
				this.player.pickUp (abilities [0]);
			} 
			if((weapons.Length != 0)&& (currentTarget.transform.position - this.player.transform.position).magnitude < (weapons [0].transform.position - this.player.transform.position).magnitude){
				this.player.pickUp(weapons[0]);
			}
			else {
				this.player.nav.SetDestination (currentTarget.transform.position);
			}
			if (!player.ranAway && player.PlayerStats.Health/player.PlayerStats.MaxHealth < 0.5) {
				//RUNAWAY
				Vector3 direction = ( player.transform.position-currentTarget.transform.position ).normalized;
				float fleeingDistance = 20; 
				player.nav.SetDestination (player.transform.position + direction* fleeingDistance);
				player.ranAway = true;
			}
		}
	}

	public override void look(){
		if (!player.gameFinished) {
			if (currentTarget != null) {
				Quaternion targetRot = Quaternion.LookRotation (currentTarget.transform.position - this.player.transform.position);
				this.player.transform.rotation = Quaternion.Lerp (this.player.transform.rotation, targetRot, 0.5f);
			}
		}
	}
	//uses approximateDistance to find closest target, and state too
	//do a raycast to the direction of all players, if one is in line of sight, chose that target (or closest one)
	//otherwise, if a target is one the same plane as you, chose that one
	//otherwise, chose closest using approximate Distance
	public Player findTarget()
	{
		Player[] targets = GameObject.FindObjectsOfType<Player> ();
		Player[] visible = new Player[4];
		int y = 0;
		bool seen = false;
		//visibleplayers
		foreach(Player player in targets){
			int i = 0;
			if (!Physics.Linecast (character.transform.position, player.transform.position) && player.PlayerNumber != this.player.PlayerNumber) {
				Debug.Log ("no obstacles for "+ player.PlayerNumber);
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
				if (seenP != null) {
					if ((character.transform.position - seenP.transform.position).magnitude <= minDist) {
						minDist = (character.transform.position - seenP.transform.position).magnitude;
						closestSeen = seenP;
					}
				}
			}
			Debug.Log ("Chose "+ closestSeen);
			return closestSeen;
		}
		//if no visible players, check if on same plane
		/*
		//same plane
		findPlane ();
		foreach (Player player in targets) {
			if (currentPlane.GetDistanceToPoint (player.transform.position) <= 5) {
				Debug.Log ("Chose " + player);
				return player;
			}
		}*/
		//otherwise use A*
		//distance
		float closestD = approximateDistance(targets[0]);
		Player closestP;
		if (targets [0].PlayerNumber == this.player.PlayerNumber)
			closestP = targets [1];
		else
			closestP = targets [0];
		foreach(Player target in targets){
			if (approximateDistance(target) < closestD && target.PlayerNumber != this.player.PlayerNumber){
				closestD = approximateDistance (target);
				closestP = target;
			}
		}
		if (closestP.PlayerNumber != this.player.PlayerNumber) {
			return closestP;
		} else {
			return null;
		}
	}


	//uses A* to find closest target
	public float approximateDistance(Player target)
	{
		return (target.transform.position - this.player.transform.position).magnitude;
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
