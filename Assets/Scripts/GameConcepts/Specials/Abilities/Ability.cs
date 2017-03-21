using UnityEngine;
using System.Collections;

/// <summary>
/// Holds a Ability Attributes, is Serializable
/// </summary>
[System.Serializable]
public class Ability{

    [SerializeField]
    private Abilities abilityType;

    [SerializeField]
    private int numOfUse;

    public Ability()
    {
        AbilityType = Abilities.None;
        numOfUse = 0;
    }

    public Ability(Abilities ability, int numOfuses)
    {
        AbilityType = ability;
        numOfUse = numOfuses;
    }

    public Abilities AbilityType
    {
        get
        {
            return abilityType;
        }

        set
        {
            abilityType = value;
        }
    }

    public int NumOfUse
    {
        get
        {
            return numOfUse;
        }

        set
        {
            numOfUse = value;
        }
    }
}
