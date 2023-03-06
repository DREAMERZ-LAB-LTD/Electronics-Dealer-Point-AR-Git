using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

/// <summary>
/// This Script will detect a targeted layer and involk a callback and shate the closest enemy transfrom
/// </summary>
public class EnemyDetect : MonoBehaviour
{
    public delegate void ShareDetectTarget(Transform tra); // Delegate function for callback
    public event ShareDetectTarget ShareDetectTargetEvent; // callback when detect enemy with transfrom or null


    [BoxGroup("Inspactor Change")][SerializeField] LayerMask targetLayer; // Filter target layer
    [BoxGroup("Inspactor Change")][SerializeField] float radius = 1f; // Detect redius
    [BoxGroup("Inspactor Change")] public bool canRotate = false; // can look at after detect enemy
    [BoxGroup("Inspactor Change")] public bool canRotateXAxis = false; // can look at in X axis
    [BoxGroup("Runtime Change")][SerializeField] Collider[] detectedEnemy; // will take all detect enemy referance

    void Update()
    {
        GetTarget();
    }

   #region Get Target
    /// <summary>
    /// Geting Enemy
    /// </summary>
    private void GetTarget()
    {
      //  Show.LogRed("Get Target");
        detectedEnemy = Physics.OverlapSphere(transform.position, radius, targetLayer);
        SortViaDistance();

    }
    /// <summary>
    /// If The object is disable send a null callback
    /// </summary>
    void OnDisable()
    {
        if(ShareDetectTargetEvent != null)ShareDetectTargetEvent.Invoke(null);
    }

    /// <summary>
    /// Find the closest detected enemy and send call back with transfrom
    /// </summary>
    private void SortViaDistance()
    {
        if (detectedEnemy.Length == 0)
        { 
            if(ShareDetectTargetEvent != null) ShareDetectTargetEvent.Invoke(null);
            return;
        }

        // booble sort
        for (int i = 0; i < detectedEnemy.Length - 1; i++)
        {
            for (int j = 0; j < detectedEnemy.Length - i - 1; j++)
            {
                if (Vector3.Distance(detectedEnemy[j].transform.position, transform.position)
                    >
                    Vector3.Distance(detectedEnemy[j + 1].transform.position, transform.position))
                {
                    // swap arr[j] and arr[j+1]
                    Collider temp = detectedEnemy[j];
                    detectedEnemy[j] = detectedEnemy[j + 1];
                    detectedEnemy[j + 1] = temp;

                }
            }

        }

        transform.DOKill();
        Vector3 vec = detectedEnemy[0].transform.position;
        if(!canRotateXAxis) vec.y = transform.position.y;
        if(ShareDetectTargetEvent != null) ShareDetectTargetEvent.Invoke(detectedEnemy[0].transform);
        
        if(canRotate)transform.DOLookAt(vec, 0.05f, AxisConstraint.None).SetEase(Ease.Linear);
        
    }



#if UNITY_EDITOR
    /// <summary>
    /// Showing the detect redius gizmos
    /// </summary>
    void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(transform.position, 1f);
        //Gizmos.DrawFrustum(transform.position,  1f, 1f, 5f, 1f);
        //Gizmos.DrawIcon(transform.position, "Name", true);
        //Gizmos.DrawCube(transform.position, Vector3.one);
        Gizmos.DrawWireSphere(transform.position, radius);
        ///Gizmos.DrawWireCube(transform.position, Vector3.one);
        //Gizmos.DrawRay(transform.position, transform.position + Vector3.one);
        //Gizmos.DrawLine(transform.position, transform.position + Vector3.one);

    }
#endif
    #endregion
}
