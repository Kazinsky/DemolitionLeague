using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : MonoBehaviour {

    public Weapon weapon;

    private int index;

    public GameObject[] bullets;

    public Hashtable search;

    public void fire(Vector3 direction,Weapon current)
    {
        GameObject temp = Instantiate(getBulletObject(current));

        temp.transform.position = transform.parent.position +
            transform.parent.transform.GetChild(0).transform.localPosition.z * transform.parent.forward
            ;
        temp.transform.forward = direction;
        temp.GetComponent<Rigidbody>().velocity = direction.normalized
            * temp.GetComponent<BulletInterface>().Speed;
    }

    public Weapon Weapon
    {
        get
        {
            return weapon;
        }

        set
        {
            weapon = value;
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            Destroy(gameObject);
    }

    public GameObject getBulletObject(Weapon current)
    {
        if (current.WeaponType == Weapons.Default)
        {
            return bullets[0];
        }
        if (current.WeaponType == Weapons.Rocket)
        {
            return bullets[1];
        }

        return null;
    }
}
