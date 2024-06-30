using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControllerAttacker : AIController
{
    public enum AIState { Advance, Engage, Reposition, Retreat };

    public AIState currentState;

    

    // Start is called before the first frame update
    void Awake()
    {
        ChangeState(AIState.Advance);
    }

    // Responsible for AI decidions
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
            case AIState.Advance:
                Debug.Log("Advancing on Player");
                // Do work for Advance
                DoAdvanceState();
                break;
            case AIState.Engage:
                Debug.Log("Firiing at Player");
                //Do work for Attack
                DoEngageState();
                break;
            case AIState.Reposition:
                Debug.Log("Repositioning from Player");
                //Do work for Reposition
                DoRepositionState();
                break;
            case AIState.Retreat:
                Debug.Log("Retreating from Player");
                //Do work for Fallback
                DoRetreatState();
                break;
        }

    }
    // Advance State
    private void DoAdvanceState()
    {
        if (CanEngage())
        {
            ChangeState(AIState.Engage);
            return;
        }
        if (TookDamage())
        {
            ChangeState(AIState.Reposition);
            return;
        }
        Chase(player);
    }

    // Engage State
    private void DoEngageState()
    {
        // Check for transitions
        if (TookDamage())
        {
            ChangeState(AIState.Reposition);
            return;
        }
        if (!CanEngage())
        {
            ChangeState(AIState.Advance);
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

    private void DoRepositionState()
    {
        
    }
    // Retreat State
    public void DoRetreatState()
    {
        if (!IsDistanceLessThan(player, dangerDistance))
        {
            ChangeState(AIState.Advance);
            return;
        }
        Flee();
    }
    // Change State
    public virtual void ChangeState(AIState State)
    {
        // Change the current state
        currentState = State;
        // Save the time when we changed states
        lastStateChangeTime = Time.time;
    }
}
