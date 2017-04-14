using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Local : PlayerController
{

    readonly WeaponObject weapon;
    Transform cannon;
    Player player;

    public Local(GameObject character) : base(character)
    {
        player = character.GetComponent<Player>();

        maxMoveSpeed = player.PlayerStats.MovementSpeed;
        maxTurnSpeed = 5;
        cannon = character.transform.GetChild(1);
        weapon = Object.Instantiate(GameObject.Find("Armory"), cannon).GetComponent<WeaponObject>();
    }

    public override void moveInput()
    {
        character.GetComponent<Rigidbody>().velocity = Vector3.zero;

		if (Input.GetAxis("Vertical" + (int)player.PlayerControllerNumber) > 0 || Input.GetAxis("Vertical" + (int)player.PlayerControllerNumber) < 0)
        {
			character.GetComponent<Rigidbody>().velocity = character.transform.forward * maxMoveSpeed * Input.GetAxis("Vertical" + (int)player.PlayerControllerNumber);
			character.GetComponent<AudioSource> ().pitch = 1.0f + Mathf.Abs (Input.GetAxis ("Vertical" + (int)player.PlayerControllerNumber));
        }
		if (Input.GetAxis("Horizontal" + (int)player.PlayerControllerNumber) < 0 || Input.GetAxis("Horizontal" + (int)player.PlayerControllerNumber) > 0)
        {
			character.transform.rotation = character.transform.rotation * Quaternion.Euler(0, maxTurnSpeed * Input.GetAxis("Horizontal" + (int)player.PlayerControllerNumber), 0);
        }
        if (Input.GetButton("ActionButtonLeft" + (int)player.PlayerControllerNumber))
        {   
            cannon.localRotation = cannon.localRotation * Quaternion.Euler(0, -maxTurnSpeed, 0);
        }
        if (Input.GetButton("ActionButtonRight" + (int)player.PlayerControllerNumber))
        {
            cannon.localRotation = cannon.localRotation * Quaternion.Euler(0, maxTurnSpeed, 0);
        }
        if (Input.GetButtonDown("UseButton" + (int)player.PlayerControllerNumber))
        {
            if (player.weaponHasAmmo())
            {
                //weapon.fire(cannon.forward);
                player.Fire(cannon.forward);
                player.removeWeaponAmmo(1);
            }
        }
        if (player.CurrentAbility.AbilityType != Abilities.None)
        {
            player.CurrentAbility.Duration -= Time.deltaTime;
            if (player.CurrentAbility.Duration < 0)
            {
                if (player.CurrentAbility.AbilityType == Abilities.Boost)
                {
                    maxMoveSpeed = GameData.PlayerStartMoveSpeed;
                    maxTurnSpeed = GameData.PlayerStartTurnSpeed;
                }
                if (player.CurrentAbility.AbilityType == Abilities.Shield)
                {
                    player.setShield(false);
                }
                player.CurrentAbility = new Ability();
            }
        }
        //if (Input.GetButtonDown("ShoulderButtonLeft" + (int)player.PlayerControllerNumber))
        //{
        //    weapon.Switch(-1);
        //}
        //if (Input.GetButtonDown("ShoulderButtonRight" + (int)player.PlayerControllerNumber))
        //{
        //    weapon.Switch(1);
        //}
    }

    public override void Shoot() { }
    public override void look() { }
}