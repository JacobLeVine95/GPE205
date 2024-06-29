using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
   

    public GameObject player;

    public float targetDistance;

    public float fleeDistance;

    protected float lastStateChangeTime;

    public Transform[] waypoints;
    public float waypointStopDistance;
    // Tells the AI which waypoint to target
    protected int currentWaypoint = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        FindPlayer();

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

    public override void ProcessInputs()
    {

    }

    
    public void Flee (Vector3 fleeLocation)
    {
        // RotateTowards the target
        pawn.RotateTowards(fleeLocation);
        // MoveForward towards the target
        pawn.MoveForward();
    }

    public void Chase(GameObject Target)
    {
        // Seek the position of our target Transform
        Flee(Target.transform.position);
    }


    public void Shoot()
    {
        // Tell the pawn to shoot
        pawn.Shoot();
    }

    // A setup for a transition out of a state
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



    public void FindPlayer()
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
