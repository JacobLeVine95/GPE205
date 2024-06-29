using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
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

    // Start is called before the first frame update
    // public abstract void Start();

    // Update is called once per frame
    // public abstract void Update();

    public abstract void Shoot(GameObject shellPrefab, float fireForce, float damageDone, float lifespan);

}
