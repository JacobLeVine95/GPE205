using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : Shooter
{
    public Transform firepointTransform;

    public override void Shoot(GameObject shellPrefab, float fireForce, float damageDone, float lifeSpan)
    {
        //Instantiate bullet
        GameObject newShell = Instantiate(shellPrefab, firepointTransform.position, firepointTransform.rotation) as GameObject;

        //Get the DamageOnHit component
        DamageOnHit doh = newShell.GetComponent<DamageOnHit>();

        //if it exists
        if (doh != null )
        {
            //apply doh values
            doh.damageDone = damageDone;
            doh.owner = GetComponent<Pawn>();
        }

        //Get the rigidbody component
        Rigidbody rb = newShell.GetComponent<Rigidbody>();

        // if rigidbody exists
        if (rb != null )
        {
            // Add force to the spawned rigidbody
            rb.AddForce(firepointTransform.forward * fireForce);
        }

        // Destroy after a set time
        Destroy(newShell, lifeSpan);
    }
}
