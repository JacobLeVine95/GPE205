using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControllerSniper : AIController
{
    public enum AIState { Idle, Snipe, Retreat, Reposition };

    public AIState currentState;




    // Start is called before the first frame update
    void Awake()
    {
        ChangeState(AIState.Idle);
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
            case AIState.Idle:
                // Do work for Idle
                DoIdleState();
                break;
            case AIState.Snipe:
                // Do work for Snipe
                DoSnipeState();
                break;
            case AIState.Retreat:
                // Do work for Retreat
                DoRetreatState();
                break;
            case AIState.Reposition:
                // Do work for Reposition
                DoRepositionState();
                break;
        }
        // Idle State
        private void DoIdleState()
        {
            throw new NotImplementedException();
        }
        //Snipe State
        private void DoSnipeState()
        {
            throw new NotImplementedException();
        }
        // Retreat State
        private void DoRetreatState()
        {
            // Check for transitions
            if (IsDistanceLessThan(player, dangerDistance))
            {
                ChangeState(AIState.Retreat);
            }
            // Doing Retreat State
            Debug.Log("Retreating");
            Flee();
        }
        // Repostition State
        private void DoRepositionState()
        {
            throw new NotImplementedException();
        }
    }



    public virtual void ChangeState(AIState State)
    {
        // Change the current state
        currentState = State;
        // Save the time when we changed states
        lastStateChangeTime = Time.time;
    }
}
