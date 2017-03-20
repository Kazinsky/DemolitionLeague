using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    #region Weapon_ATTRIBUTES
    private int index;

    public GameObject[] bullets;
    public Hashtable search;
    public float offset;
    #endregion

    #region MonoBehaviour_FUNCTIONS
    // Use this for initialization
    private void Start () {
		
	}

    private void Awake()
    {   
        if(transform.parent)
            offset = transform.parent.transform.localScale.z / 2.0f;
    }

    // Update is called once per frame
    private void Update () {
		
	}
    #endregion

    #region Public_FUNCTIONS
    public void Switch(int i)
    {
        index = (index + i) % bullets.Length;
        if (index < 0) { index = bullets.Length - 1; }   
    }

    public void fire(Vector3 direction)
    {
        GameObject temp = Instantiate(bullets[index]);
        temp.transform.position = transform.parent.position + offset* direction;
        temp.transform.forward = direction;
        temp.GetComponent<Rigidbody>().velocity = direction.normalized
            * temp.GetComponent<BulletInterface>().Speed;
    }
    #endregion
}