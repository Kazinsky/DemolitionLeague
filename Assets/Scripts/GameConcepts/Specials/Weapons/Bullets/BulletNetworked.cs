using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNetworked : Photon.PunBehaviour
{

    void update()
    {
        transform.position += transform.forward * Time.deltaTime;
    }

     void OnTriggerEnter(Collider other)
    {

        if (photonView.isMine)
        {
            photonView.RPC("DestroyPhotonView", PhotonTargets.All, this.gameObject.GetComponent<PhotonView>().viewID);
        }
    }

    //Network message call
    [PunRPC]
    void DestroyPhotonView(int photonViewId)
    {
            PhotonNetwork.Destroy(PhotonView.Find(photonViewId));
    }
}
