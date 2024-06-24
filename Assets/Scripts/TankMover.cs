using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : Mover
{
    // Variable to hold the Rigidbody Component
    private Rigidbody rb;
    // Variable to hold the Transform Component
    private Transform tf;

    // Start is called before the first frame update
    public override void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        //Get the Transform component
        tf = gameObject.transform;
    }

    public override void Move(Vector3 direction, float speed)
    {
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVector);
    }

    public override void Rotate(float rotationSpeed)
    {
        Vector3 rotateVector = new Vector3(0.0f, 1.0f, 0.0f) * rotationSpeed * Time.deltaTime;
        tf.Rotate(rotateVector); 
    }
}
