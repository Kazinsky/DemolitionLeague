using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Local : PlayerControler {

    readonly Weapon weapon;
    Transform cannon;

    public Local(GameObject character):base(character)
    {
        maxMoveSpeed = 10;
        maxTurnSpeed = 5;
        cannon = character.transform.GetChild(1);
        weapon = Object.Instantiate(GameObject.Find("Weapon"), cannon).GetComponent<Weapon>();
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
            character.transform.rotation = character.transform.rotation * Quaternion.Euler(0, -maxTurnSpeed,0);
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
            weapon.fire(cannon.forward);
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
