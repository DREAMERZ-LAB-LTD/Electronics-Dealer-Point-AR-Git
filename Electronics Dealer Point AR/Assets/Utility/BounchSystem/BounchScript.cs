using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Script will use for maintaning a object bounch and its diraction via twicking it.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CollisionSystem))]
public class BounchScript : MonoBehaviour
{
    [SerializeField] bool isInfinity; //is the object bounch for infinity time
    [SerializeField] float meterialForce = 1f; // bounch value of a matrial.

    #region call Back
    CollisionSystem triggerSystem;
    Rigidbody rb;
    void OnEnable()
    {
        triggerSystem = GetComponent<CollisionSystem>();
        rb = GetComponent<Rigidbody>();
        triggerSystem.CollisionEnterEvent += Bounch;
      
    }
    private void OnDisable()
    {
        triggerSystem.CollisionEnterEvent -= Bounch;
     
    }
    #endregion

    Vector3 velocity;
    private void Update()
    {
        velocity = rb.velocity;
    }

    /// <summary>
    /// Bounch Fuction
    /// </summary>
    /// <param name="collision"> Call back data from On Collistion</param>

    private void Bounch(Collision collision)
    {
        Show.Log(velocity.magnitude);
        var direction = Vector3.Reflect(velocity.normalized, collision.contacts[0].normal); // calculating direction after Collistion. 
        rb.velocity = Vector3.zero;
        rb.velocity = direction * meterialForce * (isInfinity==false?Mathf.Max(velocity.magnitude, 0) : 1);
       
    }
}
