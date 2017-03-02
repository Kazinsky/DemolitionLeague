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
