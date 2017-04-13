using UnityEngine;
using System.Collections;
using System;

public class PlayerNetworked : Photon.PunBehaviour, IPunObservable
{
    //For Networking
    public static GameObject localPlayerInstance;

    //UI
    public GameObject PlayerUIPrefab;

    public float defaultMovementSpeed = 0.1f;

    public float rotationSpeed = 5f;

    public int score;

    //Movement Speed related
    private float movementSpeed;

    private float movementSpeedBoostDuration = 2.0f;

    private float movementSpeedTimer;

    Rigidbody ThisRigidBody;
    Animator ThisAnimator;

    Vector3 nextDestination;

    Vector3 rotateDirection;

    float verticalInput;
    float horizontalInput;

    bool run;

    private AudioSource audioSource;

    private Vector3 initialPosition;


    void Awake()
    {
        //Static instance of the Local Player GameObject
        if (photonView.isMine)
            PlayerNetworked.localPlayerInstance = this.gameObject;

        //Don't destroy When you load a new scene
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        initialPosition = transform.position;

        audioSource = GetComponent<AudioSource>();

        ThisRigidBody = GetComponent<Rigidbody>();

        ThisAnimator = GetComponent<Animator>();

        nextDestination = transform.position;

        movementSpeed = defaultMovementSpeed;

        score = 0;

        run = false;

        //Networking

        //Add a method call to when the Unity Scene Manager loads a scene then this method will be called 
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, loadingMode) =>
        {
            this.CalledOnLevelWasLoaded(scene.buildIndex);
        };


        //UI
        if (PlayerUIPrefab != null)
        {
            GameObject ui = Instantiate(PlayerUIPrefab) as GameObject;

            //Call the SetTarget Method of this prefab and send it this player
            ui.SendMessage("SetTarget", gameObject, SendMessageOptions.RequireReceiver);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //If my client controls this player
        if (photonView.isMine)
        {
            //Rotate always to be where player is facing
            Vector3 newDir = Vector3.RotateTowards(transform.forward, rotateDirection, rotationSpeed * Time.deltaTime, 0.0F);

            transform.rotation = Quaternion.LookRotation(newDir);

            checkSpeed();
        }
      
    }

    void FixedUpdate()
    {

        //If my client controls this player
        if (photonView.isMine)
        {
            //Move towards next destination iteratively using movement speed
            Vector3 newPosition = Vector3.MoveTowards(transform.position, nextDestination, movementSpeed);

            ThisRigidBody.MovePosition(newPosition);

            ProcessInputs();
        }

    }


    void ProcessInputs()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");


        //Check if we are already moving
        if (transform.position == nextDestination)
        {
            run = false;
            ThisAnimator.SetBool("Run", run);

            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && isValidMovement(Vector3.forward))
            {
                rotateDirection = -Vector3.forward;
                nextDestination = transform.position + Vector3.forward;
            }
                

            if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && isValidMovement(Vector3.back))
            {
                rotateDirection = -Vector3.back;
                nextDestination = transform.position + Vector3.back;
            }
                

            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && isValidMovement(Vector3.left))
            {
                rotateDirection = -Vector3.left;
                nextDestination = transform.position + Vector3.left;
            }
               

            if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && isValidMovement(Vector3.right))
            {
                rotateDirection = -Vector3.right;
                nextDestination = transform.position + Vector3.right;
            }

        }
        else
        {
            run = true;
            ThisAnimator.SetBool("Run", run);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (photonView.isMine)
        {
            if (other.tag == "Ghost")
            {
                resetPosition();
                other.transform.parent.GetComponent<BasicNPC>().resetTarget();
            }
                


            else if (other.tag == "Dot")
            {

                photonView.RPC("DestroyPhotonView", PhotonTargets.All, other.gameObject.GetComponent<PhotonView>().viewID);

            }

            else if (other.tag == "SpecialDot")
            {


                photonView.RPC("DestroyPhotonView", PhotonTargets.All, other.gameObject.GetComponent<PhotonView>().viewID);
            }

        }
        
    }

    void OntriggerStay(Collider other)
    {
    }

   [PunRPC]
   void DestroyPhotonView(int photonViewId)
    {
        if(PhotonNetwork.isMasterClient)
            PhotonNetwork.Destroy(PhotonView.Find(photonViewId));
    }

    public void addPoints(int points)
    {
        score += points;
    }

    public void addSpeed(float speed)
    {
        movementSpeedTimer = Time.time + movementSpeedBoostDuration;

        movementSpeed += speed;
    }

    public void setNextDestination(Transform position)
    {
        nextDestination = position.position;
    }

    public void playSound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void checkSpeed()
    {
        if (movementSpeedTimer < Time.time)
            applyDefaultMovementSpeed();
    }

    private void applyDefaultMovementSpeed()
    {
        movementSpeed = defaultMovementSpeed;
    }


    //Cast a ray from player to destination in the direction we are trying to go if it's a wall then we can't go else we can
    private bool isValidMovement(Vector3 direction)
    {
        RaycastHit[] hit;

        Ray playerRay = new Ray(transform.position, direction);

        hit = Physics.SphereCastAll(playerRay, 0.5f, 1.0f);

        for (int i =0; i< hit.Length; i++)
        {
            if (hit[i].collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                return false;
        }

        return true;

    }

    private void resetPosition()
    {
        transform.position = initialPosition;
        nextDestination = transform.position;
    }

    //Networking

     //Send updates of change of certain data
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //Send My client
            stream.SendNext(score);
        }
        else
        {
            //Receive other users data
            this.score = (int)stream.ReceiveNext();
        }
    }

    //Called when we load a new scene
    void CalledOnLevelWasLoaded(int level)
    {
        Debug.Log("Load scene on player was called");
    }

}