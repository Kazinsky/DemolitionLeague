using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour {
	int levelToLoad;
	int maxPlayers = 2;
	int nbPlayers = 1;
	int nbAi = 1;

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
	}
}
