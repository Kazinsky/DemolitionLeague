using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerNetTest : NetworkedCharacter, IPunObservable
{

    //For Networking
    public static GameObject localPlayerInstance;

    //UI
    public GameObject PlayerUIPrefab;

    //UI
    public GameObject BulletPrefab;

    [SerializeField]
    private string playerName;

    [SerializeField]
    private int playerNumber;

    [SerializeField]
    private int playerModelNumber;

    [SerializeField]
    private Ability currentAbility;

    [SerializeField]
    private Weapon currentWeapon;

    [SerializeField]
    private PlayerColor playerColor;

    [SerializeField]
    private PlayerControllerNumber playerControllerNumber = PlayerControllerNumber.Player1;

    [SerializeField]
    private bool localPlayer;

    private int score;

    private Game parentGame;

    private PlayerController playerController;

    //Initialisers In case they are needed to set up values
    public void Initialize()
    {
        playerNumber = 0;
        playerModelNumber = 0;
        playerStats = new CharacterStats(0, 0, 0);
        currentAbility = new Ability();
        currentWeapon = new Weapon();
        playerColor = PlayerColor.Red;
    }


    public void Initialize(int playerNumber, int playerModelNumber,CharacterStats stats, Ability ability, Weapon weapon, PlayerColor color, Game parentGame)
    {
        this.parentGame = parentGame;
        this.playerNumber = playerNumber;
        this.playerModelNumber = playerModelNumber;
        playerStats = stats;
        currentAbility = ability;
        currentWeapon = weapon;
        playerColor = color;
    }

    void Awake()
    {
        //Static instance of the Local Player GameObject
        if (photonView.isMine)
            PlayerNetworked.localPlayerInstance = this.gameObject;

        //Don't destroy When you load a new scene
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    public override void Start () {

        base.Start();

        if (localPlayer)
        {
            //by default set to local player
            setUpPlayerController(new Networked(gameObject));
        }

        //UI
        if (PlayerUIPrefab != null)
        {
            GameObject ui = Instantiate(PlayerUIPrefab) as GameObject;

            //Call the SetTarget Method of this prefab and send it this player
            ui.SendMessage("SetTarget", gameObject, SendMessageOptions.RequireReceiver);
        }
    }
	
	// Update is called once per frame
	void Update () {

        //If my client controls this player
        if (photonView.isMine)
        {
            if (playerController != null)
                playerController.moveInput();
        }
    }

    //Network message call
    [PunRPC]
    void DestroyPhotonView(int photonViewId)
    {
        if (PhotonNetwork.isMasterClient)
            PhotonNetwork.Destroy(PhotonView.Find(photonViewId));
    }

    //Networking
    //Send updates of change of certain data
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //Send My client
            stream.SendNext(PlayerStats.Health);
        }
        else
        {
            //Receive other users data
            PlayerStats.Health = (int)stream.ReceiveNext();
        }
    }

    public void setUpPlayerController(PlayerController controller)
    {
        playerController = controller;
    }

    /*Functions To Alter Stats */
    public override void removeHealth(int value)
    {
        if (photonView.isMine){
        base.removeHealth(value);

        if (parentGame != null)
            parentGame.PlayerUpdateHealth(playerNumber, playerStats.Health);
        }
    }

    public override void addHealth(int value)
    {
        if (photonView.isMine)
        {
            base.addHealth(value);

            if (parentGame != null)
                parentGame.PlayerUpdateHealth(playerNumber, playerStats.Health);
        }
    }

    public override void removeMaxHealth(int value)
    {
        if (photonView.isMine)
        {
            base.removeMaxHealth(value);

            if (parentGame != null)
                parentGame.PlayerUpdateMaxHealth(playerNumber, playerStats.MaxHealth);
        }
    }

    public override void addMaxHealth(int value)
    {
        if (photonView.isMine)
        {
            base.addMaxHealth(value);

            if (parentGame != null)
                parentGame.PlayerUpdateMaxHealth(playerNumber, playerStats.MaxHealth);
        }
    }


    /*Functions To Alter ammo count */
    public void removeWeaponAmmo(int num)
    {
        if (photonView.isMine)
        {
            if (currentWeapon.AmmoCount != Infinity.InfinityValue())
            {
                currentWeapon.AmmoCount -= num;

                if (parentGame != null)
                    parentGame.PlayerUpdateAmmoCount(playerNumber, currentWeapon.AmmoCount);
            }
        }
       
    }

    public void addWeaponAmmo(int num)
    {
        if (photonView.isMine)
        {
            if (currentWeapon.AmmoCount != Infinity.InfinityValue())
            {
                currentWeapon.AmmoCount += num;

                if (parentGame != null)
                    parentGame.PlayerUpdateAmmoCount(playerNumber, currentWeapon.AmmoCount);
            }
        }
    }

    public void removeScore(int value)
    {
        Score -= value;
    }

    public  void addScore(int value)
    {
        Score += value;
    }

    public bool weaponHasAmmo()
    {
        if (CurrentWeapon.AmmoCount > 0)
            return true;
        else
            return false;
    }


    /* Damage */

    /* Damage Trigger */

    //For Trigger Collisions
    public override void OnTriggerEnter(Collider other)
    {
        if (photonView.isMine)
        {
            base.OnTriggerEnter(other);

            if (other.gameObject.layer == LayerMask.NameToLayer("Specials"))
            {
                AbilityObject ability;
                WeaponObject weapon;

                if (ability = other.gameObject.GetComponent<AbilityObject>())
                {
                    CurrentAbility = ability.Ability;
                }
                else if (weapon = other.gameObject.GetComponent<WeaponObject>())
                {
                    CurrentWeapon = weapon.Weapon;
                }

                    photonView.RPC("DestroyPhotonView", PhotonTargets.All, other.gameObject.GetComponent<PhotonView>().viewID);
            }

        }
    }

    //For typical Collisions
    public override void OnCollisionEnter(Collision other)
    {
        if (photonView.isMine)
        {
            base.OnCollisionEnter(other);

            if (other.gameObject.layer == LayerMask.NameToLayer("Specials"))
            {
                AbilityObject ability;
                WeaponObject weapon;

                if (ability = other.gameObject.GetComponent<AbilityObject>())
                {
                    CurrentAbility = ability.Ability;
                }
                else if (weapon = other.gameObject.GetComponent<WeaponObject>())
                {
                    CurrentWeapon = weapon.Weapon;
                }
            }
        }
    }

    public override void OnParticleCollision(GameObject other)
    {
        if (photonView.isMine)
        {
            base.OnParticleCollision(other);

            if (other.gameObject.layer == LayerMask.NameToLayer("Specials"))
            {
                AbilityObject ability;
                WeaponObject weapon;

                if (ability = other.gameObject.GetComponent<AbilityObject>())
                {
                    CurrentAbility = ability.Ability;
                }
                else if (weapon = other.gameObject.GetComponent<WeaponObject>())
                {
                    CurrentWeapon = weapon.Weapon;
                }
            }
        }
    }

    public int PlayerNumber
    {
        get
        {
            return playerNumber;
        }

        set
        {
            playerNumber = value;
            UpdatePlayer();
        }
    }

    public override CharacterStats PlayerStats
    {
        get
        {
            return base.PlayerStats;
        }

        set
        {
            base.PlayerStats = value;
            UpdatePlayer();
        }
    }

    public Ability CurrentAbility
    {
        get
        {
            return currentAbility;
        }

        set
        {
            currentAbility = value;

            if (parentGame != null)
                parentGame.PlayerUpdateAbility(playerNumber, currentAbility);
        }
    }

    public Weapon CurrentWeapon
    {
        get
        {
            return currentWeapon;
        }

        set
        {
            currentWeapon = value;

            if (parentGame != null)
            {
                parentGame.PlayerUpdateWeapon(playerNumber, currentWeapon);
                parentGame.PlayerUpdateAmmoCount(playerNumber, currentWeapon.AmmoCount);
            }
                
        }
    }

    public PlayerColor PlayerColor
    {
        get
        {
            return playerColor;
        }

        set
        {
            playerColor = value;

            if (parentGame != null)
                parentGame.PlayerUpdateColor(playerNumber, playerColor);
        }
    }

    public Game ParentGame
    {
        get
        {
            return parentGame;
        }

        set
        {
            parentGame = value;
        }
    }

    public int PlayerModelNumber
    {
        get
        {
            return playerModelNumber;
        }

        set
        {
            playerModelNumber = value;

            if(parentGame != null)
            parentGame.PlayerUpdateModel(playerNumber, playerModelNumber);
        }
    }

    public string PlayerName
    {
        get
        {
            return playerName;
        }

        set
        {
            playerName = value;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public PlayerControllerNumber PlayerControllerNumber
    {
        get
        {
            return playerControllerNumber;
        }

        set
        {
            playerControllerNumber = value;
        }
    }

    public void UpdatePlayer()
    {
        if (photonView.isMine)
        {
        }
    }

    public void fire()
    {
        PhotonNetwork.Instantiate(BulletPrefab.name, transform.position, Quaternion.identity, 0);
    }
}
