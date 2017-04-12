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

        if (Input.GetAxis("Vertical" + (int)player.PlayerControllerNumber) > 0)
        {
            character.GetComponent<Rigidbody>().velocity = character.transform.forward * maxMoveSpeed;
        }
        if (Input.GetAxis("Vertical" + (int)player.PlayerControllerNumber) < 0)
        {
            character.GetComponent<Rigidbody>().velocity = -character.transform.forward * maxMoveSpeed;
        }
        if (Input.GetAxis("Horizontal" + (int)player.PlayerControllerNumber) < 0)
        {
            character.transform.rotation = character.transform.rotation * Quaternion.Euler(0, -maxTurnSpeed, 0);
        }
        if (Input.GetAxis("Horizontal" + (int)player.PlayerControllerNumber) > 0)
        {
            character.transform.rotation = character.transform.rotation * Quaternion.Euler(0, maxTurnSpeed, 0);
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
            }
        }
    }


}