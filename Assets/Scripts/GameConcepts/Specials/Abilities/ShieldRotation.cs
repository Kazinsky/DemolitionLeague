﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation *= Quaternion.Euler(2,2,2);
	}
}