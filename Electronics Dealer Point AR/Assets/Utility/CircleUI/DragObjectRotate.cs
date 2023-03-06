using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script rotate world space canvas in to right and left.
/// </summary>
[RequireComponent(typeof(SingleTouchSystem))]
public class DragObjectRotate : MonoBehaviour
{
    [SerializeField] GameObject canvas;
	[SerializeField] float sencativity = 5f;
	Vector3 privpos = Vector3.zero;

    #region CallBack
    SingleTouchSystem singleTouchSystem;
    private void OnEnable()
    {
		singleTouchSystem = GetComponent<SingleTouchSystem>();
		singleTouchSystem.touchBegainCallBack += TouchBegainCallBack;
		singleTouchSystem.touchMovedCallBack += TouchMoveCallBack;
    }
    private void OnDisable()
    {
		singleTouchSystem.touchBegainCallBack -= TouchBegainCallBack;
		singleTouchSystem.touchMovedCallBack -= TouchMoveCallBack;
	}
    #endregion

    private void TouchBegainCallBack(Touch touch)=> privpos = touch.position;
	private void TouchMoveCallBack(Touch touch)
	{
		
		//Left 
		if (privpos.x > touch.position.x && 0.1 < Mathf.Abs(privpos.x - touch.position.x))
		{

			canvas.transform.Rotate(Vector3.up * sencativity * Time.deltaTime, Space.World);


		}
		//Right
		else if (privpos.x < touch.position.x && 0.1 < Mathf.Abs(privpos.x - touch.position.x))
		{


			canvas.transform.Rotate(Vector3.down * sencativity * Time.deltaTime, Space.World);

		}
		privpos = touch.position;
	}


	
}
