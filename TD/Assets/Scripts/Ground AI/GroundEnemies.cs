using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemies : MonoBehaviour
{
    // Keeps track of the current speed of the ai unit
    public float currentSpeed;

    // Max speed the unit will go
    public float maxSpeed = 10f;

    // Distance to the next waypoint
    private float distToTarget;
    // Distance between the last 2 waypoints
    private float distBetweenLastTargets;

    // Coordiantes of the next waypoint the ai is tracking to
    public Transform target;

    // Coordiantes of the last waypoint in the list
    public Transform finalWaypoint;

    // Whereabouts in the list of waypoints the AI is tracking to
    private int waypointIndex = 0;

    // Distance to final target as a percentage
    float percentToTarget = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.points[0];
        currentSpeed = maxSpeed;
        finalWaypoint = Waypoints.points[Waypoints.points.Length - 1];
        distBetweenLastTargets = Vector3.Distance(Waypoints.points[Waypoints.points.Length - 2].position, Waypoints.points[Waypoints.points.Length - 1].position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * currentSpeed * Time.deltaTime, Space.World);

        distToTarget = Vector3.Distance(transform.position, target.position);

        if (distToTarget <= 0.2f)
            GetNextWaypoint();
        else if(target == finalWaypoint)
        {
            percentToTarget = (distToTarget / distBetweenLastTargets);

            currentSpeed = percentToTarget * maxSpeed;
        } 
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
           currentSpeed = 0;
            return;
        }        

        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    
}
