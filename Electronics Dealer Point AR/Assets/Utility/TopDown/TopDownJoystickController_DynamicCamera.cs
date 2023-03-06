using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


/// <summary>
/// Top down/ third person player movement based on camera facing 
/// </summary>
public class TopDownJoystickController_DynamicCamera : MonoBehaviour
{
	[SerializeField] FloatingJoystick floatingjoystick; // joystick srcipt referance
	[SerializeField] JoystickAngle joystickAngle; // joystic angle script referance
	
	[SerializeField] Transform PlayerCam; // player camera referance
	[SerializeField] float speed = 5f; // player speed
	[SerializeField] float rotationSpeed = 0.1f; // rotation speed

	public bool canMove = true; // can move
	Vector3 vec3;

	protected void Update()
	{
		float h = floatingjoystick.Horizontal;
		float v = floatingjoystick.Vertical;
		Show.Log(h + " " + v);

		vec3.x = h;
		vec3.y = 0;
		vec3.z = v;
		
		// Angle From JoyStick
		float AngleInDegrees = joystickAngle.GetJosystickAngle();
		// Angle Between camPvot vs joystic angle
		float angle = -AngleInDegrees + 90 + PlayerCam.transform.rotation.eulerAngles.y;
		//Show.LogRed(h + " " + v);
		if (Mathf.Abs(h) <= 0.1f && Mathf.Abs(v) <= 0.1f) return;
		

		transform.DORotate(new Vector3(0, angle, 0), rotationSpeed, RotateMode.Fast); // rotating via angle 

		if (canMove)
		{ 
			transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self); // moving forword
		}
	}

	
}