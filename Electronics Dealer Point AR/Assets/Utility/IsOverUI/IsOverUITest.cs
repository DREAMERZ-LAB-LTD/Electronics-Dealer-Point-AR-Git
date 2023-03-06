using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SingleTouchSystem))]
public class IsOverUITest : MonoBehaviour
{
    SingleTouchSystem singleTouchSystem;
    void OnEnable()
    {
        singleTouchSystem = GetComponent<SingleTouchSystem>();
        
        singleTouchSystem.touchBegainCallBack += TouchBegainCallBack;
        singleTouchSystem.touchMovedCallBack += TouchMoveCallBack;
        singleTouchSystem.touchEndedCallBack += TouchEndCallBack;

    }
    private void OnDisable()
    {
        singleTouchSystem.touchBegainCallBack -= TouchBegainCallBack;
        singleTouchSystem.touchMovedCallBack -= TouchMoveCallBack;
        singleTouchSystem.touchEndedCallBack -= TouchEndCallBack;

    }

    
    private void TouchBegainCallBack(Touch touch)
    {
        Show.LogRed(touch.position);
        if (IsOverUI.isIt) Show.LogGreen("Over UI");
        else { Show.LogGray("Not over UI");
            
        }
    }
    private void TouchMoveCallBack(Touch touch)
    {
        Show.LogRed(touch.position);
        if (IsOverUI.isIt) Show.LogRed("Over UI");
        else
        {
            Show.LogGray("Not over UI");

        }
    }
    private void TouchEndCallBack(Touch touch)
    {
        Show.LogRed(touch.position);
        if (IsOverUI.isIt) Show.LogBlue("Over UI");
        else
        {
            Show.LogGray("Not over UI");

        }
    }

}
