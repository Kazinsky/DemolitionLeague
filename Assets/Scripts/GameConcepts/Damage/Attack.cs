
/// <summary>
/// Used To store and Attack, is Serializable
/// </summary>
[System.Serializable]
public class Attack{

    /*Stats are all Public so they are visible in Unity Editor*/

    //Base Damage of the Attack
    public int BaseDamage;
        
    //Delay between attacks
    public float Cooldown;

    //Maximum range of an Attack
    public float Range;

    //Physical Force of an Attack, can cause push back to a target
    public float Force;

    //Used for if we want to add elemental types of damage.
    public DamageType DamageType;

    public DamageOverTime DamageOvertime;
}
