using UnityEngine;

public class Turret : MonoBehaviour
{
    #region Variables
    private Transform target;
    [Tooltip("Radius of the range for the turret(s).")]
    public float turretRange = 10.0f;
    public float fireRate = 2.5f;
    private float fireCountDown = 0;
    [Tooltip("The speed of the turret head turn rate.")]
    public float turnSpeed = 5.0f;
    [Tooltip("Add the TurretHead Prefab here.")]
    public Transform turretRotationPart;


    public GameObject bulletPrefab;
    public Transform firePoint;

    private GameObject[] enemies = null;

    public string enemyTag = "Enemy";
    public string enemyTagAir = "AirEnemy";
    
    #endregion
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
        }

        fireCountDown -= Time.deltaTime;
    }
    
    /// <summary>
    /// instanciating bullets to shoot
    /// </summary>
    void Shoot()
    {

        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bull = bulletGO.GetComponent<Bullet>();

        if(bull != null)
        {
            bull.Seek(target);
        }

    }


    void UpdatingTarget()
    {
        if (this.gameObject.tag != "AATurret")
        {
            //finding all the eneimes that are tagged with "enemyTag" and store them into the array
            enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        }
        else
        {
            //finding all the eneimes that are tagged with "enemyTagAir" and store them into the array
            enemies = GameObject.FindGameObjectsWithTag(enemyTagAir);
        }

            //storing the shortest distance to a enemy
            float shortDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;


            foreach (GameObject enemy in enemies)
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, turretRange);
    }
}
