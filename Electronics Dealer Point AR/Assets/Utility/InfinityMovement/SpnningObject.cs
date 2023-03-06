using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// The Object attatch with this script will spin finity time.
/// 
/// </summary>
public class SpnningObject : MonoBehaviour
{
	[SerializeField] float speed = 4f;
	[SerializeField] Vector3 localRotationSide; // axis enable 1 axis disbale 0 
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		transform.Rotate(localRotationSide * speed * Time.deltaTime);
		
	}
   
}
