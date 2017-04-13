using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NetworkController : Photon.PunBehaviour {

    static public NetworkController Instance;
    
    public GameObject playerPrefab;

    public GameObject npcPrefab;


    public Transform playerSpawn;
    public Transform npcSpawn;

    public string[] levels;

    public string mainMenuLevel = "launcher";

    public static int currentLevel = 0;

    void Start()
    {
        Instance = this;

        //If I am the master client then I initialize the Ai Ghosts
        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("Initializing NPC");
           
            PhotonNetwork.Instantiate(npcPrefab.name, npcSpawn.position, Quaternion.identity, 0);
        }

        if (playerPrefab == null)
            Debug.LogError("Network controller is missing the Player prefab");
        else
        {

            //If local player hasn't already been created
            if (PlayerNetworked.localPlayerInstance == null)
            {
                //Instantiate this Local clients Player
                PhotonNetwork.Instantiate(playerPrefab.name, playerSpawn.position, Quaternion.identity, 0);

                Debug.Log("Instantiating my Local Player");
            }
            else
                Debug.Log("New Scene Load, Player Already exists so not recreating it");            
        }

        //Add a method call to when the Unity Scene Manager loads a scene then this method will be called 
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, loadingMode) =>
        {
            this.CalledOnLevelWasLoaded(scene.buildIndex);
        };
    }

    public void LoadArena()
    {
        if(!PhotonNetwork.isMasterClient)
        {
            Debug.LogError("PhotonNetwork: We are not the Master client we can't load a level");
        }

        PhotonNetwork.LoadLevel(getNextLevel());
    }

    //Photon Network listener
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(mainMenuLevel);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    //Watchers
    public override void OnPhotonPlayerConnected(PhotonPlayer other)
    {
        Debug.Log("Player Connected:" + other.NickName);


        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("Master client connected " + PhotonNetwork.isMasterClient);
        }
    }


    public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
    {
        Debug.Log("Player Disconnected: " + other.NickName);
    }

    //Called when we load a new scene
    void CalledOnLevelWasLoaded(int level)
    {
        Debug.Log("Load scene on player was called");

        //PlayerNetworked.localPlayerInstance.transform.position = playerSpawn.transform.position;
    }

    private string getNextLevel()
    {
        NetworkController.currentLevel++;

        if (NetworkController.currentLevel >= levels.Length)
            NetworkController.currentLevel = 0;

        return levels[NetworkController.currentLevel];
    }
}
