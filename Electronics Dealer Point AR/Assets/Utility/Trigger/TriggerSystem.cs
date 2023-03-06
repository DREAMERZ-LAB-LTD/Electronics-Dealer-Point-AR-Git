using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will send callback for On Trigger Enter/Exit/Stay
/// Need to attach any kind of collider or its through errors in start
/// </summary>
public class TriggerSystem : MonoBehaviour
{
    private void Start()
    {
        // this script for collistion detection so isTrigger will be true.
        gameObject.GetComponent<Collider>().isTrigger = true;
    }
    public delegate void Trigger(Collider other);// Create a delegate function
    public event Trigger TriggerEnterEvent; // Call back enter
    public event Trigger TriggerExitEvent; // Call back Exit
    public event Trigger TriggerStayEvent; // Call back stay
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger Enter: "+other.name);
         if(TriggerEnterEvent != null) TriggerEnterEvent.Invoke(other);// sending call backs
    }
    private void OnTriggerExit(Collider other)
    {
       // Debug.Log("Trigger Exit: " + other.name);
        if (TriggerExitEvent != null) TriggerExitEvent.Invoke(other);// sending call backs
    }
    private void OnTriggerStay(Collider other)
    {
        // Debug.Log("Trigger Stay: " + other.name);
        if (TriggerStayEvent != null) TriggerStayEvent.Invoke(other);// sending call backs
    }
}
