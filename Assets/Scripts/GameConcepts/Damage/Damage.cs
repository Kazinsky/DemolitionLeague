
/// <summary>
/// Used to store Damage
/// </summary>
public class Damage {

    public int DamageValue { get; set; }
    public DamageType damageType { get; set; }


    //Constructor
    public Damage()
    {
        DamageValue = 0;
    }

    //Constructor
    public Damage(int damage)
    {
        DamageValue = damage;
    }

    //Constructor
    public Damage(int damage, DamageType type)
    {
        DamageValue = damage;
        damageType = type;
    }

}
