using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemies : MonoBehaviour
{
    public float currentSpeed;
    public float maxSpeed = 10f;

    private float distToTarget;

    public Transform target;

    static float t = 0.0f;

    private int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.points[0];
        currentSpeed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * currentSpeed * Time.deltaTime, Space.World);

        distToTarget = Vector3.Distance(transform.position, target.position);

        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            t += 1.2f * Time.deltaTime;
            currentSpeed = Mathf.Lerp(maxSpeed, 0, t);
        }

        if (distToTarget < 0.1f)
            GetNextWaypoint();
    }

    void GetNextWaypoint()
    {
        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    
}
