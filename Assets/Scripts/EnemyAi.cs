using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    // array of waypoints pulls waypoint info from Moving Platform script tab / reusable
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    // how many game units to move when speed is called / game unit = 16bit gridbox
    [SerializeField] private float speed = 2f;

    private void Update()
    {
        // if distance b/w platform and current waypoint position
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            // move to next waypoint 
            currentWaypointIndex++;
            transform.Rotate(0, 180, 0);
            // if idx >= length of array
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        else
        {
            // change position of platform toward position of the next waypoint in array
            // Time.deltaTime matches framerate of device to make sure platform moves two game units per second
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        }
    }
}