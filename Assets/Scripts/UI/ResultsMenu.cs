using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find ("GameRecorder").GetComponent<GameRecorder> ().DisplayRanking ();		
	}

	public void MainMenu() {
		Destroy (GameObject.Find ("GameRecorder"));
		Destroy (GameObject.Find ("Players"));

		SceneManager.LoadScene (0);
	}
}
