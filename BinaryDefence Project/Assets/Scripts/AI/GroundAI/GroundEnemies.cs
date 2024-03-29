﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemies : MonoBehaviour
{
    // Max speed the AI will go
    public float maxAISpeed = 10f;
    // Keeps track of the current speed of the AI 
    private float currentSpeed;

    public float rotationSpeed = 5f;

    // Distance to the next waypoint
    private float distToTarget;
    // Distance between the last 2 waypoints
    private float distBetweenLastTargets;

    // Coordiantes of the next waypoint the ai is tracking to
    private Transform target;
    // Coordiantes of the last waypoint in the list
    private Transform finalWaypoint;

    // Whereabouts in the list of waypoints the AI is tracking to
    private int waypointIndex = 0;

    // Distance to final target as a percentage
    private float percentToTarget = 0;

    // Game object representing the 'Base' structure
    private GameObject baseStructure;
    public LayerMask baseStructMask;

    // Amount of damage the ground units do (to the Base Structure)
    public float damagePerShot = 1f;

    // Delay between each shot from the enemy
    private float shotTimer;
    public float shotDelay;

    Animator animations;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the target to the first waypoint
        target = Waypoints.points[0];
        // Current speed is set to the max speed
        currentSpeed = maxAISpeed;
        // Get the position of the last waypoint
        finalWaypoint = Waypoints.points[Waypoints.points.Length - 1];
        // Get the distance between the last two waypoints
        distBetweenLastTargets = Vector3.Distance(Waypoints.points[Waypoints.points.Length - 2].position, Waypoints.points[Waypoints.points.Length - 1].position);

        animations = GetComponent<Animator>();
        // Find the gameobject tagged as the Base structure
        if(baseStructure == null)
        {
            baseStructure = GameObject.FindGameObjectWithTag("BaseStruct");
        }
        if (currentSpeed > 0.01)
        {
            animations.SetFloat("Walking", currentSpeed);
        }

        // Sets the time delay for the ai shots
        shotTimer = shotDelay;
    }

    // Update is called once per frame
    void Update()
    {
        MoveAI();
        AttackBase();
    }


    void MoveAI()
    {
        // Calculates what direction the AI should be moving in
        Vector3 dir = target.position - transform.position;

        float step = rotationSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, dir, step, 0.00f);
        transform.rotation = Quaternion.LookRotation(newDir);

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
        else if (target == finalWaypoint)
        {
            // Work out the percentage of the distance to the final waypoint
            percentToTarget = (distToTarget / distBetweenLastTargets);

            // Slow down the AI accordingly
            currentSpeed = percentToTarget * maxAISpeed;
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

    void AttackBase()
    {
        if (baseStructure != null)
        {
            bool inRange = Physics.CheckSphere(transform.position, 3.0f, baseStructMask);

            // If the enemy has stopped in front of the base
            if (inRange)
            {
                // Start the shot delay timer
                shotTimer += Time.deltaTime;
                // When the shot delay timer maxes out
                animations = GetComponent<Animator>();
                animations.SetBool("Fire", true);
                if (shotTimer >= shotDelay)
                {
                    // Shoot
                    shotTimer -= shotDelay;
                    baseStructure.GetComponent<ObjectHealth>().TakeDamage(damagePerShot);
                }

            }
        }
    }


    
}
