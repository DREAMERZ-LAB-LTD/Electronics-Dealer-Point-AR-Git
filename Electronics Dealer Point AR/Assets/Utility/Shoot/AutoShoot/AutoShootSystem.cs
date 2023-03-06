using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


/// <summary>
/// Auto Shoot system
/// </summary>
[RequireComponent(typeof(ObjectPooler))]
[RequireComponent(typeof(EnemyDetect))]
[RequireComponent(typeof(ShootSystem))]
public class AutoShootSystem : MonoBehaviour
{
    [BoxGroup("Inspactor Change")][SerializeField] float attackIntervalTime = 1f; // interval between shooting
    [BoxGroup("Runtime Change")] public bool canAttack = false; // can attack or not
    [BoxGroup("Runtime Change")] [SerializeField] Transform targetTransfrom; // target referance
    
    #region CallBack
   
    EnemyDetect enemyDetect;
    ShootSystem shootSystem;
    void OnEnable()
    {
        shootSystem = GetComponent<ShootSystem>();

         enemyDetect =  GetComponent<EnemyDetect>();
        enemyDetect.ShareDetectTargetEvent += SetTarget;
    }
    void OnDisable()
    {
        enemyDetect.ShareDetectTargetEvent -= SetTarget;
        targetTransfrom =  null;
    }

    void SetTarget(Transform tra)
    {
        if(tra == null) OffAttack();
        else{
            targetTransfrom = tra;
            shootSystem.targetTransfrom = tra;
            Attack();
        }
    }
    #endregion

    #region  Attack

    float tempTime = 0;
    private void Update()
    {
        // calling after interval of time
        if(canAttack && tempTime <= 0) 
        {
            shootSystem.Shoot();
            tempTime = attackIntervalTime;
        }
        tempTime -= Time.deltaTime;
    }

    /// <summary>
    /// Enable attack
    /// </summary>
    [Button]
    public void Attack()
    {
        canAttack = true;
    }

    /// <summary>
    /// Disable attack
    /// </summary>
    [Button]
    public void OffAttack()
    {
       canAttack = false;
       tempTime = 0;
    }

    private IEnumerator AttackCo()
    {
        if(targetTransfrom != null)
        {
            Show.Log("Attack".Color(Color.red).Bold());
            shootSystem.Shoot();
        }
        yield return new WaitForSecondsRealtime(attackIntervalTime);
        Attack();
    }
    
    #endregion
}
