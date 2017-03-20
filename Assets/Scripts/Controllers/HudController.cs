using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class HudController : MonoBehaviour {

    [SerializeField]
    private List<PlayerUIElement> playersInfoUI;

    [SerializeField]
    private Timer timer;

    private RessourcesController ressourcesController;

    private int numPlayers;

    private const int MAX_NUM_PLAYERS= 4;
    private const int defaultTextFontSize = 20;
    private const int largerTextFontSize  = 29;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    /**
     * Used to initialize the Hud for the first time
     */
    public void Initialize(RessourcesController ressourcesController, List<Player> players)
    {
        this.ressourcesController = ressourcesController;

        int numOfPlayers = 0;

        //Check if list provided of players is more then the maximum allowed, if it is then just take the ones up to it's max
        if (players.Count > MAX_NUM_PLAYERS)
            numOfPlayers = MAX_NUM_PLAYERS;
        else
            numOfPlayers = players.Count;

        setNumOfPlayers(numOfPlayers);

        //For each player Set up initial Values
        for(int i=0; i < numOfPlayers; i++)
        {
            PlayerUpdate(players[i]);
        }

        timer.Run = true;

    }

    //Updates the UI with new values of the Player uses Player Number to determine which player to update
    public void PlayerUpdate(Player player)
    {
        ChangePlayerIcon(player.PlayerNumber, player.PlayerModelNumber);

        ChangePlayerColor(player.PlayerNumber, player.PlayerColor);

        ChangeCurrentHealthValue(player.PlayerNumber, player.PlayerStats.Health);

        ChangeMaxHealthValue(player.PlayerNumber, player.PlayerStats.MaxHealth);

        ChangeAbilityIcon(player.PlayerNumber, player.CurrentAbility);

        ChangeWeaponIcon(player.PlayerNumber, player.CurrentWeapon);

        ChangeAmmoCount(player.PlayerNumber, player.CurrentWeapon.AmmoCount);
    }

    //Updates Model
    public void PlayerUpdateModel(int playerNumber, int playerModel)
    {
        ChangePlayerIcon(playerNumber, playerModel);
    }

    //Updates Color
    public void PlayerUpdateColor(int playerNumber, PlayerColor playerColor)
    {
        ChangePlayerColor(playerNumber, playerColor);
    }

    //Updates Health
    public void PlayerUpdateHealth(int playerNumber, int health)
    {
        ChangeCurrentHealthValue(playerNumber, health);
    }

    //Updates Max Health
    public void PlayerUpdateMaxHealth(int playerNumber, int health)
    {
        ChangeMaxHealthValue(playerNumber, health);
    }

    //Updates Ability
    public void PlayerUpdateAbility(int playerNumber, Ability ability)
    {
        ChangeAbilityIcon(playerNumber, ability);
    }

    //Updates Weapon
    public void PlayerUpdateWeapon(int playerNumber, Weapon weapon)
    {
        ChangeWeaponIcon(playerNumber, weapon);
    }

    //Updates Ammo count
    public void PlayerUpdateAmmoCount(int playerNumber, int ammo)
    {
        ChangeAmmoCount(playerNumber, ammo);
    }
     

    /*
     * Updates the number of players for the Hud
     */
    private void setNumOfPlayers(int num)
    {
        numPlayers = num;
        hudSetup();
    }

    /*
     * Sets up the Hud based on the players in the game
     */
    private void hudSetup()
    {

    }

    /* Alter Values */

    public void ChangeCurrentHealthValue(int number, float health)
    {
        playersInfoUI[number].ChangeCurrentHealthValue(health);
    }

    public void ChangeMaxHealthValue(int number, float health)
    {
        playersInfoUI[number].ChangeMaxHealthValue(health);
    }

    public void ChangePlayerIcon(int number, int modelNumber)
    {
        if (modelNumber < ressourcesController.PlayerIcons.Length)
            playersInfoUI[number].ChangePlayerIcon(ressourcesController.PlayerIcons[modelNumber]);
        else
            Debug.Log("Could not Change Player Icon");
    }

    public void ChangePlayerColor(int number, PlayerColor playerColor)
    {
        if ((int)playerColor < ressourcesController.PlayerColors.Length)
            playersInfoUI[number].ChangePlayerColor(ressourcesController.PlayerColors[(int)playerColor]);
        else
            Debug.Log("Could not Change Player Color");
    }

    public void ChangeAbilityIcon(int number, Ability ability)
    {
        if ((int)ability.AbilityType < ressourcesController.AbilityIcons.Length)
            playersInfoUI[number].ChangeAbilityIcon(ressourcesController.AbilityIcons[(int)ability.AbilityType]);
        else
            Debug.Log("Could not Change Ability Icon");
    }


    public void ChangeWeaponIcon(int number, Weapon weapon)
    {

        if ((int)weapon.WeaponType < ressourcesController.WeaponIcons.Length)
            playersInfoUI[number].ChangeWeaponIcon(ressourcesController.WeaponIcons[(int)weapon.WeaponType]);
        else
            Debug.Log("Could not Change Weapon Icon");
    }

    public void ChangeAmmoCount(int number, int textNumber)
    {

        if (!(textNumber == Infinity.InfinityValue()))
        {
            if (number < 100)
                playersInfoUI[number].ChangeAmmoCount(textNumber.ToString(), defaultTextFontSize);
            else
                Debug.Log("Ammo text is too large");
        }
        else
            playersInfoUI[number].ChangeAmmoCount("∞", largerTextFontSize);
    }

}
