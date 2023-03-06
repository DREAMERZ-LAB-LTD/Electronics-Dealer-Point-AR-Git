using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerSystem))]
public class TriggerCallBackTest : MonoBehaviour
{
    TriggerSystem triggerSystem;
    void OnEnable()
    {
        triggerSystem = GetComponent<TriggerSystem>();
        triggerSystem.TriggerEnterEvent += TEnter;
        triggerSystem.TriggerExitEvent += TExit;
        triggerSystem.TriggerStayEvent += TStay;
    }
    private void OnDisable()
    {
        triggerSystem.TriggerEnterEvent -= TEnter;
        triggerSystem.TriggerExitEvent -= TExit;
        triggerSystem.TriggerStayEvent -= TStay;
    }
    private void TEnter(Collider other)
    {
        Show.LogRed("Trigger Enter: " + other.name);
    }
    private void TStay(Collider other)
    {
        Show.LogGreen("Trigger Stay: " + other.name);
    }
    private void TExit(Collider other)
    {
        Show.LogBlue("Trigger Exit: " + other.name);
    }
    

   
}
