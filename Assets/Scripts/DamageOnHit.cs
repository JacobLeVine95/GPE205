using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    [Header("Damage To Other")]
    // Variable for amount of damage done.
    public float damageDone;
    // Variable for the owner of the object inflicting damage.
    public Pawn owner;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == owner.gameObject)
        {
            return;
        }


        // Get the Health component from the Game Object that has the collider that is being overlapping
        Health otherHealth = other.gameObject.GetComponent<Health>();
        // Only damage if it has a Health component
        if (otherHealth != null)
        {
            // Do damage
            otherHealth.TakeDamage(damageDone, owner);
        }

        // Destroy ourselves, whether we did damage or not
        Destroy(gameObject);
    }
}
