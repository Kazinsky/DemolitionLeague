using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public class BasicBulletHandler : CollisionHandler
{
    public static void collide(Collision collision,GameObject bullet) {
        Debug.Log("hit");
    }
}

public class LaserHandler : CollisionHandler
{
    public static void collide(Collision collision, GameObject bullet)
    {
        Debug.Log("biu");
    }
}