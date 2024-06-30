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
            case AIState.Patrol:
                // Do work for Patrol
                DoPatrolState();
                break;
            case AIState.Alert:
                Debug.Log("Alerted to player");
                // Do work for Alert
                DoAlertState();
                break;
            case AIState.Engage:
                // Do work for Engage
                DoEngageState();
                break;
            case AIState.Retreat:
                // Do work for Retreat
                DoRetreatState();
                break;
        }
    }

    // Patrol State
    protected void DoPatrolState()
    {
        // Check for transitions

        if (IsDistanceLessThan(player, alertDistance))
        {
            Debug.Log("Player within distance");
            ChangeState(AIState.Alert);
            return;
        }
        Debug.Log("Patrolling");
        // If we have enough waypoints in our list to move to a current waypoint
        if (waypoints.Length > currentWaypoint)
        {
            Debug.Log("Moving to current waypoint");
            // Then seek that waypoint
            Chase(waypoints[currentWaypoint].gameObject);
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

    // Retreat State
    public void DoRetreatState()
    {
        // Check for transitions
        if (IsDistanceLessThan(player, dangerDistance))
        {
            ChangeState(AIState.Patrol);
        }
        // Doing Retreat State
        Debug.Log("Retreating");

        Flee();
    }

    // Alert State  
    public void DoAlertState()
    {
        // Check for transitions
        if (health.currentHealth <= 0)
        {
            ChangeState(AIState.Retreat);
            return;
        }
        else if (IsDistanceLessThan(player, engageDistance))
        {
            ChangeState(AIState.Engage);
            return;
        }
        else if (!IsDistanceLessThan(player, alertDistance))
        {
            ChangeState(AIState.Patrol);
            return;
        }

        // Doing Chase State
        Debug.Log("Chasing");
        Chase(player);
    }

    // Attack State
    public void DoEngageState()
    {
        // Check for transitions
        if (!CanEngage())
        {
            ChangeState(AIState.Alert);
            return;
        }
        // Chase
        if (!IsTooClose())
        {
            Chase(player);
        }
       
        // Shoot
        Shoot();
    }

    // Flee State
    public void DoFleeState()
    {
        if (!IsDistanceLessThan(player, dangerDistance))
        {
            ChangeState(AIState.Patrol);
            return;
        }
        Flee();
    }

    public virtual void ChangeState(AIState State)
    {
        // Change the current state
        currentState = State;
        // Save the time when we changed states
        lastStateChangeTime = Time.time;
    }
}
