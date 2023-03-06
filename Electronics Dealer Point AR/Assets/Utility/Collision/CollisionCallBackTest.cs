using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionSystem))]
public class CollisionCallBackTest : MonoBehaviour
{
    CollisionSystem triggerSystem;

    void OnEnable()
    {
        triggerSystem = GetComponent<CollisionSystem>();
        triggerSystem.CollisionEnterEvent += CEnter;
        triggerSystem.CollisionExitEvent += CExit;
        triggerSystem.CollisionStayEvent += CStay;
    }
    private void OnDisable()
    {
        triggerSystem.CollisionEnterEvent -= CEnter;
        triggerSystem.CollisionExitEvent -= CExit;
        triggerSystem.CollisionStayEvent -= CStay;
    }
    private void CEnter(Collision collision)
    {
        Show.LogRed("Collision Enter: " + collision.transform.name);
    }
    
    private void CStay(Collision collision)
    {
        Show.LogGreen("Collision Stay: " + collision.transform.name);
    }
    private void CExit(Collision collision)
    {
        Show.LogBlue("Collision Exit: " + collision.transform.name);
    }
}
