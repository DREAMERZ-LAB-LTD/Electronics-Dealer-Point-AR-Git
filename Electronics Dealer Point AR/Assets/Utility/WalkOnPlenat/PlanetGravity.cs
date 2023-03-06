using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script will force it self like gravity 
/// </summary>
public class PlanetGravity : MonoBehaviour
{
    public float gravity = -10; // Gravity value

    /// <summary>
    /// This function attract the parameter object like gravity
    /// </summary>
    /// <param name="body">Planet Player</param>
    public void Attract(Transform body)
    {
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;
        body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);
        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);
    }
}
