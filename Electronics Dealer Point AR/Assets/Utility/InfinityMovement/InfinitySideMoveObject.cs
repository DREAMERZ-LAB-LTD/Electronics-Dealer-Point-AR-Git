using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// The gameobject attach with this scirpt will move -X to +X Infinity time.
/// </summary>
public class InfinitySideMoveObject : MonoBehaviour
{
	[SerializeField] float moveMin = -2f;
	[SerializeField] float moveMax = 2f;
	[SerializeField] float moveSpeedInSec = 2f;
	
	
	Vector3 vec;
	// This function is called when the object becomes enabled and active.
	protected void OnEnable()
	{
		vec = transform.position;
		Move();
	}
	
	private void Move()
	{
		transform.DOMove(new Vector3(vec.x + moveMin, vec.y, vec.z), moveSpeedInSec, false).SetEase(Ease.Linear).OnComplete(()=>
		{
			Show.Log("move compleat");
			transform.DOMove(new Vector3(vec.x + moveMax, vec.y, vec.z), moveSpeedInSec, false).SetEase(Ease.Linear).OnComplete(()=>
			{
				Show.Log("move compleat");
				Move();
			});
		});
		
	}
	
}
