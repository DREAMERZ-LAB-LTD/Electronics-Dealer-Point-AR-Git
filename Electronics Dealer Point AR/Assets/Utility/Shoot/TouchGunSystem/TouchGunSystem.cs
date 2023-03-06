using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;


/// <summary>
/// This script will using single touch system and exicute gun system for mobile.
/// </summary>
[RequireComponent(typeof(SingleTouchSystem))]
[RequireComponent(typeof(ObjectPooler))]
[RequireComponent(typeof(ShootSystem))]
public class TouchGunSystem : MonoBehaviour
{
    //Gun Shooting Type
    enum GunShootingType
    {
        SingleShoot = 0,
        ContinusShoot = 1,
        
    };
    [ BoxGroup("Inspactor Change")] [SerializeField] GunShootingType gunShootingType; // see enum
    
    
   
    [ HorizontalLine(color:  EColor.Red)]
    
    //[ BoxGroup("Inspactor Change")] [ ShowIf("bulletType", BulletType.RayCast)] [ Layer] [ EnumFlags] [SerializeField] int  rayTargetlayer;
    [ BoxGroup("Inspactor Change")] [ ShowIf("gunType", GunShootingType.ContinusShoot)][SerializeField] int mechinGunRoundPerSecond = 10; // shooting rate
   
    [ HorizontalLine(color:  EColor.Blue)]
    [ BoxGroup("Inspactor Change")] [SerializeField] Transform shootPoint; // shooting point 
    [ BoxGroup("Inspactor Change")] [SerializeField] Transform gunBody; // gun body
    [ BoxGroup("Inspactor Change")] [SerializeField] float recallForce = 0.3f; // gun recall force => this might change via animation

    [ Foldout("Runtime Change")][SerializeField] bool isPressed = false; // is the touch pressed
    [ Foldout("Runtime Change")] [SerializeField] bool canShootMechinGun = false; // can shoot mechingun
    [ Foldout("Runtime Change")] [SerializeField] bool canShootNormalGun = false; // can shoot Normal gun



    #region call back system
    SingleTouchSystem gunCallBack;
    ObjectPooler objectPooler;
    ShootSystem shootSystem;

    Vector3 gunbodyOriginPos;
    
    private void OnEnable()
    {
        shootSystem = GetComponent<ShootSystem>();
        gunCallBack = GetComponent<SingleTouchSystem>();
        gunCallBack.updateCallBack += CallingShoot;
        gunCallBack.touchBegainCallBack += GunTriggerPressed;
        gunCallBack.touchEndedCallBack += GunTriggerRelease;
    }
    private void OnDisable()
    {
        gunCallBack.updateCallBack -= CallingShoot;
        gunCallBack.touchBegainCallBack -= GunTriggerPressed;
        gunCallBack.touchEndedCallBack -= GunTriggerRelease;
    }
    private void GunTriggerPressed(Touch touch)
    {
        isPressed = true;
        canShootNormalGun = false;
        canShootMechinGun = true;
    }
    private void GunTriggerRelease(Touch touch)
    {
        isPressed = false;
        canShootNormalGun = true;
        canShootMechinGun = false;
    }
    #endregion

    /// <summary>
    /// Calling for shoot
    /// This function will desied what kind of shoot will happed
    /// </summary>
    private void CallingShoot()
    {
        
        if (gunShootingType == GunShootingType.ContinusShoot)
        {
            if (isPressed) StartCoroutine(MechinGun());
        }
        else if (gunShootingType == GunShootingType.SingleShoot)
        {
            if (!isPressed)
            {
                if (canShootNormalGun)
                {
                    NormalGun();
                    canShootNormalGun = false;
                    isPressed = false;
                }
            }
        }
        
    }
    /// <summary>
    /// Meching Gun Coroutine for multiple shoot call
    /// </summary>
    /// <returns></returns>
    private IEnumerator MechinGun()
    {
        if (canShootMechinGun)
        {
            NormalGun();
            canShootMechinGun = false;
            float time = 1f / mechinGunRoundPerSecond;
            //Debug.Log(time);

            yield return new WaitForSecondsRealtime(time);
            canShootMechinGun = true;
        }
        else
        {
            yield return null;
        }
    }

    /// <summary>
    /// Normal Shoot call
    /// </summary>
    private void NormalGun()
    {

        if (shootPoint == null) return;
        shootSystem.Shoot();
    }
   
}
