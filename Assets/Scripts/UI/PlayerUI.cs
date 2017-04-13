using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUI : MonoBehaviour {

    GameObject player;
    PlayerNetTest playerObject;
    public Vector3 offset = new Vector3(0f, 3f, 3f);

    public Text PlayerNameText;


    void Awake()
    {
        this.GetComponent<Transform>().SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
    }

    public void SetTarget(GameObject target)
    {
        player = target;

        playerObject = player.GetComponent<PlayerNetTest>();

        if (PlayerNameText != null)
            PlayerNameText.text = playerObject.photonView.owner.NickName;
    }

    void Update()
    {

        if(player == null)
        {
            Destroy(gameObject);
            return;
        }

    }

    void LateUpdate()
    {
        if(player != null)
            this.transform.position = player.transform.position + offset;
    }

}
