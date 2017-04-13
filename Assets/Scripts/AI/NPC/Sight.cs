using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sets the target of it's parent when a player enters its field of view
public class Sight : MonoBehaviour {

    public BasicNPC parentAi;

    void OnTriggerEnter(Collider other)
    {
        if (parentAi != null)
        {
            if (parentAi.photonView.isMine)
            {
                if (other.tag == "Player")
                {

                    parentAi.setTarget(other.gameObject);
 
                }
            }

        }

    }

}
