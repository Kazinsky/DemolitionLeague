
/// <summary>
/// Used To store and Damage over time Attacks, is Serializable
/// </summary>
[System.Serializable]
public class DamageOverTime{

    public bool IsDamageOvertime;

    //Damage per Interval
    public int Damage;

    //Total duration
    public float Duration;

    //Damage intervals
    public float Intervals;

    public int DamageInstances { get; set; }

    public float totalDuration { get; set; }

}
