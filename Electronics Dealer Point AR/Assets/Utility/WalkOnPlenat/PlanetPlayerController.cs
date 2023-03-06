using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


/// <summary>
/// Planet player walking controller
/// it will use only for gravity planet player
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlanetPlayerController : MonoBehaviour
{
    [SerializeField] FixedJoystick fixedJoystick; // joystick referance
    [SerializeField] Transform childBody; // child body 
    public float moveSpeed = 15; // walking speed
    private Vector3 movedir; 
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");
        float h = fixedJoystick.Horizontal;
        float v = fixedJoystick.Vertical;
        RotateChild(h, v);
        movedir = new Vector3(h, 0, v).normalized;
        
    }

    /// <summary>
    /// rotate child body to moving direction
    /// </summary>
    /// <param name="h">Horizantal value</param>
    /// <param name="v">Virtical value</param>
    private void RotateChild(float h, float v)
    {
        if (h == 0 && v == 0) return;
        float ang = CalculateAngle.XY(Vector2.zero, -new Vector2(v, h)); // calculating angle
        Show.Log(ang);
        childBody.DOLocalRotate(new Vector3(0, ang, 0), 0, RotateMode.Fast);
    }

    /// <summary>
    /// Moving using RB.MovePosication
    /// </summary>
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(movedir) * moveSpeed * Time.deltaTime);
    }
}