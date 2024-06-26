using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //Header just creates a Header text to seperate things.
    [Header("Player Health")]
    //Variable for current Health
    public float currentHealth;
    //Variable for max health
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        // Set health to max
        currentHealth = maxHealth; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Reduces Player Health when event happens.
    public void TakeDamage(float amount, Pawn source)
    {
        // Subtract amount from current health. (currentHealth = currentHealth - amount)
        currentHealth -= amount;
        // (Print Statement; Concatenation is at play here) Shows who did how much damage to who. 
        Debug.Log(source.name + " did " + amount + " damage to " + gameObject.name);
        // Clamp function allows Player to only take damage to zero and resets it to zero if gone below.
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        // If our health is equal or less than zero, it calls the 'Die' function.
        if (currentHealth <= 0)
        {
            Die(source);
        }
    }

    // Increases the Player Health up to the Max Health.
    public void Heal(float amount)
    {
        // Add amount to current health. (currentHealth = currentHealth + amount)
        currentHealth += amount;
        // Clamp function allows Player to only heal to max health and resets it to max health if gone over.
        currentHealth = Mathf.Clamp( currentHealth, 0, maxHealth);
    }

    // Destroys Player after too much damage is recieved.
    public void Die(Pawn source)
    {
        Destroy(gameObject);
    }
}
