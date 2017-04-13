using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour {
	[SerializeField]
	private GameObject playerPrefab;

	[SerializeField]
	private GameObject dropDownMaxPlayers;

	[SerializeField]
	private GameObject dropDownNbPlayers;

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

	public void setMaxPlayers(int max) {
		maxPlayers = max;

		Dropdown dd = dropDownMaxPlayers.GetComponent<Dropdown> ();
		Dropdown dd2 = dropDownNbPlayers.GetComponent<Dropdown> ();

		dd.options.Clear ();
		dd2.options.Clear ();
		for (int i = 0; i < maxPlayers; ++i) {
			dd.options.Add (new Dropdown.OptionData((i + 1).ToString ()));
			dd2.options.Add (new Dropdown.OptionData ((i + 1).ToString ()));
		}

		dd.value = 0;
		dd.value = 0;
	}

	public void setNbPlayers(int players) {
		nbPlayers = players;
		nbAi = maxPlayers - nbPlayers;
	}

	public void start() {
		SceneManager.LoadScene (levelToLoad);
	}

	public void load() {
		GameObject players = GameObject.Find ("Players");
		// First load players
		for (int i = 0; i < nbPlayers; ++i) {
			GameObject player = (GameObject)Instantiate (playerPrefab);

			player.transform.parent = players.transform;
		}

		// Then load AI
		for (int i = 0; i < nbAi; ++i) {
			// TODO : Load AI prefab when done
		}

		Destroy (gameObject);
	}
}
