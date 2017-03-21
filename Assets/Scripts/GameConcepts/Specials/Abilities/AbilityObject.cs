using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityObject : MonoBehaviour {

    [SerializeField]
    private Ability ability;

    public Ability Ability
    {
        get
        {
            return ability;
        }

        set
        {
            ability = value;
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
