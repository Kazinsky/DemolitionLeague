using UnityEngine;
using System.Collections;

public enum State { Roaming, Chasing };

public class BasicNPC : NetworkedCharacter
{

    #region Public Properties
    public GameObject Path;
    public float leaveCellDelay;
    public float chaseDuration;
    #endregion


    #region Private Properties
    int currentRoamingPositionIndex = 0;

    private bool leaveCell = false;

    //Timers
    private float leaveCellTimer;
    private float chaseTimer;

    //State
    private State currentState;

    private GameObject chaseTarget;

    private Transform goalPosition;

    private UnityEngine.AI.NavMeshAgent ghostAgent;

    private Transform[] RoamingPositions;


    #endregion


    #region MonoBehaviour Messages

    // Use this for initialization
    public override void Start()
    {
        if (photonView.isMine)
        {
            base.Start();
            //Get Tranform point from path gameobject
            if (Path != null)
                RoamingPositions = Path.GetComponentsInChildren<Transform>();

            //Set inital State for AI
            currentState = State.Roaming;

            goalPosition = transform;

            ghostAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

            leaveCellTimer = Time.time + leaveCellDelay;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            if (leaveCell)
            {
                if (currentState == State.Roaming)
                {
                    //If arrived at destination then choose the next one
                    if (ghostArrivedAtDestination())
                    {
                        if (RoamingPositions != null)
                        {
                            setNextIndex();
                            goalPosition = RoamingPositions[currentRoamingPositionIndex];
                        }
                    }

                }
                else if (currentState == State.Chasing)
                {
                    if (chaseTarget != null)
                    {
                        if (chaseTimer > Time.time)
                        {
                            if (Vector3.Distance(goalPosition.position, chaseTarget.transform.position) > 1.0f)
                            {
                                goalPosition = chaseTarget.transform;

                            }
                        }
                        else
                        {
                            setToRoaming();
                        }

                    }
                    else
                    {
                        setToRoaming();
                    }
                    
                }

                //Set Destination to the AI
                if (ghostAgent.destination != goalPosition.position)
                    ghostAgent.destination = goalPosition.position;
            }
            else
            {
                if (leaveCellTimer < Time.time)
                    leaveCell = true;
            }

        }
    }

    public override void OnTriggerEnter(Collider other)
    {

        if (photonView.isMine)
        {
            base.OnTriggerEnter(other);

            if (other.tag == "Player")
            {

                chaseTarget = other.gameObject;
                currentState = State.Chasing;

                chaseTimer = Time.time + chaseDuration;
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (photonView.isMine)
        {
            if (other.tag == "Player")
            {

                chaseTarget = null;

                setToRoaming();

            }
        }
    }

    #endregion


    #region Public Methods

    public void resetTarget()
    {
        chaseTarget = null;
    }

    public void setTarget(GameObject target)
    {
        chaseTarget = target.gameObject;
        currentState = State.Chasing;
        chaseTimer = Time.time + chaseDuration;
    }

    #endregion

    #region Private Methods

    private void setNextIndex()
    {
        if (currentRoamingPositionIndex + 1 < RoamingPositions.Length)
            currentRoamingPositionIndex++;
        else
            currentRoamingPositionIndex = 1;
    }

    private bool ghostArrivedAtDestination()
    {
        if (!ghostAgent.pathPending)
        {
            if (ghostAgent.remainingDistance <= ghostAgent.stoppingDistance)
            {
                if (!ghostAgent.hasPath || ghostAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void setToRoaming()
    {
        if (RoamingPositions != null)
        {
            currentState = State.Roaming;
            goalPosition = RoamingPositions[currentRoamingPositionIndex];
        }
    }



    #endregion




}
