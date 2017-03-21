using UnityEngine;
using System.Collections;

/// <summary>
/// Holds a Weapon Attributes, is Serializable
/// </summary>
[System.Serializable]
public class Weapon{

    [SerializeField]
    private int ammoCount;

    [SerializeField]
    private Weapons weaponType;


    public Weapon()
    {
        AmmoCount = Infinity.InfinityValue();
        WeaponType = Weapons.Default;
    }


    public int AmmoCount
    {
        get
        {
            return ammoCount;
        }

        set
        {
            ammoCount = value;
        }
    }

    public Weapons WeaponType
    {
        get
        {
            return weaponType;
        }

        set
        {
            weaponType = value;
        }
    }
}
