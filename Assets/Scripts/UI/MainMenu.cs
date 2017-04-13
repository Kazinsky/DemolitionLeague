using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	GameObject previousPage;
	GameObject currentPage;

    public void StartGame()
    {
		GameObject levelPicker = GameObject.Find ("Canvas").transform.GetChild(1).gameObject;
		GameObject mainMenu = GameObject.Find ("MainMenu_Page");

		previousPage = mainMenu;
		currentPage = levelPicker;

		levelPicker.SetActive (true);
		mainMenu.SetActive (false);
    }

    public void JoinGame()
    {
		GameObject joinPage = GameObject.Find ("Canvas").transform.GetChild(2).gameObject;
		GameObject mainMenu = GameObject.Find ("MainMenu_Page");

		previousPage = mainMenu;
		currentPage = joinPage;

		joinPage.SetActive (true);
		mainMenu.SetActive (false);
    }

	public void GameOptions() {
		GameObject gameOptions = GameObject.Find ("Canvas").transform.GetChild(3).gameObject;
		GameObject levelPicker = GameObject.Find ("LevelPicker_Page");

		previousPage = levelPicker;
		currentPage = gameOptions;

		levelPicker.SetActive (false);
		gameOptions.SetActive (true);
	}

	public void Previous() {
		currentPage.SetActive (false);
		previousPage.SetActive (true);

		currentPage = previousPage;
		previousPage = GameObject.Find ("Canvas").transform.GetChild(0).gameObject;
	}

    public void Exit()
    {
		Application.Quit ();
    }

	public void OnMaximumPlayerChanged() {
		Dropdown mpdd = GameObject.Find ("MaxPlayers_Dropdown").GetComponent<Dropdown> ();
		Dropdown dd = GameObject.Find ("NbPlayers_Dropdown").GetComponent<Dropdown> ();
		List<Dropdown.OptionData> options = mpdd.options;
		int maxSelected = mpdd.value + 2;
		int current = dd.value + 1;

		dd.options.Clear ();
		for (int i = 1; i <= maxSelected; i++) {
			dd.options.Add (new Dropdown.OptionData(i.ToString()));
		}
		dd.value = (current <= maxSelected) ? current - 1 : maxSelected - 1;

		GameLoader gl = GameObject.Find ("GameLoader").GetComponent<GameLoader> ();

		gl.setMaxPlayers (maxSelected);
		gl.setNbPlayers (current);
	}
}
