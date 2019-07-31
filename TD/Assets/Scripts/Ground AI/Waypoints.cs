using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    // List of all of the waypoints for the ai to follow
    public static Transform[] points;

    private void Awake()
    {
        // Makes the list the correct size for the amount of waypoints
        points = new Transform[transform.childCount];

        // Adds each child of the gameobject into the list
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
