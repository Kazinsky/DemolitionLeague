﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameContoller : MonoBehaviour {

    //Game object to hold current Games variables and it's run method, etc.
    private Game currentGame;

    private RessourcesController ressourcesController;

    [SerializeField]
    private HudController hudController;

	List<Player> players = new List<Player>();

    // Use this for initialization
    void Start () {

		//players.Clear ();
		GameObject.Find ("GameLoader").GetComponent<GameLoader> ().load (players);

        currentGame = gameObject.AddComponent<Game>();
        ressourcesController = new RessourcesController();
        setUpPlayers();

		GameObject.Find ("GameRecorder").GetComponent<GameRecorder> ().Initialize (players.Count);

        InitializeControllers();
        
        StartGame();
    }
	
    private void InitializeControllers()
    {
        DamagePopUpController.Initialize();
        ressourcesController.Initialize();
    }

    private void StartGame()
    {
        currentGame.SetUpGame(hudController, ressourcesController,  players);
    }

    private void setUpPlayers()
    {
      foreach (Player player in players)
        {
            player.ParentGame = currentGame;
        }
    }
}
