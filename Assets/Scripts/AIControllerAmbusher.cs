using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControllerAmbusher : AIController
{
    public enum AIState { Evasion, Ambush, Strike, Reevaluate };

    public AIState currentState;



    // Start is called before the first frame update
    void Awake()
    {
        ChangeState(AIState.Evasion);
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
            case AIState.Evasion:
                //Do work for Evasion
                DoEvasionState();
                break;
            case AIState.Ambush:
                //Do work for Ambush
                DoAmbushState();
                break;
            case AIState.Strike:
                // Do work for Engage
                DoStrikeState();
                break;
            case AIState.Reevaluate:
                //Do work for Re-evaluate
                DoReevaluateState();
                break;
        }
    }
    // Evasion State
    private void DoEvasionState()
    {
        throw new NotImplementedException();
    }
    // Ambush State
    private void DoAmbushState()
    {
        throw new NotImplementedException();
    }
    // Strike State
    private void DoStrikeState()
    {
        throw new NotImplementedException();
    }
    // Re-evaluation State
    private void DoReevaluateState()
    {
        throw new NotImplementedException();
    }

    public virtual void ChangeState(AIState State)
    {
        // Change the current state
        currentState = State;
        // Save the time when we changed states
        lastStateChangeTime = Time.time;
    }
}
