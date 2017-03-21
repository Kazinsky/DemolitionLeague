using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    private HudController hudController;

    private List<Player> playersInGame;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetUpGame(HudController hud, RessourcesController ressourcesController, List<Player> players) {
        HudController = hud;
        PlayersInGame = players;
        hudController.Initialize(ressourcesController,playersInGame);
    }

    //Used to signal to Hud that a players Values have been Changes. Signal update.
    public void PlayerUpdate(Player player)
    {
       if(hudController!= null)
        hudController.PlayerUpdate(player);
    }

    //Updates Model
    public void PlayerUpdateModel(int playerNumber, int playerModel)
    {
        hudController.ChangePlayerIcon(playerNumber, playerModel);
    }

    //Updates Color
    public void PlayerUpdateColor(int playerNumber, PlayerColor playerColor)
    {
        hudController.ChangePlayerColor(playerNumber, playerColor);
    }

    //Updates Health
    public void PlayerUpdateHealth(int playerNumber, int health)
    {
        hudController.ChangeCurrentHealthValue(playerNumber, health);
    }

    //Updates Max Health
    public void PlayerUpdateMaxHealth(int playerNumber, int health)
    {
        hudController.ChangeMaxHealthValue(playerNumber, health);
    }

    //Updates Ability
    public void PlayerUpdateAbility(int playerNumber, Ability ability)
    {
        hudController.ChangeAbilityIcon(playerNumber, ability);
    }

    //Updates Weapon
    public void PlayerUpdateWeapon(int playerNumber, Weapon weapon)
    {
        hudController.ChangeWeaponIcon(playerNumber, weapon);
    }

    //Updates Ammo count
    public void PlayerUpdateAmmoCount(int playerNumber, int ammo)
    {
        hudController.ChangeAmmoCount(playerNumber, ammo);
    }

    public HudController HudController
{
    get
    {
        return hudController;
    }

    set
    {
        hudController = value;
    }
}

public List<Player> PlayersInGame
{
    get
    {
        return playersInGame;
    }

    set
    {
        playersInGame = value;
    }
}
}
