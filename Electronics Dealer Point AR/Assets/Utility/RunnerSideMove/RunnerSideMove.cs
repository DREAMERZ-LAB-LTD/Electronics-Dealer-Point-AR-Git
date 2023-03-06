using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Swipe right and left and the visual body will move
/// Its more for Finity run game.
/// </summary>
[RequireComponent(typeof(SingleTouchSystem))]
public class RunnerSideMove : MonoBehaviour
{
    [SerializeField] float sensitivity = 0.3f; // move senctivity 
    [SerializeField] Vector2 minMaxSide = new Vector2(-2f, 2f); // Min Max X axis movement
    [SerializeField] Transform visualBody; // Referance of mova able object

    #region callBack
    SingleTouchSystem singleTouchSystem;
    private void OnEnable()
    {
        singleTouchSystem = GetComponent<SingleTouchSystem>();
        singleTouchSystem.touchMovedCallBack += SideMove;
        singleTouchSystem.updateCallBack += SideMoveFollower;
    }
    private void OnDisable()
    {
        singleTouchSystem.touchMovedCallBack -= SideMove;
        singleTouchSystem.updateCallBack -= SideMoveFollower;
    }
    #endregion


    Vector3 vec3;

    /// <summary>
    /// Move the attatch object(Invisable) 
    /// </summary>
    /// <param name="touch">touch data</param>
    private void SideMove(Touch touch)
    {
        vec3 = transform.localPosition;
        vec3.x += touch.deltaPosition.x * sensitivity * Time.deltaTime;
        
        vec3.x = Mathf.Clamp(vec3.x, minMaxSide.x, minMaxSide.y);
        transform.localPosition = vec3;
        /*transform.DOKill();
        transform.DOLocalMoveX(vec3.x, 0.1f, false).SetEase(Ease.Linear);*/
    }
    /// <summary>
    /// Visual body will follow the invisable object
    /// but using DoMove the problem of snaping while moving will solve
    /// </summary>
    private void SideMoveFollower()
    {
        if (visualBody != null) 
        {
            visualBody.DOKill();
            visualBody.DOMoveX(transform.position.x, 0.25f, false);
            
        }
    }
}
