using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRecorder : MonoBehaviour {

	[SerializeField]
	private Transform players;

	private Stack<GameObject> lifeRanking;

	private int nbPlayers;
	private int alivePlayers;
	private bool checkAlive;

	// Use this for initialization
	void Start () {
		players = GameObject.Find ("Players").transform;

		DontDestroyOnLoad (gameObject);
		DontDestroyOnLoad (players.gameObject);

		lifeRanking = new Stack<GameObject> ();
		checkAlive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (checkAlive && alivePlayers <= 1) {
			GameObject winner = GameObject.FindGameObjectWithTag ("Player");
			winner.GetComponent<AudioSource> ().Stop ();
			winner.GetComponent<AudioSource> ().loop = false;

			checkAlive = false;
			lifeRanking.Push (winner);
			SceneManager.LoadScene (3);
		}
	}

	public void Initialize(int players) {
		nbPlayers = players;
		alivePlayers = nbPlayers;
	}

	public void playerDies(GameObject player) {
		lifeRanking.Push (player);
		alivePlayers -= 1;

		player.GetComponent<AudioSource> ().loop = false;
		player.GetComponent<AudioSource> ().Stop ();
	}

	public void DisplayRanking() {
		GameObject[] ranks = lifeRanking.ToArray ();
		Transform places = GameObject.Find ("PodiumSpawn").transform;

		for (int i = 0; i < ranks.Length; ++i) {
			ranks [i].transform.position = places.GetChild (i).position + new Vector3 (0, 0.7f, 0);
			ranks [i].transform.localScale *= 2;
			ranks [i].transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
			ranks [i].SetActive (true);
		}
	}
}
