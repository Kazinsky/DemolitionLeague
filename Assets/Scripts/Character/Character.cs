using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats {
    int health;
}

public abstract class Character : MonoBehaviour {

    #region CHARACTER_ATTRIBUTES
    CharacterStats stats;
    //List<Level> levels;
    //State state;
    GameSettings settings;
    #endregion

    #region MonoBehaviour_FUNCTIONS
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
            ;
    }
    #endregion

    #region Public_FUNCTIONS
    public void takeDamage() {

    }
    #endregion
}
