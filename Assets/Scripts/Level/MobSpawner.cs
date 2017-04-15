using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour {
	[SerializeField]
	private GameObject mobPrefab;

	[SerializeField]
	private float timerSpawn;

	private Transform possibleSpawns;
	private float nextSpawn;

	// Use this for initialization
	void Start () {
		possibleSpawns = GameObject.Find ("MobSpawns").transform;
		nextSpawn = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		nextSpawn += Time.deltaTime;
		if (nextSpawn >= timerSpawn) {
			int spawnLocation = Random.Range (0, possibleSpawns.childCount);
			Transform spawn = possibleSpawns.GetChild (spawnLocation);

			Instantiate (mobPrefab, spawn.position, Quaternion.identity);
			nextSpawn = 0.0f;
		}
	}
}
