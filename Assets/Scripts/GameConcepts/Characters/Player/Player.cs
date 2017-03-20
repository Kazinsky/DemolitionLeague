using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    [SerializeField]
    private int playerNumber;

    [SerializeField]
    private int playerModelNumber;

    [SerializeField]
    private CharacterStats playerStats;

    [SerializeField]
    private Ability currentAbility;

    [SerializeField]
    private Weapon currentWeapon;

    [SerializeField]
    private PlayerColor playerColor;

    private Game parentGame;

    //Components
    public Collider ThisCollider { get; private set; }
    public Rigidbody ThisRigidBody { get; private set; }

    //Constructors

    public void Initialize()
    {
        playerNumber = 0;
        playerModelNumber = 0;
        playerStats = new CharacterStats(0, 0, 0);
        currentAbility = new Ability();
        currentWeapon = new Weapon();
        playerColor = PlayerColor.Red;
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

    public void Initialize(int playerNumber, int playerModelNumber, CharacterStats stats, PlayerColor color, Game parentGame)
    {
        this.parentGame = parentGame;
        this.playerNumber = playerNumber;
        this.playerModelNumber = playerModelNumber;
        playerStats = stats;
        this.currentAbility = new Ability();
        this.currentWeapon = new Weapon();
        playerColor = color;
    }

    // Use this for initialization
    void Start () {
        ThisCollider = GetComponent<Collider>();
        ThisRigidBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    /*Functions To Alter Stats */
    public void removeHealth(int value)
    {
        playerStats.Health -= value;

        if (parentGame != null)
            parentGame.PlayerUpdateHealth(playerNumber, playerStats.Health);
    }

    public void addHealth(int value)
    {
        playerStats.Health += value;

        if (parentGame != null)
            parentGame.PlayerUpdateHealth(playerNumber, playerStats.Health);
    }

    public void removeMaxHealth(int value)
    {
        playerStats.MaxHealth -= value;

        if (parentGame != null)
            parentGame.PlayerUpdateMaxHealth(playerNumber, playerStats.MaxHealth);
    }

    public void addMaxHealth(int value)
    {
        playerStats.MaxHealth += value;

        if (parentGame != null)
            parentGame.PlayerUpdateMaxHealth(playerNumber, playerStats.MaxHealth);
    }

    public void removeMovementSpeed(int value)
    {
        playerStats.MovementSpeed -= value;
        UpdatePlayer();
    }

    public void addMovementSpeed(int value)
    {
        playerStats.MovementSpeed += value;
        UpdatePlayer();
    }


    /*Functions To Alter ammo count and  reset Ability */
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

    /* Damage */

    //Deals with removing health, activating damage animations, and checking if dead then trigger death
    private void TakeDamage(Damage damage)
    {

        if (damage.DamageValue < 0)
            damage.DamageValue = 0;

        removeHealth(damage.DamageValue);

        //Create a UI Damage Pop up
        DamagePopUpController.CreateDamagePopUp(damage.DamageValue.ToString(), new Vector3(transform.position.x + ThisCollider.bounds.size.x / 4.0f, transform.position.y + ThisCollider.bounds.size.y / 2.0f), damage.damageType);

        if (IsDead())
        {
            Die();
        }

    }

    public bool IsDead()
    {
        if (playerStats.Health <= 0)
            return true;
        else
            return false;
    }

    private void Die()
    {
        Destroy(this);
    }

    /* Damage Trigger */

    //For Trigger Collisions
    public void OnTriggerEnter(Collider other)
    {
        if (!IsDead())
        {

            //If character can take damage from these sources
            if (other.gameObject.layer == LayerMask.NameToLayer("Projectile") || other.gameObject.layer == LayerMask.NameToLayer("Trap"))
            {
                if(other.gameObject.GetComponent<DamageDealer>() != null)
                {
                    //Get Attack Component From Other object
                    Attack attack = other.gameObject.GetComponent<DamageDealer>().Attack;

                    TakeDamage(CalculateDamage(attack));


                    //Check if this attack apllies damage overtime Also 
                    if ((attack.DamageOvertime.IsDamageOvertime))
                    {
                        if (attack.DamageOvertime.DamageInstances > 0)
                        {
                            attack.DamageOvertime.totalDuration = attack.DamageOvertime.Duration;
                        }
                        else
                            attack.DamageOvertime.totalDuration = attack.DamageOvertime.Duration + Time.time;


                        attack.DamageOvertime.DamageInstances++;
                        //Start coroutine that will damage this character at each interval until duration is over
                        StartCoroutine(doDamageOverTime(attack));
                    }
                }
            }

            else if (other.gameObject.layer == LayerMask.NameToLayer("Specials"))
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
    }

    //For typical Collisions
    public void OnCollisionEnter(Collision other)
    {
        if (!IsDead())
        {
            //If character can take damage from this source
            if (other.gameObject.layer == LayerMask.NameToLayer("Projectile") || other.gameObject.layer == LayerMask.NameToLayer("Trap"))
            {
                if (other.gameObject.GetComponent<DamageDealer>() != null)
                {
                    //Get Attack Component From Other object
                    Attack attack = other.gameObject.GetComponent<DamageDealer>().Attack;

                    TakeDamage(CalculateDamage(attack));


                    //Check if this attack apllies damage overtime Also 
                    if ((attack.DamageOvertime.IsDamageOvertime))
                    {
                        if (attack.DamageOvertime.DamageInstances > 0)
                        {
                            attack.DamageOvertime.totalDuration = attack.DamageOvertime.Duration;
                        }
                        else
                            attack.DamageOvertime.totalDuration = attack.DamageOvertime.Duration + Time.time;


                        attack.DamageOvertime.DamageInstances++;
                        //Start coroutine that will damage this character at each interval until duration is over
                        StartCoroutine(doDamageOverTime(attack));
                    }
                }
            }
        }
    }

    //Calculates damage based on Character State And An Attack
    private Damage CalculateDamage(Attack attack)
    {
        Damage damage = new Damage();

        //Calculate Damage This can later be used to block damage if Player has a shield or something like that etc.
        damage.DamageValue = Mathf.RoundToInt(attack.BaseDamage);

        return damage;
    }


    //Coroutine to take damage from an attack over time damage dealer
    private IEnumerator doDamageOverTime(Attack attack)
    {

        while (attack.DamageOvertime.totalDuration > Time.time)
        {
            yield return new WaitForSeconds(attack.DamageOvertime.Intervals);
            TakeDamage(new Damage(attack.DamageOvertime.Damage, attack.DamageType));
        }

        attack.DamageOvertime.DamageInstances--;

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
            UpdatePlayer();
        }
    }

    public CharacterStats PlayerStats
    {
        get
        {
            return playerStats;
        }

        set
        {
            playerStats = value;
            UpdatePlayer();
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


    public void UpdatePlayer()
    {
        if (parentGame != null)
            parentGame.PlayerUpdate(this);
    }
}
