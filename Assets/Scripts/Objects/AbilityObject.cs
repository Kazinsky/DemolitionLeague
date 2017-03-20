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
}
