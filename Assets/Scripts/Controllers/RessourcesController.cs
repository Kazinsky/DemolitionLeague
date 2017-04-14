using UnityEngine;
using System.Collections;

public class RessourcesController{

    //Ressources
    public Sprite[] PlayerIcons { get; private set; }
    public Sprite[] AbilityIcons { get; private set; }
    public Sprite[] WeaponIcons { get; private set; }
    public Sprite[] PlayerColors { get; private set; }

    /**
 * Used to initialize the Ressources for the first time
 */
    public void Initialize()
    {
        PlayerIcons = Resources.LoadAll<Sprite>("Sprites/PlayerIcons");
        AbilityIcons = Resources.LoadAll<Sprite>("Sprites/AbilityIcons");
        WeaponIcons = Resources.LoadAll<Sprite>("Sprites/WeaponIcons");
        PlayerColors = Resources.LoadAll<Sprite>("Sprites/playerColors");

        Debug.Log("AbilityIcons.Length"+ AbilityIcons.Length);
    }
  
}
