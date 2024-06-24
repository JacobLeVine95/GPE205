using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    [Header("Player Movement")]
    // Variable for move speed
    public float moveSpeed;
    // Variable for turn speed
    public float turnSpeed;
    //Variable to hold our Mover
    public Mover mover;

    [Header("Shooting Logic")]
    // Variable for Rate of Fire
    public float fireRate;
    public float fireCounter;
    public bool canFire = true;

    //Firepoint Transform
    public Transform firePoint;
    public GameObject bullet;

    // Start is called before the first frame update
    public virtual void Start()
    {
        mover = GetComponent<Mover>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
    }
    
    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void Shooting();
}
