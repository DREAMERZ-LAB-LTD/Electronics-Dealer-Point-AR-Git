using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
/// <summary>
/// This script will be used to FPS rotation
/// EmptyObject>Body>Head(Camera) + FPS_ROtation
/// </summary>
[RequireComponent(typeof(SingleTouchSystem))]
public class FPS_Rotation : MonoBehaviour
{
    [SerializeField] Transform body; //parent of head(Camera)
    [SerializeField] bool limitX = false; // enable or disable X limit 
    [ShowIf("limitX")] [SerializeField] float rotXmin = -25f; // X min
    [ShowIf("limitX")] [SerializeField] float rotXmax = 25f; // X max

    [SerializeField] bool limitY = false; // enable or disable Y limit 
    [ShowIf("limitY")] [SerializeField] float rotYmin = -25f; // Y min
    [ShowIf("limitY")] [SerializeField] float rotYmax = 25f; // Y max
    [SerializeField] float sencativity = 100f; //rotation sencativity

    #region CallBack
    SingleTouchSystem singleTouchSystem;
    private void OnEnable()
    {
        singleTouchSystem = GetComponent<SingleTouchSystem>();
        singleTouchSystem.touchMovedCallBack += Rotate;
    }
    private void OnDisable()
    {
        singleTouchSystem.touchMovedCallBack -= Rotate;
    }
    #endregion

    Vector3 vec3;
    float rotX;
    float rotY;
    float dt;

    /// <summary>
    /// Menual Rotation Setup in private veriables
    /// </summary>
    /// <param name="x">X axis</param>
    /// <param name="y">Y Axis</param>
    public void MenualRotationSetUP(float x, float y) { rotX = -x; rotY = y; }

    /// <summary>
    /// Touch Move Callback from single touch system script
    /// And using that the rotation function works
    /// </summary>
    /// <param name="touch">touch Data</param>
    private void Rotate(Touch touch)
    {
        dt = Time.deltaTime;
        //  Vector2 pos = touch.position;
        rotX += touch.deltaPosition.y * sencativity * Mathf.Deg2Rad * dt;
        rotY += touch.deltaPosition.x * sencativity * Mathf.Deg2Rad * dt;

        if (limitX) rotX = Mathf.Clamp(rotX, rotXmin, rotXmax);
        if (limitY) rotY = Mathf.Clamp(rotY, rotYmin, rotYmax);

        vec3.x = -rotX;
        vec3.y = float.Epsilon;
        vec3.z = float.Epsilon;
        // transform.Rotate(-vec3, spaceCamra);
        //transform.localEulerAngles = vec3;
        transform.DOLocalRotate(vec3, 0);
        vec3.x = float.Epsilon;
        vec3.y = rotY;
        vec3.z = float.Epsilon;
        // cameraHulder.Rotate(vec3, spaceHolder);
        //body.localEulerAngles = vec3;
        body.DOLocalRotate(vec3, 0);
    }


}
