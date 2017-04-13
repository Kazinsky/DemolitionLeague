using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletInterface : MonoBehaviour
{

    #region BULLET_ATTRIBUTES
    public float Speed;
    public float Damage;
	public GameObject parent;
    #endregion

    #region MonoBehaviour_FUNCTIONS
    private void Awake() { instantEffect(); }

    private void Update() { persistantEffect(); }

    protected virtual void OnCollisionEnter(Collision collision)
    {
		if (collision.gameObject != parent) {
			Destroy (gameObject);
		}
    }
    #endregion

    #region Abstract_FUNTIONS
    protected abstract void instantEffect();

    protected abstract void persistantEffect();
    #endregion
}
