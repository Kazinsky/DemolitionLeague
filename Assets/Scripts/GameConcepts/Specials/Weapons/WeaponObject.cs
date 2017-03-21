using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : MonoBehaviour {

    [SerializeField]
    private Weapon weapon;

    private int index;

    public GameObject[] bullets;

    public Hashtable search;


    public void Switch(int i)
    {
        index = (index + i) % bullets.Length;
        if (index < 0) { index = bullets.Length - 1; }
    }

    public void fire(Vector3 direction)
    {
        GameObject temp = Instantiate(bullets[index]);
        temp.transform.position = transform.parent.position;
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
}
