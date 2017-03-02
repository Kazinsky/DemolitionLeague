
public class Ability{

    private Abilities abilityType;

    public Ability()
    {
        AbilityType = Abilities.None;
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
}
