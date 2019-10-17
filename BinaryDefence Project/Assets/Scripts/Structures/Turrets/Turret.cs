using UnityEngine;
using System.Linq;

public class Turret : MonoBehaviour
{
    #region Variables
    private Transform target;
    [Header("Fire Rate & Range")]
    [Tooltip("Radius of the range for the turret(s).")]
    public float turretRange = 10.0f;
    public float fireRate = 2.5f;
    private float fireCountDown = 0;

    public AudioClip shootSounds;
    private AudioSource audioFile;
    public float lowVolLevels;
    public float highVolLevels;
    
    [Header("Rotation")]
    [Tooltip("The speed of the turret head turn rate.")]
    public float turnSpeed = 5.0f;
    [Tooltip("Add the TurretHead Prefab here.")]
    public Transform turretRotationPart;

    public GameObject bulletPrefab;
    [Header("FirePoints")]
    [Tooltip("Firepoints for the turrets.")]
    public Transform firePoint1;
    public Transform firePoint2;
    public Transform firePoint3;
    public Transform firePoint4;
    public Transform firePoint5;
    public Transform firePoint6;

    //public Transform[] firePoints;

    private GameObject[] targetedEnemies = null;

    public string enemyTag = "Enemy";
    private GameObject[] groundEnemies = null;

    public string enemyTagAir = "AirEnemy";
    private GameObject[] airEnemies = null;

    #endregion

    private void Awake()
    {
        audioFile = GetComponent<AudioSource>();
    }


    // Start is called before the first frame update
    void Start()
    {
        //calling "UpdatingTarget" twice every second, NOT FRAME!
        InvokeRepeating("UpdatingTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()

    {

        if (target == null)
            return;

        Vector3 direction = target.position - transform.position;
        //look rotation for turrets
        Quaternion Rotate = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(turretRotationPart.rotation, Rotate, Time.deltaTime * turnSpeed).eulerAngles;
        turretRotationPart.rotation = Quaternion.Euler(0, rotation.y, 0);
        

        //firing for the turret
        if (fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1f / fireRate;

            Shoot2();
            fireCountDown = 1.5f / fireRate;

            Shoot3();
            fireCountDown = 2f / fireRate;

            Shoot4();
            fireCountDown = 2.5f / fireRate;

            Shoot5();
            fireCountDown = 3f / fireRate;

            Shoot6();
            fireCountDown = 3.5f / fireRate;

            //firePoint();
        }

        fireCountDown -= Time.deltaTime;
    }


    // void firePoint(Transform _firePoint)
    // {
    //     GameObject bulletFIRE = Instantiate(bulletPrefab, _firePoint.position, _firePoint.rotation);
    //     Bullet bullet = bulletFIRE.GetComponent<Bullet>();
    //
    //     if(bullet != null)
    //     {
    //         bullet.Seek(target);
    //     }
    // }
    #region Bullet Shots
    /// <summary>
    /// instanciating bullets to shoot
    /// </summary>
    void Shoot()
    {
        
        GameObject bulletGO1 = Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        Bullet bull1 = bulletGO1.GetComponent<Bullet>();

        if(bull1 != null)
        {
            bull1.Seek(target);
        }
        
    }

    void Shoot2()
    {
        GameObject bulletGO2 = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        Bullet bull2 = bulletGO2.GetComponent<Bullet>();

        if(bull2 != null)
        {
            bull2.Seek(target);
        }
    }

    void Shoot3()
    {
        GameObject bulletGO3 = Instantiate(bulletPrefab, firePoint3.position, firePoint3.rotation);
        Bullet bull3 = bulletGO3.GetComponent<Bullet>();

        if(bull3 != null)
        {
            bull3.Seek(target);
        }
    }

    void Shoot4()
    {
        GameObject bulletGO4 = Instantiate(bulletPrefab, firePoint4.position, firePoint4.rotation);
        Bullet bull4 = bulletGO4.GetComponent<Bullet>();

        if(bull4 != null)
        {
            bull4.Seek(target);
        }
    }

    void Shoot5()
    {
        GameObject bulletGO5 = Instantiate(bulletPrefab, firePoint5.position, firePoint5.rotation);
        Bullet bull5 = bulletGO5.GetComponent<Bullet>();

        if(bull5 != null)
        {
            bull5.Seek(target);
        }
    }

    void Shoot6()
    {
        GameObject bulletGO6 = Instantiate(bulletPrefab, firePoint6.position, firePoint6.rotation);
        Bullet bull6 = bulletGO6.GetComponent<Bullet>();

        if(bull6 != null)
        {
            bull6.Seek(target);
        }
    }
    #endregion
    
    void UpdatingTarget()
    {
        groundEnemies = GameObject.FindGameObjectsWithTag(enemyTag);
        airEnemies = GameObject.FindGameObjectsWithTag(enemyTagAir);

        if (this.gameObject.tag == "GroundTurret")
        {
            //finding all the eneimes that are tagged with "enemyTag" and store them into the array
            targetedEnemies = groundEnemies;
        }
        else if (this.gameObject.tag == "AATurret")
        {
            //finding all the eneimes that are tagged with "enemyTagAir" and store them into the array
            targetedEnemies = airEnemies;
        }
        else
        {
            targetedEnemies = groundEnemies.Concat(airEnemies).ToArray();
        }

        //storing the shortest distance to a enemy
        float shortDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;


            foreach (GameObject enemy in targetedEnemies)
            {
                float distToEnemy = Vector3.Distance(transform.position, enemy.transform.position);


                if (distToEnemy < shortDistance)
                {
                    shortDistance = distToEnemy;
                    nearestEnemy = enemy;

                }
            }


            //found a enemy and within the range
            if (nearestEnemy != null && shortDistance <= turretRange)
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null;
            }

        
    }
    /// <summary>
    /// Draws a sphere INSIDE UNITY not gameplay
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, turretRange);
    }
}
