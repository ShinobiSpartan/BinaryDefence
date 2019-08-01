using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemies : MonoBehaviour
{
    // Max speed the AI will go
    public float maxSpeed = 10f;
    // Keeps track of the current speed of the AI 
    private float currentSpeed;

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
    private float percentToTarget = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the target to the first waypoint
        target = Waypoints.points[0];
        // Current speed is set to the max speed
        currentSpeed = maxSpeed;
        // Get the position of the last waypoint
        finalWaypoint = Waypoints.points[Waypoints.points.Length - 1];
        // Get the distance between the last two waypoints
        distBetweenLastTargets = Vector3.Distance(Waypoints.points[Waypoints.points.Length - 2].position, Waypoints.points[Waypoints.points.Length - 1].position);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculates what direction the AI should be moving in
        Vector3 dir = target.position - transform.position;
        // Moves the AI towards the next waypoint
        transform.Translate(dir.normalized * currentSpeed * Time.deltaTime, Space.World);

        // Finds the distance to the current target
        distToTarget = Vector3.Distance(transform.position, target.position);

        // If the AI is at the waypoint
        if (distToTarget <= 0.2f)
        {
            // Get the next waypoint
            GetNextWaypoint();
        }
        // Unless the current target is the final waypoint
        else if(target == finalWaypoint)
        {
            // Work out the percentage of the distance to the final waypoint
            percentToTarget = (distToTarget / distBetweenLastTargets);

            // Slow down the AI accordingly
            currentSpeed = percentToTarget * maxSpeed;
        } 
    }

    void GetNextWaypoint()
    {
        // If the Enemy reaches the last waypoint
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            // Set the speed of the enemy to 0
           currentSpeed = 0;
            return;
        }        

        // Adds 1 to the index
        waypointIndex++;
        // Sets the next target to the waypoint of the specified index
        target = Waypoints.points[waypointIndex];
    }

    
}
