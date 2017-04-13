using UnityEngine;

public class Launcher : Photon.PunBehaviour {

    //Client's version number. Used to tell users appart from each other
    string gameVersion = "1";
    //Used to know if a user is trying to connect
    bool userTryingToConnect;

    public PhotonLogLevel logLevel = PhotonLogLevel.Informational;

    public byte MaxNumberOfPlayersPerRoom = 4;

    public string levelToLoad = "Loby";
    //UI Elements
    public GameObject controlPanel;
    public GameObject connectionProgressLabel;

    void Awake()
    {
        //Initial settings
        PhotonNetwork.autoJoinLobby = false;
        //Can use this to sync all connected users to the same scene
        PhotonNetwork.automaticallySyncScene = true;

        //Loging setting
        PhotonNetwork.logLevel = logLevel;
    }


	// Use this for initialization
	void Start () {
        //Set UI elements
        connectionProgressLabel.SetActive(false);
        controlPanel.SetActive(true);
	}

    /// <summary>
    /// Try to connect To the Photon cloud network or if already connected then join a room
    /// </summary>
    public void Connect()
    {
        userTryingToConnect = true;

        connectionProgressLabel.SetActive(true);
        controlPanel.SetActive(false);

        if (PhotonNetwork.connected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings(gameVersion);
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");

        if(userTryingToConnect)
            PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("User joined a room");

        //If we are the first player to join
        if(PhotonNetwork.room.PlayerCount == 1)
        {
            Debug.Log("Load Level: " + levelToLoad);
            PhotonNetwork.LoadLevel(levelToLoad);
        }

    }

    public override void OnDisconnectedFromPhoton()
    {
        connectionProgressLabel.SetActive(false);
        controlPanel.SetActive(true);

        Debug.LogWarning("Disconnected from Photon Server");
    }


    // If failure to join a random room then we need to create a new one
    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("No rooms to connect to, creating a new Room");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxNumberOfPlayersPerRoom }, null);
        
    }
}
