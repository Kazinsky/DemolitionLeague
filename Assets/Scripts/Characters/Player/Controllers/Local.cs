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
        if (Input.GetKey(KeyCode.W))
        {
            character.GetComponent<Rigidbody>().velocity = character.transform.forward * maxMoveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            character.GetComponent<Rigidbody>().velocity = -character.transform.forward * maxMoveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            character.transform.rotation = character.transform.rotation * Quaternion.Euler(0, -maxTurnSpeed, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            character.transform.rotation = character.transform.rotation * Quaternion.Euler(0, maxTurnSpeed, 0);
        }
        if (Input.GetKey(KeyCode.Z))
        {   
            cannon.localRotation = cannon.localRotation * Quaternion.Euler(0, -maxTurnSpeed, 0);
        }
        if (Input.GetKey(KeyCode.C))
        {
            cannon.localRotation = cannon.localRotation * Quaternion.Euler(0, maxTurnSpeed, 0);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (player.weaponHasAmmo())
            {
                weapon.fire(cannon.forward);
                player.removeWeaponAmmo(1);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            weapon.Switch(-1);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            weapon.Switch(1);
        }
    }
}