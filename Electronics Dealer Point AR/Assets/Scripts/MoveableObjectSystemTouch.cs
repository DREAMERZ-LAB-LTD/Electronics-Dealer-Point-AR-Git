using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Touch = UnityEngine.Touch;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using static RayCastHitSystem;


[RequireComponent(typeof(SingleTouchSystem))]
[RequireComponent(typeof(ARAnchorManager))]
[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class MoveableObjectSystemTouch : MonoBehaviour
{
    [SerializeField] Text ray;
    [SerializeField] Text rayPlane;
    [SerializeField] LayerMask layerMask;
  //  [SerializeField][ReadOnly] Touch tempTouch;
    [SerializeField][ReadOnly] bool touchBegain = false;
    [SerializeField][ReadOnly] bool touchMove = false;
    [SerializeField][ReadOnly] bool touchEnd = false;
    [SerializeField][ReadOnly] GameObject touchObject = null;
    [SerializeField][ReadOnly] GameObject touchObjectFinal = null;

    [SerializeField] float longTouchLength = 2f;

    #region callback Events Start
    SingleTouchSystem singleTouchSystem;
    private void OnEnable()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        m_AnchorManager = GetComponent<ARAnchorManager>();
        m_PlaneManager = GetComponent<ARPlaneManager>();
        m_AnchorPoints = new List<ARAnchor>();

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
    #endregion callback Events End

    #region Callback function Start
    private void TouchBegainCallBack(Touch touch)
    {
        touchObjectFinal = null;
        if (Input.touchCount == 1)
        {

            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit, 1000, layerMask))
            {
                ray.text = raycastHit.transform.name;
                Show.LogGreen(raycastHit.collider.gameObject.name);
                if (touchObject == null)
                {
                    touchObject = raycastHit.collider.gameObject;
                    EnableLongTouch();
                  //  var list = gameObject.GetComponent<AnchorCreator>().m_AnchorPoints;
                  //  foreach (var l in list)
                    {
                        if (touchObject.GetComponent<ObjectRotator>())
                        {
                            touchObject.GetComponent<ObjectRotator>().canRotate = false;
                        }
                    }
                }

            }
            else
            {
                var list = gameObject.GetComponent<AnchorCreator>().m_AnchorPoints;
                foreach (var l in list)
                {
                    if (l.GetComponent<ObjectRotator>())
                    {
                        l.GetComponent<ObjectRotator>().canRotate = true;
                    }
                }

            }
        }


        Show.LogRed(touch.position);
       // tempTouch = touch;
        touchBegain = true;
        touchMove = false;
        touchEnd = false;
        
    }
    private void TouchMoveCallBack(UnityEngine.Touch touch)
    {
        Show.LogGreen(touch.position);
        touchBegain = false;
        touchMove = true;
        touchEnd = false;
        EndLongTouch();
    }
    private void TouchEndCallBack(Touch touch)
    {
        Show.LogBlue(touch.position);
        touchBegain = false;
        touchMove = false;
        touchEnd = true;
        if (touchObject.GetComponent<ObjectRotator>())
        {
            touchObject.GetComponent<ObjectRotator>().canRotate = true;
        }
        if (longActivated)touchObject.transform.position -= new Vector3(0, 0.1f, 0);
        longActivated = false;
        touchObject = null;
        touchObjectFinal = null;
        EndLongTouch();
    }

    #endregion Callback function End

    private void EnableLongTouch()
    {
        tempLongTouchLength = longTouchLength;
        longActivated = false;
        longTouch = true;
        touchObject = null;
        
    }
    private void EndLongTouch()
    {
        longTouch = false;
        //touchObject = null;
    }

    bool longActivated = false;
    bool longTouch = false;
    float tempLongTouchLength = 1f;
    private void Update()
    {
        if (longTouch)
        {
            tempLongTouchLength -= Time.deltaTime;

            if (tempLongTouchLength > 0)
            {
                RayShoot();
            }

            else if (tempLongTouchLength <= 0)
            {
                touchObjectFinal = touchObject;
                Show.LogYellow("LongTouch Active");
                {
                    if (touchObject.GetComponent<ObjectRotator>())
                    {
                        ray.text = "Rotate Off";
                        touchObject.GetComponent<ObjectRotator>().canRotate = false;
                    }
                }
                longTouch = false;
                longActivated = true;
            }
        }
        else {
         if (touchObjectFinal != null)
            {
                ObjectMoving();
            }
        }
    }

    private void RayShoot()
    {
        if (Input.touchCount == 1)
        {
            
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit, 1000, layerMask))
            {
                ray.text = raycastHit.transform.name;
                Show.LogGreen(raycastHit.collider.gameObject.name);
                if (touchObject == null)
                {
                    touchObject = raycastHit.collider.gameObject;
                }
                else if (touchObject != raycastHit.collider.gameObject)
                {
                    EndLongTouch();
                }
            }
            else {
                //if(touchObject != null)
                //{
                //    ObjectMoving();
                //}
            }
        }
    }

    private void ObjectMoving()
    {
        // If there is no tap, then simply do nothing until the next call to Update().
        if (Input.touchCount == 0)
            return;

        var touch = Input.GetTouch(0);
        Debug.Log("Object Name " + touchObject.name);
        if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
        {

            
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            var hitPose = s_Hits[0].pose;
            var hitTrackableId = s_Hits[0].trackableId;
            var hitPlane = m_PlaneManager.GetPlane(hitTrackableId);
            var anchor = m_AnchorManager.AttachAnchor(hitPlane, hitPose);
            touchObjectFinal.transform.position = anchor.transform.position;
            touchObjectFinal.transform.position += new Vector3(0, 0.1f, 0);
            Debug.Log("Plane Name " + anchor.transform.name);
            rayPlane.text = anchor.transform.name;
        }
    }
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    public List<ARAnchor> m_AnchorPoints;

    ARRaycastManager m_RaycastManager;

    ARAnchorManager m_AnchorManager;

    ARPlaneManager m_PlaneManager;
}
