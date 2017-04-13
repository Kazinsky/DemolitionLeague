using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public GameObject target;
	public GameObject boundsContainer;
	private BoxCollider bounds;
    private Vector3 initialPos;

    private Vector3 center;
    private Vector3 pos;

    public float factor = 1.0f;

	// Use this for initialization
	void Start () {
		bounds = boundsContainer.GetComponent<BoxCollider> ();
        initialPos = transform.position;
		pos = bounds.center + boundsContainer.transform.position / 2;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // Convert target position to a local postition based on the center (w/ negative values)

		Vector3 center = bounds.center + boundsContainer.transform.position / 2;

        Vector3 velocity = Vector3.zero;
		Vector3 localPos = (target.transform.position - center) / factor + center;
        pos = Vector3.SmoothDamp(pos, localPos, ref velocity, .1f);
		Vector3 zoomLevel = new Vector3 (0.0f, 0.0f, 0.0f);
        //transform.position = new Vector3(pos.x, initialPos.y, initialPos.z);
        transform.position = pos - center + initialPos + zoomLevel;

		var targetRotation = Quaternion.LookRotation(localPos - transform.position);
        // Smoothly rotate towards the target point.
        Quaternion rot = Quaternion.Slerp(transform.rotation, targetRotation, .8f * Time.deltaTime);
        transform.rotation = rot;
        //transform.rotation = target.transform.rotation;
    }

    void OnDrawGizmosSelected()
    {
		bounds = boundsContainer.GetComponent<BoxCollider> ();
		Vector3 center = bounds.center + boundsContainer.transform.position / 2;
        Gizmos.color = Color.yellow;
        Vector3 localPos = (target.transform.position - center) / factor + center;
        Gizmos.DrawWireCube(localPos, new Vector3(1, 1, 1));
        Gizmos.DrawWireCube(pos, bounds.size / factor);
    }
}
