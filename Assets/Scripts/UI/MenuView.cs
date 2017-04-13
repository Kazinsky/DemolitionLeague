using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : MonoBehaviour {

	public GameObject previousView;

	public void previous() {
		gameObject.SetActive (false);
		previousView.SetActive (true);
	}
}
