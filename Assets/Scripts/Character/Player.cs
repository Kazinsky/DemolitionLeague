using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    #region PLAYER_ATTRIBUTES
    string playerName;
    int id;
    bool isAlive;
    int score;
    PlayerControler controler;

    public string PlayerName
    {
        set { playerName = value; }
        get { return playerName; }
    }
    public int Id
    {
        set { id = value; }
        get { return id; }
    }
    public bool IsAlive
    {
        set { isAlive = value; }
        get { return isAlive; }
    }
    public int Score
    {
        set { score = value; }
        get { return score; }
    }
    #endregion

    #region MonoBehaviour_FUNCTIONS
    // Use this for initialization
    void Start () {
        set(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        controler.moveInput();
    }

    #endregion

    #region Public_FUNCTIONS
    public void set(GameObject gameObject)
    {
        controler = new Local(gameObject);
    }
    #endregion
}
