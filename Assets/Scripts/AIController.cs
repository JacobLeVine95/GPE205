using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Guard, Chase, Flee, Attack, Patrol, Seek, Shoot };

    public AIState currentState;

    public GameObject player;

    public float targetDistance;

    public float fleeDistance;

    private float lastStateChangeTime;

    public Transform[] waypoints;
    public float waypointStopDistance;
    // Tells the AI which waypoint to target
    private int currentWaypoint = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        TargetPlayerOne();
        ChangeState(AIState.Patrol);
        //Run the parents (base) start
        base.Start();  
    }

    // Update is called once per frame
    public override void Update()
    {
        //Make Decisions
        if(pawn != null)
        {
            // Check if pawn has been destroyed; only make decisions if not destroyed
            ProcessInputs();
        }
        // Run the parent (base) update
        base.Update();  
    }

    // Responsible for AI decisions
    public override void ProcessInputs()
    {
        Debug.Log("Making Decisions");
        if (player == null)
        {
            TargetPlayerOne();
            return;
        }
        switch (currentState)
        {
            case AIState.Guard:
                // Do work for Guard
                DoGuardState();
                // Check for transitions
                if(IsDistanceLessThan(player, targetDistance))
                {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.Chase:
                Debug.Log("Started chasing player");
                // Do work for Chase
                DoChaseState();
                // Check for transitions
                if (IsDistanceLessThan(player, targetDistance))
                {
                    ChangeState(AIState.Attack);
                }
                else if (IsDistanceLessThan(player, targetDistance))
                {
                    ChangeState(AIState.Guard);
                }
                else if (IsDistanceLessThan(player, targetDistance))
                {
                    ChangeState(AIState.Patrol);
                }
                break;
            case AIState.Attack:
                // Do work for Attack
                DoAttackState();
                // Check for transitions
                if(IsDistanceLessThan(player, 7))
                {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.Patrol:
                // Do work for Patrol
                DoPatrolState();
                // Check for transitions

                if (IsDistanceLessThan(player, targetDistance))
                {
                    Debug.Log("Player within distance");
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.Flee:
                // Do work for Flee
                DoFleeState();
                if (IsDistanceLessThan(player, targetDistance))
                {
                    ChangeState(AIState.Guard);
                }
                break;
        }
    }

    // Patrol State
    protected void DoPatrolState()
    {
        Debug.Log("Patrolling");
        // If we have enough waypoints in our list to move to a current waypoint
        if(waypoints.Length > currentWaypoint)
        {
            Debug.Log("Moving to current waypoint");
            // Then seek that waypoint
            Seek(waypoints[currentWaypoint].transform.position);
            // If we are close enough, then increment to the next waypoint
            if(Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < waypointStopDistance)
            {
                Debug.Log("Moving to next waypoint");
                currentWaypoint++;
            }
        }
        else
        {
            RestartPatrol();
        }
    }

    public void RestartPatrol()
    {
        // Set the index to 0
        currentWaypoint = 0;
    }

    // Guard State
    public void DoGuardState()
    {
        // Doing Guard State
        Debug.Log("Guarding");
    }

    // Chase State  
    public void DoChaseState()
    {
        // Doing Chase State
        Debug.Log("Chasing");
        Seek(player);
    }

    // Flee State
    public void DoFleeState()
    {
        //Doing Flee State
        Debug.Log("Fleeing");
        // Find the vector to our target
        Vector3 vectorToTarget = player.transform.position - pawn.transform.position;
        // Find the vector away from our target
        Vector3 vectorAwayFromTarget = -vectorToTarget;
        // Find the direction and determines how far it runs away
        Vector3 fleeVector = vectorAwayFromTarget.normalized * fleeDistance;

        Seek(pawn.transform.position + fleeVector);
    }

    public void Seek (Vector3 targetPosition)
    {
        // RotateTowards the target
        pawn.RotateTowards(targetPosition);
        // MoveForward towards the target
        pawn.MoveForward();
    }

    public void Seek(GameObject Target)
    {
        // Seek the position of our target Transform
        Seek(Target.transform.position);
    }

    // Attack State
    public void DoAttackState()
    {
        // Chase
        Seek(player);
        // Shoot
        Shoot();
    }

    public void Shoot()
    {
        // Tell the pawn to shoot
        pawn.Shoot();
    }

    // A setup for a transition our of a state
    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        if (Vector3.Distance(pawn.transform.position, target.transform.position) < distance)
        {
            return true; 
        }
        else
        {
            return false;
        }
    }

    public virtual void ChangeState(AIState State)
    {
        // Change the current state
        currentState = State;
        // Save the time when we changed states
        lastStateChangeTime = Time.time;
    }

    public void TargetPlayerOne()
    {
        // If the GameManager exists
        if(GameManager.instance != null)
        {
            // And there are players in it
            if(GameManager.instance.players.Count > 0)
            {
                if(GameManager.instance.players[0].pawn == null)
                {
                    return;
                }
                // Then target the gameObject of the pawn of the first player controller in the list
                player = GameManager.instance.players[0].pawn.gameObject;
                Debug.Log("Found Player");
            }
        }
    }

    protected bool IsHasTarget()
    {
        // return true is we have a target, false if we don't
        return (player != null);
    }
}
