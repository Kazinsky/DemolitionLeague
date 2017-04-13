using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedCharacter : Photon.PunBehaviour
{
    [SerializeField]
    protected CharacterStats playerStats;

    //Components
    public Collider ThisCollider { get; private set; }
    public Rigidbody ThisRigidBody { get; private set; }

    // Use this for initialization
    public virtual void Start()
    {
        ThisCollider = GetComponent<Collider>();
        ThisRigidBody = GetComponent<Rigidbody>();
    }

    public bool IsDead()
    {
        if (playerStats.Health <= 0)
            return true;
        else
            return false;
    }

    protected void Die()
    {
    }


    public virtual CharacterStats PlayerStats
    {
        get
        {
            return playerStats;
        }

        set
        {
            playerStats = value;
        }
    }

    /*Functions To Alter Stats */
    public virtual void removeHealth(int value)
    {
        playerStats.Health -= value;

    }

    public virtual void addHealth(int value)
    {
        playerStats.Health += value;

    }

    public virtual void removeMaxHealth(int value)
    {
        playerStats.MaxHealth -= value;

    }

    public virtual void addMaxHealth(int value)
    {
        playerStats.MaxHealth += value;

    }

    public void removeMovementSpeed(int value)
    {
        playerStats.MovementSpeed -= value;
    }

    public void addMovementSpeed(int value)
    {
        playerStats.MovementSpeed += value;
    }

    /* Damage */

    //Deals with removing health, activating damage animations, and checking if dead then trigger death
    private void TakeDamage(Damage damage)
    {

        if (damage.DamageValue > 0)
        {
            removeHealth(damage.DamageValue);

            //Create a UI Damage Pop up
            DamagePopUpController.CreateDamagePopUp(damage.DamageValue.ToString(), new Vector3(transform.position.x, transform.position.y + ThisCollider.bounds.size.y, transform.position.z), damage.damageType);

            if (IsDead())
            {
                Die();
            }
        }
    }

    //Calculates damage based on An Attack
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


    //For Trigger Collisions
    public virtual void OnTriggerEnter(Collider other)
    {
        if (!IsDead())
        {

            //If character can take damage from these sources
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



    //For typical Collisions
    public virtual void OnCollisionEnter(Collision other)
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


    public virtual void OnParticleCollision(GameObject other)
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

}
