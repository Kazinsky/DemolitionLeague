using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public GameObject target;
    public BoxCollider bounds;
    private Vector3 initialPos;

    private Vector3 center;
    private Vector3 pos;

    public float factor = 1.0f;

	// Use this for initialization
	void Start () {
        initialPos = transform.position;
        center = bounds.center;
        pos = center;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // Convert target position to a local postition based on the center (w/ negative values)


        Vector3 velocity = Vector3.zero;
        Vector3 localPos = (target.transform.position - bounds.center) / factor + bounds.center;
        pos = Vector3.SmoothDamp(pos, localPos, ref velocity, .1f);
        //transform.position = new Vector3(pos.x, initialPos.y, initialPos.z);
        transform.position = pos - bounds.center + initialPos;
        var targetRotation = Quaternion.LookRotation(localPos - transform.position);

        // Smoothly rotate towards the target point.
        Quaternion rot = Quaternion.Slerp(transform.rotation, targetRotation, .2f * Time.deltaTime);
        transform.rotation = rot;
        //transform.rotation = target.transform.rotation;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 localPos = (target.transform.position - bounds.center) / factor + bounds.center;
        Gizmos.DrawWireCube(localPos, new Vector3(1, 1, 1));
        Gizmos.DrawWireCube(pos, bounds.size / factor);
    }
}
