using System.Collections;
using UnityEngine;

public class AirAIUnit : MonoBehaviour
{
    public Transform target;
    public float speed = 20f;
    public float rotationSpeed = 5f;

    private GameObject baseStructure;

    public LayerMask structures;

    Vector3[] path;
    int targetIndex;

    GameObject[] listOfRefineries;
    int numOfRefineries = 0;
    int prev_numOfRefineries = 0;

    public float damagePerShot = 1f;

    private float shotTimer;
    public float shotDelay;

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

        if (numOfRefineries == 0)
            listOfRefineries = null;

        if (prev_numOfRefineries != numOfRefineries)
        {
            AdjustTarget();
            prev_numOfRefineries = numOfRefineries;
        }

        AttackStructures();
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

    private void AdjustTarget()
    {
        if (numOfRefineries > 0)
        {
            target = listOfRefineries[0].transform;
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("BaseStruct").transform;
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        }

    }

    private void AttackStructures()
    {
        if (baseStructure != null)
        {
            bool inRange = Physics.CheckBox(transform.position - new Vector3(0, 10, 0), new Vector3(2, 2, 2), Quaternion.identity, structures);

            if (inRange)
            {
                shotTimer += Time.deltaTime;
                if(shotTimer >= shotDelay)
                {
                    shotTimer -= shotDelay;

                    if (listOfRefineries != null)
                    {
                        if(target == listOfRefineries[0].transform)
                        {
                            listOfRefineries[0].GetComponent<ObjectHealth>().TakeDamage(damagePerShot);
                            Debug.Log("Bang");
                            Debug.Log("Refinery Health = " + listOfRefineries[0].GetComponent<ObjectHealth>().DisplayHealth() + "%");
                        }
                    }
                    else
                    {
                        baseStructure.GetComponent<ObjectHealth>().TakeDamage(damagePerShot);
                        Debug.Log("Bang");
                        Debug.Log(baseStructure.GetComponent<ObjectHealth>().DisplayHealth() + "%");
                    }
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
