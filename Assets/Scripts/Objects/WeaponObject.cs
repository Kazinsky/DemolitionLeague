using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : MonoBehaviour {

    [SerializeField]
    private Weapon weapon;

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
}
