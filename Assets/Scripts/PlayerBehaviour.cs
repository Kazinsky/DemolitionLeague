using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    // Use this for initialization
    void Start () {
	
	}

    static Plane XZPlane = new Plane(Vector3.up, Vector3.zero);

    public static Vector3 GetMousePositionOnXZPlane()
    {
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (XZPlane.Raycast(ray, out distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            //Just double check to ensure the y position is exactly zero
            hitPoint.y = 0;
            return hitPoint;
        }
        return Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate () {
        //Vector3 pos = GetMousePositionOnXZPlane();
        //GetComponent<Rigidbody>().MovePosition(new Vector3(pos.x, 10.0f, pos.z));
        Vector3 movedir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        GetComponent<Rigidbody>().AddForce(movedir * 10f);
    }
}
