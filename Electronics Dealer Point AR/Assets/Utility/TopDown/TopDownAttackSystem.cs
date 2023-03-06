using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Top down player shooting controlling system.
/// </summary>
[RequireComponent(typeof(SingleTouchSystem))]
[RequireComponent(typeof(EnemyDetect))]
public class TopDownAttackSystem : MonoBehaviour
{

    #region call Back System
    EnemyDetect enemyDetect;
    SingleTouchSystem singleTouchSystem;
    void OnEnable()
    {
        singleTouchSystem = GetComponent<SingleTouchSystem>();
        singleTouchSystem.touchBegainCallBack += OffAttackCallBack;
        singleTouchSystem.touchEndedCallBack += AttackCallBack;

        enemyDetect = GetComponent<EnemyDetect>();
    }
    private void OnDisable()
    {
        singleTouchSystem.touchBegainCallBack -= OffAttackCallBack;
        singleTouchSystem.touchEndedCallBack -= AttackCallBack;
    }
    // enabling enemy detect and shooting
    public void AttackCallBack(Touch touch)
    {
       enemyDetect.enabled = true;
    }
    // disabling enemy detect and shooting
    public void OffAttackCallBack(Touch touch)
    {
       enemyDetect.enabled = false;
    }
    #endregion
}
