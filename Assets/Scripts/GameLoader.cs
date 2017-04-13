using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class GameLoader : MonoBehaviour {
	[SerializeField]
	private GameObject playerPrefab;

	[SerializeField]
	private GameObject aiPlayerPrefab;

	[SerializeField]
	private GameObject dropDownMaxPlayers;

	[SerializeField]
	private GameObject dropDownNbPlayers;

	private NavMesh navMesh;

	int levelToLoad;
	int maxPlayers = 2;
	int nbPlayers = 1;
	int nbAi = 1;

	public void Start() {
		DontDestroyOnLoad (gameObject);
	}

	public void setLevelToLoad(int level) {
		levelToLoad = level;
	}

	public void setGameMaxPlayer(int max) {
		maxPlayers = max;

		Dropdown dd = dropDownMaxPlayers.GetComponent<Dropdown> ();
		Dropdown dd2 = dropDownNbPlayers.GetComponent<Dropdown> ();

		dd.options.Clear ();
		dd2.options.Clear ();
		dd2.options.Add (new Dropdown.OptionData ("1"));

		for (int i = 1; i < maxPlayers; ++i) {
			dd.options.Add (new Dropdown.OptionData((i + 1).ToString ()));
			if (i < 2)
				dd2.options.Add (new Dropdown.OptionData ((i + 1).ToString ()));
		}

		dd.value = 0;
		dd2.value = 0;
	}

	public void setNavMesh(NavMesh nm) {
		navMesh = nm;
	}

	public void setMaxPlayers(int max) {
		maxPlayers = max;
		Dropdown dd2 = dropDownNbPlayers.GetComponent<Dropdown> ();
		int selected = dd2.value + 1;

		dd2.options.Clear();
		for (int i = 1; i <= maxPlayers; ++i) {
			dd2.options.Add(new Dropdown.OptionData(i.ToString()));
		}
		dd2.value = (selected <= maxPlayers) ? selected - 1 : maxPlayers;
	}

	public void setNbPlayers(int players) {
		nbPlayers = players;
		nbAi = maxPlayers - nbPlayers;
	}

	public void start() {
		SceneManager.LoadScene (levelToLoad);
	}

	public void load(List<Player> playersList) {
		GameObject players = GameObject.Find ("Players");
		Transform spawns = GameObject.Find ("Spawns").transform;
		int i;

		// First load players
		for (i = 0; i < nbPlayers; ++i) {
			GameObject player = (GameObject)Instantiate (playerPrefab, spawns.GetChild(i).position, Quaternion.identity);
			Player p = player.GetComponent<Player> ();

            p.PlayerNumber = i;
			p.PlayerColor = (PlayerColor)i;
			p.PlayerControllerNumber = (PlayerControllerNumber)(i + 1);
			p.PlayerNumber = i;

			player.transform.parent = players.transform;
			playersList.Add (p);
		}

		// Then load AI
		for (; i < maxPlayers; ++i) {
			GameObject player = (GameObject)Instantiate (aiPlayerPrefab, spawns.GetChild(i).position, Quaternion.identity);
			Player p = player.GetComponent<Player> ();

			p.PlayerNumber = i;
			p.PlayerColor = (PlayerColor)i;
			p.PlayerControllerNumber = (PlayerControllerNumber)(i + 1);
			p.PlayerNumber = i;

			player.transform.parent = players.transform;
			playersList.Add (p);
		}

		Destroy (gameObject);
	}
}
