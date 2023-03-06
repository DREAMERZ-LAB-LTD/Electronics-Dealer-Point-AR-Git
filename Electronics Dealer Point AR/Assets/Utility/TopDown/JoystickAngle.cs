using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Getting angle between joystic center and the moving ball
/// </summary>
public class JoystickAngle : MonoBehaviour
{
    [SerializeField] RectTransform floatingjoystick_Background; // joystick center
    [SerializeField] RectTransform floatingjoystick_Ball; // joystic moving ball

	/// <summary>
    /// Getting funtion Joystick angle
    /// </summary>
    /// <returns>angle float date</returns>
	public float GetJosystickAngle()
    {
		float angle = CalculateAngle.XY(floatingjoystick_Ball.position, floatingjoystick_Background.position);
		//Show.LogMagenta(angle);
		return angle;

	}
	
}
