using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SingleTouchSystem))]
public class SingleTouchCallBackTest : MonoBehaviour
{
    SingleTouchSystem singleTouchSystem;
    void OnEnable()
    {
        singleTouchSystem = GetComponent<SingleTouchSystem>();
        singleTouchSystem.updateCallBack += UpdateCallBack;
        singleTouchSystem.touchBegainCallBack += TouchBegainCallBack;
        singleTouchSystem.touchMovedCallBack += TouchMoveCallBack;
        singleTouchSystem.touchEndedCallBack += TouchEndCallBack;
    }
    private void OnDisable()
    {
        singleTouchSystem.updateCallBack -= UpdateCallBack;
        singleTouchSystem.touchBegainCallBack -= TouchBegainCallBack;
        singleTouchSystem.touchMovedCallBack -= TouchMoveCallBack;
        singleTouchSystem.touchEndedCallBack -= TouchEndCallBack;
    }

    private void UpdateCallBack()
    {
        Show.Log("Update");
    }
    private void TouchBegainCallBack(Touch touch)
    {
        Show.LogRed(touch.position);
    }
    private void TouchMoveCallBack(Touch touch)
    {
        Show.LogGreen(touch.position);
    }
    private void TouchEndCallBack(Touch touch)
    {
        Show.LogBlue(touch.position);
    }
    
}
