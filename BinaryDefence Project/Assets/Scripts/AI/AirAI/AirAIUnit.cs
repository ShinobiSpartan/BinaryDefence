using System.Collections;
using UnityEngine;

public class AirAIUnit : MonoBehaviour
{
    public Transform target;
    public float speed = 20f;
    public float rotationSpeed = 5f;

    Vector3[] path;
    int targetIndex = 0;

    GameObject baseStructure;
    public LayerMask baseStructMask;

    private float shotTimer;
    public float shotDelay;

    public int damagePerShot;

    GameObject[] listOfRefineries = null;
    int numOfRefineries;

    private void Start()
    {
        baseStructure = GameObject.FindGameObjectWithTag("BaseStruct");
        target = baseStructure.transform;
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    private void Update()
    {

        listOfRefineries = GameObject.FindGameObjectsWithTag("Refinery");
        numOfRefineries = listOfRefineries.Length;

        AttackBase();

        if (numOfRefineries < 1)
            return;
        else
            target = listOfRefineries[0].transform;

    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    targetIndex = 0;
                    path = new Vector3[0];
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            Vector3 targetDir = currentWaypoint - this.transform.position;
            float step = rotationSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.00f);
            transform.rotation = Quaternion.LookRotation(newDir);

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;

        }
    }

    void AttackBase()
    {
        if (baseStructure != null)
        {
            bool inRange = Physics.CheckSphere(transform.position, 3.0f, baseStructMask);

            // If the enemy has stopped in front of the base
            if (speed == 0 && inRange)
            {
                // Start the shot delay timer
                shotTimer += Time.deltaTime;
                // When the shot delay timer maxes out
                if (shotTimer >= shotDelay)
                {
                    // Shoot
                    shotTimer -= shotDelay;
                    baseStructure.GetComponent<ObjectHealth>().TakeDamage(damagePerShot);
                    Debug.Log("Bang");
                    Debug.Log(baseStructure.GetComponent<ObjectHealth>().DisplayHealth() + "%");
                }

            }
        }
    }
    
    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
