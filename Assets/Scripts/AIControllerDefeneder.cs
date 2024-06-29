using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControllerDefeneder : AIController
{
    public enum AIState { Patrol, Alert, Engage, Retreat };

    public AIState currentState;



    // Start is called before the first frame update
    void Awake()
    {
        ChangeState(AIState.Patrol);
    }

    // Responsible for AI decisions
    public override void ProcessInputs()
    {
        Debug.Log("Making Decisions");
        if (player == null)
        {
            FindPlayer();
            return;
        }
        switch (currentState)
        {
            case AIState.Retreat:
                // Do work for Guard
                DoGuardState();
                break;
            case AIState.Alert:
                Debug.Log("Started chasing player");
                // Do work for Chase
                DoChaseState();
                break;
            case AIState.Engage:
                // Do work for Attack
                DoAttackState();
                break;
            case AIState.Patrol:
                // Do work for Patrol
                DoPatrolState();
                break;
            //case AIState.Retrea:
            //    // Do work for Flee
            //    DoFleeState();
            //    break;
        }
    }

    // Patrol State
    protected void DoPatrolState()
    {
        // Check for transitions

        if (IsDistanceLessThan(player, targetDistance))
        {
            Debug.Log("Player within distance");
            ChangeState(AIState.Alert);
        }
        Debug.Log("Patrolling");
        // If we have enough waypoints in our list to move to a current waypoint
        if (waypoints.Length > currentWaypoint)
        {
            Debug.Log("Moving to current waypoint");
            // Then seek that waypoint
            Flee(waypoints[currentWaypoint].transform.position);
            // If we are close enough, then increment to the next waypoint
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < waypointStopDistance)
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
        // Check for transitions
        if (IsDistanceLessThan(player, targetDistance))
        {
            ChangeState(AIState.Alert);
        }
        // Doing Guard State
        Debug.Log("Guarding");
    }

    // Chase State  
    public void DoChaseState()
    {
        // Check for transitions
        if (IsDistanceLessThan(player, targetDistance))
        {
            ChangeState(AIState.Engage);
            return;
        }
        else if (IsDistanceLessThan(player, targetDistance))
        {
            ChangeState(AIState.Retreat);
            return;
        }
        else if (IsDistanceLessThan(player, targetDistance))
        {
            ChangeState(AIState.Patrol);
            return;
        }

        // Doing Chase State
        Debug.Log("Chasing");
        Chase(player);
    }

    // Attack State
    public void DoAttackState()
    {
        // Check for transitions
        if (IsDistanceLessThan(player, 7))
        {
            ChangeState(AIState.Engage);
            return;
        }
        // Chase
        Chase(player);
        // Shoot
        Shoot();
    }

    // Flee State
    public void DoFleeState()
    {
        if (IsDistanceLessThan(player, targetDistance))
        {
            ChangeState(AIState.Retreat);
        }
        //Doing Flee State
        Debug.Log("Fleeing");
        // Find the vector to our target
        Vector3 vectorToTarget = player.transform.position - pawn.transform.position;
        // Find the vector away from our target
        Vector3 vectorAwayFromTarget = -vectorToTarget;
        // Find the direction and determines how far it runs away
        Vector3 fleeVector = vectorAwayFromTarget.normalized * fleeDistance;

        Flee(pawn.transform.position + fleeVector);
    }

    public virtual void ChangeState(AIState State)
    {
        // Change the current state
        currentState = State;
        // Save the time when we changed states
        lastStateChangeTime = Time.time;
    }
}
