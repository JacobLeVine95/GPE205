using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball_Logic : MonoBehaviour
{
    // Variable for bullet speed
    public float bulletSpeed;

    //Getting Rigidbody component
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Makes the bullet travel
        rb.velocity += transform.forward * bulletSpeed * Time.deltaTime;
        
    }
}
