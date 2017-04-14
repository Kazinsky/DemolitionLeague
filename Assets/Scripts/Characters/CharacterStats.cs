using UnityEngine;
using System.Collections;

/// <summary>
/// Holds a Character's Attributes, is Serializable
/// </summary>
[System.Serializable]
public class CharacterStats{
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private int health;

    [SerializeField]
    private float movementSpeed;

    private bool shield = false;

    public bool Shield
    {
        get { return shield; }
        set { shield = value; }
    }


    public CharacterStats()
    {
        MaxHealth = 0;
        Health = 0;
        MovementSpeed = 0;
    }

    public CharacterStats(int health, int maxHealh, float movementSpeed)
    {
        MaxHealth = health;
        Health = maxHealth;
        MovementSpeed = movementSpeed;
    }

    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }

        set
        {
            maxHealth = value;
        }
    }

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public float MovementSpeed
    {
        get
        {
            return movementSpeed;
        }

        set
        {
            movementSpeed = value;
        }
    }
}
