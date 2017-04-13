using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour {
	[SerializeField]
	private GameObject playerPrefab;

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
