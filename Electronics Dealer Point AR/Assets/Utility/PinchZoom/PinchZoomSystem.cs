using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// Pinch zoom In/Out
/// FOV will cahnge to make this effect
/// </summary>
public class PinchZoomSystem : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Vector2 zoom;
    float touchDist = 0;
    float lastDist = 0;
    [SerializeField] float zoomSpeed = 0.3f;



    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began && touch2.phase == TouchPhase.Began)
            {
                lastDist = Vector2.Distance(touch1.position, touch2.position);
            }

            if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
            {
                float newDist = Vector2.Distance(touch1.position, touch2.position);
                touchDist = lastDist - newDist;
                lastDist = newDist;

                // Your Code Here
                cam.fieldOfView += ((touchDist >= 0 ? 1 : -1) * zoomSpeed);
                if (cam.fieldOfView <= zoom.x) cam.fieldOfView = zoom.x;
                if (cam.fieldOfView >= zoom.y) cam.fieldOfView = zoom.y;

            }
        }
    }

}
