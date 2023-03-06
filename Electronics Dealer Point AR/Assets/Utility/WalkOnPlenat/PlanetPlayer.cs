using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This object will attatch with player and if will send Planet Gravity it referance 
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlanetPlayer : MonoBehaviour
{
    public PlanetGravity planetGravity;
    private Transform myTranstorn;
    private Rigidbody rb;
    void Start()
    {
        // setup
        rb = GetComponent<Rigidbody>();
        rb.constraints=RigidbodyConstraints.FreezeRotation;
        rb.useGravity=false;
        myTranstorn = transform;
    }
    void Update()
    {
        planetGravity.Attract(myTranstorn); // sending referance
    }
}
