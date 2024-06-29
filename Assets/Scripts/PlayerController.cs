using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController : Controller
{
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    public KeyCode shootKey;

    // Start is called before the first frame update
    public override void Start()
    {
        // If we have a GameManager
        if (GameManager.instance != null)
        {
            // And it tracks the player(s)
            if (GameManager.instance.players != null)
            {
                // Register with the GameManager
                GameManager.instance.players.Add(this);
            }
        }
        // Run the Start() function from the parent (base) class
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        //Make Decisions
        if (pawn != null)
        {
            // Check if pawn has been destroyed; only make decisions if not destroyed
            ProcessInputs();
        }
        // Run the parent (base) update
        base.Update();
    }

    public override void ProcessInputs()
    {
        // On key being pressed or held.
        if (Input.GetKey(moveForwardKey))
        {
            pawn.MoveForward();
        }

        if (Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
        }

        if (Input.GetKey(rotateClockwiseKey))
        {
            pawn.RotateClockwise();
        }

        if (Input.GetKey(rotateCounterClockwiseKey))
        {
            pawn.RotateCounterClockwise();
        }

        // On key pressed down, activates.
        if (Input.GetKeyDown(shootKey))
        {
            pawn.Shoot();
        }

    }
    public void OnDestroy()
    {
        // If we have a GameManager
        if (GameManager.instance != null)
        {
            // And it tracks the player(s)
            if (GameManager.instance.players != null)
            {
                // Deregister with the GameManager
                GameManager.instance.players.Remove(this);
            }
        }
    }
}
