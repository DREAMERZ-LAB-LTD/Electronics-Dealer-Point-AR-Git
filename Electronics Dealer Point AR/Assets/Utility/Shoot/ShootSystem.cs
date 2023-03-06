using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;

/// <summary>
/// This Script will exicute when other Shoot controlling script will call shoot
/// this script do real bullet and ray cast both type
/// and 3 type of shoot single, caterpool & Shoot Gun
/// </summary>
public class ShootSystem : MonoBehaviour
{
    // Gun Type
    enum GunType
    {
        Single = 1,
        Catterpoll = 2,
        ShotGun = 3
    };

    // Bullet Type
    enum BulletType
    {
        RealBullet = 0,
        RayCast = 1
    };

    // [OnValueChanged("SetRedius")][ BoxGroup("Inspactor Change")][SerializeField] float attackRadius = 1f;
    [BoxGroup("Inspactor Change")][SerializeField] Transform shootPoint; // shooting point
    [BoxGroup("Inspactor Change")] [SerializeField] GunType gunType; // see enum

    [ValidateInput("CatterPoolWorks", "Catterpoll dont work with RayCast")]
    [BoxGroup("Inspactor Change")] [SerializeField] BulletType bulletType; // see enum
    [HorizontalLine(color: EColor.Red)]

    //[ BoxGroup("Inspactor Change")] [ ShowIf("bulletType", BulletType.RayCast)] [ Layer] [ EnumFlags] [SerializeField] int  rayTargetlayer;
    [BoxGroup("Inspactor Change")] [ShowIf("bulletType", BulletType.RayCast)] [SerializeField] protected LayerMask targetRayLayer; // target layer
    [BoxGroup("Inspactor Change")] [ShowIf("bulletType", BulletType.RayCast)] [SerializeField] float rayShootDistance = 100f; // shoot distance in ray

    [HorizontalLine(color: EColor.Green)]
    [BoxGroup("Inspactor Change")] [ShowIf("bulletType", BulletType.RealBullet)] [Required] [SerializeField] GameObject bullet_prefab; // real bullet referance prefab
    [BoxGroup("Inspactor Change")] [ShowIf("bulletType", BulletType.RealBullet)] [SerializeField] float bulletForce = 1000f; // Bullet force
    [BoxGroup("Inspactor Change")] [ShowIf("bulletType", BulletType.RealBullet)] [SerializeField] bool canUseGravity = false; // Bullet can effect in Gravity or not

    [HorizontalLine(color: EColor.Blue)]
    
    [BoxGroup("Inspactor Change")] [ShowIf("shootType", GunType.Catterpoll)] [InfoBox("Catterpoll not use for TouchGunSystem Script", EInfoBoxType.Normal)] [SerializeField] float catterPower; // Catter pull shooting power
    
    [BoxGroup("Inspactor Change")] [ShowIf("shootType", GunType.ShotGun)] [SerializeField] int numbefBulletInABranchFire = 5; // ShotGun single branch bullet
    [BoxGroup("Inspactor Change")] [ShowIf("shootType", GunType.ShotGun)] [SerializeField] Vector3 randomBranchPoint; // random branch point range in vector3


    [BoxGroup("Runtime Change")] public Transform targetTransfrom; // Target transfrom


    /// <summary>
    /// This Region will use only in edior and for NaughtyAttributes function exicute 
    /// </summary>
    /// 
    #region editor Helper
    bool CatterPoolWorks()
    {
        if (gunType == GunType.Catterpoll && bulletType == BulletType.RayCast) return false;
        else { return true; }
    }

    #endregion

    ObjectPooler objectPooler;


    void OnEnable()
    {
        objectPooler = GetComponent<ObjectPooler>();
        if (bullet_prefab != null) objectPooler.CreatePool(bullet_prefab, 15); // create bullet for use later from pool
     //   if (catterBullet != null) objectPooler.CreatePool(catterBullet, 15);
    }

    /// <summary>
    /// This public function will call from other shoot controlling function
    /// Based on Gun Setting in Inspector the function will desided what kind of gun this function will chose.
    /// </summary>
    public void Shoot()
    {
        if (shootPoint == null) return;
        shootPoint.localEulerAngles = Vector3.zero;
        if(GunType.Single ==  gunType)
        {
            if (BulletType.RealBullet == bulletType) RealBullet();
            else if (BulletType.RayCast == bulletType) RayBullet();
        }
        else if(GunType.Catterpoll == gunType)
        {
            if(BulletType.RealBullet  ==  bulletType && targetTransfrom != null) CettarPoll(targetTransfrom);
        }
        else if(GunType.ShotGun == gunType)
        {
            ShotGun();
        }
       
    }

    /// <summary>
    /// For exicuting Shor Gun
    /// </summary>
    private void ShotGun()
    {

        if (shootPoint == null) return;
        
        shootPoint.localEulerAngles = Vector3.zero;
        for (int i = 0; i < numbefBulletInABranchFire; i++)
        {
            RandomShotPoint();
            if (bulletType == BulletType.RealBullet) RealBullet();
            else if (bulletType == BulletType.RayCast) RayBullet();
        }
    }
    /// <summary>
    /// Creating random point for shotGun
    /// </summary>
    private void RandomShotPoint()
    {
        float randX = Random.Range(transform.localEulerAngles.x - randomBranchPoint.x, transform.localEulerAngles.x + randomBranchPoint.x);
        float randY = Random.Range(transform.localEulerAngles.y - randomBranchPoint.y, transform.localEulerAngles.y + randomBranchPoint.y);
        float randZ = Random.Range(transform.localEulerAngles.z - randomBranchPoint.z, transform.localEulerAngles.z + randomBranchPoint.z);
        shootPoint.transform.localEulerAngles = new Vector3(randX, randY, randZ);
    }

    /// <summary>
    /// Shooting real bullet
    /// </summary>
    private void RealBullet()
    {
        if (bullet_prefab == null) return;
        Show.Log("Shoot".Color(Color.gray).Italic());

        // GameObject obj = Instantiate(bullet.gameObject);
        GameObject obj = objectPooler.ReuseObject(bullet_prefab);

        obj.transform.localPosition = shootPoint.position;
        obj.transform.rotation = shootPoint.localRotation;
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            obj.AddComponent(typeof(Rigidbody));
            rb = obj.GetComponent<Rigidbody>();

        }
        if (!canUseGravity) rb.useGravity = false;
        else { rb.useGravity = true; }

        rb.velocity = Vector3.zero;
        rb.AddForce(shootPoint.forward * bulletForce);
    }

    /// <summary>
    /// Shooting Cetter pool
    /// </summary>
    /// <param name="targetTransfrom"></param>
    private void CettarPoll(Transform targetTransfrom)
    {
        if (bullet_prefab == null) return;
        Show.Log("Shoot".Color(Color.black).Italic());

        // GameObject obj = Instantiate(bullet.gameObject);
        GameObject obj = objectPooler.ReuseObject(bullet_prefab);

        obj.transform.position = shootPoint.position;
        obj.transform.rotation = shootPoint.rotation;
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            obj.AddComponent(typeof(Rigidbody));
            rb = obj.GetComponent<Rigidbody>();

        }

        rb.isKinematic = true;
        rb.useGravity = false;
        float distance = Vector3.Distance(transform.position, targetTransfrom.position);
        obj.transform.DOJump(targetTransfrom.position, catterPower, 1, distance / 4f, false).SetEase(Ease.Linear).OnComplete(() =>
        {
            obj.SetActive(false);
        });

    }

    /// <summary>
    /// Shooting ray
    /// </summary>
    private void RayBullet()
    {
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, rayShootDistance, targetRayLayer))
        {
            Show.LogGreen("Ray found " + hit.transform.name + " distance: " + hit.distance);
            Debug.DrawLine(shootPoint.position, hit.point, Color.green);
            if (hit.transform.GetComponent<RayCastHitSystem>() != null) hit.transform.GetComponent<RayCastHitSystem>().RayHit(hit);
        }
        /*else { Debug.DrawRay(shootPoint.position, shootPoint.forward, Color.red, Mathf.Infinity); }
*/
    }

}
