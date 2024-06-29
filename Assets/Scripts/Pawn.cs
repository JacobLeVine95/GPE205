using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    [Header("Player Movement options")]
    // Variable for move speed
    public float moveSpeed;
    // Variable for turn speed
    public float turnSpeed;
    // Variable to hold our Mover
    public Mover mover;

    [Header("Shooting Options")]
    // Variable for the firing force
    public float fireForce;
    // Variable for the fire rate
    public float fireRate;
    // Variable for the damage done
    public float damageDone;
    // Variable for the shell prefab
    public GameObject shellPrefab;
    // Variable for how long our bullets survive if they don't collide
    public float shellLifespan;
    // Variable to hold the Shooter function
    public Shooter shooter;

    // Start is called before the first frame update
    public virtual void Start()
    {
        mover = GetComponent<Mover>();
        shooter = GetComponent<Shooter>();
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }
    
    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void Shoot();
    public abstract void RotateTowards(Vector3 targetPosition);
}
