using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.AI;

public class SimpleFSM : MonoBehaviour
{
    //Player Transform
    public Transform playerTransform;

    //List of points for patrolling
    public GameObject[] pointList;
    private int pointIndex;

    private NavMeshAgent agent;

    public float chaseRange = 5.0f;
    public float attackRange = 1.0f;
    public float returnRange = 7.5f;
    public enum FSMState
    {
        None,
        Patrol,
        Chase,
        Attack,
    }

    //Expected period spente in 

    //Current state that the NPC is reaching
    public FSMState curState;

    //Initialize the Finite state machine for the NPC tank
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //Set destination point first
        FindNextPoint();

        curState = FSMState.Patrol;

        //Get the target player
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;

        if (!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");
    }

    //Update each frame
    private void Update()
    {
        switch (curState)
        {
            case FSMState.Patrol:
                UpdatePatrolState();
                break;
            case FSMState.Chase:
                UpdateChaseState();
                break;
            case FSMState.Attack:
                UpdateAttackState();
                break;
        }
    }

    /// <summary>
    /// Patrol state
    /// </summary>
    protected void UpdatePatrolState()
    {
        //Find another patrol point if the current point is reached
        if (agent.remainingDistance <= 0.1f && pointList.Length > 1)
        {
            print("Reached to the destination point\ncalculating the next point");
            FindNextPoint();
        }
        //Check the distance with player
        //When the distance is near, transition to chase state
        if (Vector3.Distance(transform.position, playerTransform.position) <= chaseRange)
        {
            print("Switch to Chase Position");
            curState = FSMState.Chase;
        }
    }

    /// <summary>
    /// Chase state
    /// </summary>
    protected void UpdateChaseState()
    {
        //Set the target position as the player position
        agent.SetDestination(playerTransform.position);

        //Check the distance with player
        //When the distance is near, transition to attack state
        float dist = agent.remainingDistance;
        if (dist <= attackRange)
        {
            print("Switch to Attack Position");
            curState = FSMState.Attack;
        }
        //Go back to patrol is it become too far
        else if (dist >= returnRange)
        {
            curState = FSMState.Patrol;
        }
    }

    /// <summary>
    /// Attack state
    /// </summary>
    protected void UpdateAttackState()
    {
        //Set the target position as the player position
        agent.velocity = Vector3.zero;
        agent.isStopped = true;

        //Attack animation goes here

        //Check the distance with the player tank
        float dist = Vector3.Distance(transform.position, playerTransform.position);
        if (dist > attackRange && dist < returnRange)
        {
            agent.isStopped = false;
            curState = FSMState.Chase;
        }
        //Transition to patrol is the tank become too far
        else if (dist >= returnRange)
        {
            agent.isStopped = false;
            curState = FSMState.Patrol;
        }
    }

    /// <summary>
    /// Find the next semi-random patrol point
    /// </summary>
    protected void FindNextPoint()
    {
        print("Finding next point");
        agent.SetDestination(pointList[pointIndex].transform.position);
        pointIndex++;
        if (pointIndex == pointList.Length)
        {
            pointIndex = 0;
        }
    }
}
