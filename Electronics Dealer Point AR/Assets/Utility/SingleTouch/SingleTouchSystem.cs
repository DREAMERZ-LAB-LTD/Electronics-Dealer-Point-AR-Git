using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This Scirpt will through all callback for
/// Touch begain
/// Touch Move
/// Touch Move
/// Touch End
/// </summary>
public class SingleTouchSystem : MonoBehaviour
{
    [SerializeField] bool useIsOverUI; // if enable then isoverUI==true then no touch call back 
    public delegate void UpdateCallBack(); // Delegate for update
    public delegate void TouchCallBack(Touch touch); // Delegate for touch events
    public event UpdateCallBack updateCallBack; // update event
    public event TouchCallBack touchBegainCallBack; // Touch Begain event
    public event TouchCallBack touchEndedCallBack;// Touch End event
    public event TouchCallBack touchMovedCallBack; // Touch Move event

    private void Update()
    {
        Touch();
        if (updateCallBack != null) updateCallBack.Invoke(); // update call back
    }

    /// <summary>
    /// Touch function where all kind of touch will detect and through their callbacks
    /// </summary>
    bool touched;
    Touch touch;
    private void Touch()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
               // Show.LogBlack("touch begain");
                bool isit = true;
                if (useIsOverUI)
                {
                    if (IsOverUI.isIt) isit = false;
                }

                if (isit)
                {
                    touched = true;
                   // Show.LogGray("touch begain");

                    if (touchBegainCallBack != null)
                    {
                        touchBegainCallBack.Invoke(touch); // touch begain call back
                    }
                }
                
            }
            if (touch.phase == TouchPhase.Moved )
            {
                // Show.LogBlack("touch Move");
                if (touched)
                {
                    //Show.LogGray("touch Move");
                    if (touchMovedCallBack != null)
                    {
                        touchMovedCallBack.Invoke(touch);// touch Move call back
                    }
                }
            }
            if (touch.phase == TouchPhase.Ended )
            {
                if (touched)
                {
                    touched = false;
                   // Show.LogGray("touch End");
                    if (touchEndedCallBack != null)
                    {
                        touchEndedCallBack.Invoke(touch); // touch End call back
                    }
                }
            }
        }
    }
}
