using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour {

    //Key for player name for networking
    static string playerNamePrefKey = "PlayerName";

	// Use this for initialization
	void Start () {

        string name = "";

        InputField inputField = this.GetComponent<InputField>();

        if(inputField != null)
        {
            //Check if player already has a name from a previous session
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                name = PlayerPrefs.GetString(playerNamePrefKey);
                inputField.text = name;
            }
        }


        PhotonNetwork.playerName = name;
	
	}

    public void SetPlayerName(string name)
    {
        PhotonNetwork.playerName = name + " ";

        PlayerPrefs.SetString(playerNamePrefKey, name);
    }
	
}
