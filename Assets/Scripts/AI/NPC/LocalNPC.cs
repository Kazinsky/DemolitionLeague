using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalNPC : Character {
	public GameObject Path;
	public float leaveCellDelay;
	public float chaseDuration;
	int currentRoamingPositionIndex = 0;

	private bool leaveCell = false;

	//State
	private State currentState;

	private GameObject chaseTarget;

	private Transform goalPosition;

	private UnityEngine.AI.NavMeshAgent ghostAgent;

	private Transform[] RoamingPositions;
	// Use this for initialization
	public override void Start () {
        base.Start();
		//Get Tranform point from path gameobject
		RoamingPositions = GameObject.Find("Path").GetComponentsInChildren<Transform>();

		//Set inital State for AI
		currentState = State.Roaming;

		goalPosition = transform;

		ghostAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!IsDead())
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            currentState = State.Roaming;
            for (int i = 0; i < players.Length; ++i)
            {
                Vector3 v = players[i].transform.position - transform.position;
                if (v.magnitude < 4.5f)
                {
                    currentState = State.Chasing;
                    chaseTarget = players[i];
                    break;
                }
            }
            if (currentState == State.Roaming)
            {
                //If arrived at destination then choose the next one
                if (ghostArrivedAtDestination())
                {
                    if (RoamingPositions != null)
                    {
                        setNextIndex();
                        goalPosition = RoamingPositions[currentRoamingPositionIndex];
                        ghostAgent.destination = goalPosition.position;
                    }
                }
            }
            else if (currentState == State.Chasing)
            {
                if (Vector3.Distance(goalPosition.position, chaseTarget.transform.position) > 1.0f)
                {
                    goalPosition = chaseTarget.transform;
                }
                //Set Destination to the AI
                if (ghostAgent.destination != goalPosition.position)
                    ghostAgent.destination = goalPosition.position;
            }
        }
        else
            die();
	}

    private void die()
    {
        Destroy(gameObject);
    }


	private void setNextIndex()
	{
		currentRoamingPositionIndex = Random.Range(0, RoamingPositions.Length);
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
}
