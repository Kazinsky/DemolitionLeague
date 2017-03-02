
public class Weapon {

    private int ammoCount;

    private Weapons weaponType;


    public Weapon()
    {
        AmmoCount = 99;
        WeaponType = Weapons.Rocket;
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
