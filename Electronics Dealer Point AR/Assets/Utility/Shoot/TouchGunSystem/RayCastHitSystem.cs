using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// if the raycast hit object have this script then it will get a referance of the hit
/// And
/// you can do anything using this data on hit area.
/// </summary>
public class RayCastHitSystem : MonoBehaviour
{
     public delegate void RayCastHit(RaycastHit hit);
     public event RayCastHit rayCastHit;

    /// <summary>
    /// Call From RayCast hit
    /// </summary>
    /// <param name="hit">Ray hit data</param>
    public void RayHit(RaycastHit hit)
    {
        if(rayCastHit != null) rayCastHit.Invoke(hit);
    }
}
