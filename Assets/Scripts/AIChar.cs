using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChar : Character {
	
	[SerializeField]
	private string playerName;

	[SerializeField]
	private int playerNumber;

	[SerializeField]
	private int playerModelNumber;

	[SerializeField]
	private Ability currentAbility;

	[SerializeField]
	private Weapon currentWeapon;

	[SerializeField]
	private PlayerColor playerColor;
	/*
	[SerializeField]
	private bool localPlayer;//maybe take out
*/
	[SerializeField]
	private bool AIPlayer;

	private int score;

	private Game parentGame;

	private AIController playerController; // this will be the AI version of local	

	//Initialisers In case they are needed to set up values
	public void Initialize()
	{
		playerNumber = 0;
		playerModelNumber = 0;
		playerStats = new CharacterStats(0, 0, 0);
		currentAbility = new Ability();
		currentWeapon = new Weapon();
		playerColor = PlayerColor.Blue;
	}


	public void Initialize(int playerNumber, int playerModelNumber,CharacterStats stats, Ability ability, Weapon weapon, PlayerColor color, Game parentGame)
	{
		this.parentGame = parentGame;
		this.playerNumber = playerNumber;
		this.playerModelNumber = playerModelNumber;
		playerStats = stats;
		currentAbility = ability;
		currentWeapon = weapon;
		playerColor = color;
	}

	public override void Start()
	{
		base.Start ();

		if (AIPlayer) {
			setUpPlayerController(new AIController(this.gameObject));
		}
			
	}

	void Update(){
		//playerController.findTarget ();
		if (playerController != null)
			playerController.moveInput ();
	}

	public void setUpPlayerController(AIController controller){
		playerController = controller;
	}


	/*Functions To Alter Stats */
	public override void removeHealth(int value)
	{
		base.removeHealth(value);

		if (parentGame != null)
			parentGame.PlayerUpdateHealth(playerNumber, playerStats.Health);
	}

	public override void addHealth(int value)
	{
		base.addHealth(value);

		if (parentGame != null)
			parentGame.PlayerUpdateHealth(playerNumber, playerStats.Health);
	}

	public override void removeMaxHealth(int value)
	{
		base.removeMaxHealth(value);

		if (parentGame != null)
			parentGame.PlayerUpdateMaxHealth(playerNumber, playerStats.MaxHealth);
	}

	public override void addMaxHealth(int value)
	{
		base.addMaxHealth(value);

		if (parentGame != null)
			parentGame.PlayerUpdateMaxHealth(playerNumber, playerStats.MaxHealth);
	}


	/*Functions To Alter ammo count */
	public void removeWeaponAmmo(int num)
	{

		if(currentWeapon.AmmoCount != Infinity.InfinityValue())
		{
			currentWeapon.AmmoCount -= num;

			if (parentGame != null)
				parentGame.PlayerUpdateAmmoCount(playerNumber, currentWeapon.AmmoCount);
		}

	}

	public void addWeaponAmmo(int num)
	{
		if (currentWeapon.AmmoCount != Infinity.InfinityValue())
		{
			currentWeapon.AmmoCount += num;

			if (parentGame != null)
				parentGame.PlayerUpdateAmmoCount(playerNumber, currentWeapon.AmmoCount);
		}
	}

	public void removeScore(int value)
	{
		Score -= value;
	}

	public  void addScore(int value)
	{
		Score += value;
	}

	public bool weaponHasAmmo()
	{
		if (CurrentWeapon.AmmoCount > 0)
			return true;
		else
			return false;
	}

	/* Damage */

	/* Damage Trigger */

	//For Trigger Collisions
	public override void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter(other);

		if (other.gameObject.layer == LayerMask.NameToLayer("Specials"))
		{
			AbilityObject ability;
			WeaponObject weapon;

			if (ability = other.gameObject.GetComponent<AbilityObject>()){
				CurrentAbility = ability.Ability;
			}
			else if(weapon = other.gameObject.GetComponent<WeaponObject>())
			{
				CurrentWeapon = weapon.Weapon;
			}
		}
	}


	//For typical Collisions
	public override void OnCollisionEnter(Collision other)
	{
		base.OnCollisionEnter(other);

		if (other.gameObject.layer == LayerMask.NameToLayer("Specials"))
		{
			AbilityObject ability;
			WeaponObject weapon;

			if (ability = other.gameObject.GetComponent<AbilityObject>())
			{
				CurrentAbility = ability.Ability;
			}
			else if (weapon = other.gameObject.GetComponent<WeaponObject>())
			{
				CurrentWeapon = weapon.Weapon;
			}
		}
	}


	public override void OnParticleCollision(GameObject other)
	{
		base.OnParticleCollision(other);

		if (other.gameObject.layer == LayerMask.NameToLayer("Specials"))
		{
			AbilityObject ability;
			WeaponObject weapon;

			if (ability = other.gameObject.GetComponent<AbilityObject>())
			{
				CurrentAbility = ability.Ability;
			}
			else if (weapon = other.gameObject.GetComponent<WeaponObject>())
			{
				CurrentWeapon = weapon.Weapon;
			}
		}
	}


	public int PlayerNumber
	{
		get
		{
			return playerNumber;
		}

		set
		{
			playerNumber = value;
			
		}
	}

	public override CharacterStats PlayerStats
	{
		get
		{
			return base.PlayerStats;
		}

		set
		{
			base.PlayerStats = value;
			
		}
	}

	public Ability CurrentAbility
	{
		get
		{
			return currentAbility;
		}

		set
		{
			currentAbility = value;

			if (parentGame != null)
				parentGame.PlayerUpdateAbility(playerNumber, currentAbility);
		}
	}

	public Weapon CurrentWeapon
	{
		get
		{
			return currentWeapon;
		}

		set
		{
			currentWeapon = value;

			if (parentGame != null)
			{
				parentGame.PlayerUpdateWeapon(playerNumber, currentWeapon);
				parentGame.PlayerUpdateAmmoCount(playerNumber, currentWeapon.AmmoCount);
			}

		}
	}

	public PlayerColor PlayerColor
	{
		get
		{
			return playerColor;
		}

		set
		{
			playerColor = value;

			if (parentGame != null)
				parentGame.PlayerUpdateColor(playerNumber, playerColor);
		}
	}

	public Game ParentGame
	{
		get
		{
			return parentGame;
		}

		set
		{
			parentGame = value;
		}
	}

	public int PlayerModelNumber
	{
		get
		{
			return playerModelNumber;
		}

		set
		{
			playerModelNumber = value;

			if(parentGame != null)
				parentGame.PlayerUpdateModel(playerNumber, playerModelNumber);
		}
	}

	public string PlayerName
	{
		get
		{
			return playerName;
		}

		set
		{
			playerName = value;
		}
	}

	public int Score
	{
		get
		{
			return score;
		}

		set
		{
			score = value;
		}
	}

}
