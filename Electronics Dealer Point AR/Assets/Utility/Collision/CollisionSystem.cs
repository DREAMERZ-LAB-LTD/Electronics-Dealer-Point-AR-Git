using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will send callback for On Collision Enter/Exit/Stay
/// Need to attach any kind of collider or its through errors in start
/// </summary>
public class CollisionSystem : MonoBehaviour
{
    private void Start()
    {
        // this script for collistion detection so isTrigger will be false.
        gameObject.GetComponent<Collider>().isTrigger = false;
    }
    public delegate void Collision(UnityEngine.Collision other); // Create a delegate function to use in callbacks
    public event Collision CollisionEnterEvent; // Call back enter
    public event Collision CollisionExitEvent; // Call back exit
    public event Collision CollisionStayEvent; // Call back stay

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        // Debug.Log("Collision Stay: " + collision.name);
        if (CollisionEnterEvent != null) CollisionEnterEvent.Invoke(collision); // sending call backs
    }
    private void OnCollisionExit(UnityEngine.Collision collision)
    {
        // Debug.Log("Collision Exit: " + collision.name);
        if (CollisionExitEvent != null) CollisionExitEvent.Invoke(collision); // sending call backs
    }
    private void OnCollisionStay(UnityEngine.Collision collision)
    {
        // Debug.Log("Collision Stay: " + collision.name);
        if (CollisionStayEvent != null) CollisionStayEvent.Invoke(collision); // sending call backs
    }
}
