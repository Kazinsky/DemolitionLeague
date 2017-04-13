using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
	public int[] size = new int[2];
	public GameObject floorGameObject;
	public GameObject borderObject;

	[ContextMenu ("Generate Floor")]
	void GenerateFloor() {
		GameObject floorContainer = GameObject.Find ("FloorContainer");
		int xMostLeft = -15;
		int zMostBottom = -25;

		for (int z = zMostBottom; z < 20 + size [1] + (5 - size[1] % 5); z += 10) {
			for (int x = xMostLeft; x < 20 + size [0] + (5 - size[0] % 5); x += 10) {
				GameObject go = (GameObject)Instantiate (floorGameObject, new Vector3 (x, 0, z), Quaternion.identity);

				go.transform.parent = floorContainer.transform;
			}
		}
	}

	[ContextMenu ("Generate Border")]
	void GenerateBorder() {
		GameObject borderContainer = GameObject.Find ("BorderContainer");

		GameObject topWall = GameObject.Instantiate (borderObject, new Vector3 (size [0] / 2.0f, 0.5f, size [1] - 1), Quaternion.identity);
		GameObject bottomWall = GameObject.Instantiate (borderObject, new Vector3 (size [0] / 2.0f, 0.5f, -0.5f), Quaternion.identity);
		GameObject leftWall = GameObject.Instantiate (borderObject, new Vector3 (0.5f, 0.5f, size[1] / 2.0f - 0.5f), Quaternion.identity);
		GameObject rightWall = GameObject.Instantiate (borderObject, new Vector3 (size[0], 0.5f, size [1] / 2.0f - 0.5f), Quaternion.identity);

		topWall.transform.parent = borderContainer.transform;
		bottomWall.transform.parent = borderContainer.transform;
		leftWall.transform.parent = borderContainer.transform;
		rightWall.transform.parent = borderContainer.transform;

		topWall.transform.localScale = new Vector3 (size [0], 1, 1);
		bottomWall.transform.localScale = new Vector3 (size [0], 1, 1);
		leftWall.transform.localScale = new Vector3 (1, 1, size [1]);
		rightWall.transform.localScale = new Vector3 (1, 1, size [1]);
	}
}
