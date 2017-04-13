using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawner : MonoBehaviour {

	[SerializeField]
	private GameObject[] abilitiesPrefab = new GameObject[1];

	[SerializeField]
	private GameObject[] weaponsPrefab = new GameObject[1];

	[SerializeField]
	private float maxNextSpawn;

	private Transform possibleSpawns;
	private float nextSpawn;

	// Use this for initialization
	void Start () {
		possibleSpawns = GameObject.Find ("DropSpawns").transform;
		nextSpawn = Random.Range (0.0f, maxNextSpawn);
	}
	
	// Update is called once per frame
	void Update () {
		nextSpawn -= Time.deltaTime;

		if (nextSpawn <= 0.0f) {
			int spawnLocation = Random.Range (0, possibleSpawns.childCount);
			Transform spawnTransform = possibleSpawns.GetChild (spawnLocation);

			if (Random.Range (0, 2) == 0) {
				Instantiate (abilitiesPrefab [Random.Range (0, abilitiesPrefab.Length)], spawnTransform.position, Quaternion.identity);
			} else {
				Instantiate (weaponsPrefab [Random.Range (0, weaponsPrefab.Length)], spawnTransform.position, Quaternion.identity);
			}
			nextSpawn = Random.Range (0.0f, maxNextSpawn);
		}
	}
}
