using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Waypoint Attributes")] 
    public WaypointSettings waypointSettings;

    [Space(10)] 
    private Vector3[] worldSpaceWaypoints;

    [Space(10)] 
    private Vector3 nextWaypointDirection;

    [Space(10)] 
    public int waypointIndex;

    private void Start()
    {
        InitializeObstacle();
    }

    private void InitializeObstacle()
    {
        worldSpaceWaypoints = ConvertWaypointsToWorldSpace(waypointSettings.waypoints);

        waypointIndex = 0;
    }

    private void Update()
    {
        ManagePlatform();
    }

    private void ManagePlatform()
    {
        //float distanceToNextWaypoint = ReturnDistanceToPoint()
    }

    private void ManagePlatformMovement()
    {
        
    }

    private float ReturnDistanceToPoint(Vector3 nextPoint)
    {
        return Vector3.Distance(transform.position, nextPoint);
    }

    private WaypointInfo ReturnNextPoint()
    {
        if (waypointIndex + 1 > waypointSettings.waypoints.Length)
        {
            return waypointSettings.waypoints[0];
        }
        else
        {
            return waypointSettings.waypoints[waypointIndex + 1];
        }
    }

    private Vector3 ReturnNextWaypointDirection(Vector3 nextWaypoint)
    {
        return (nextWaypoint - transform.position).normalized;
    }
    private Vector3[] ConvertWaypointsToWorldSpace(WaypointInfo[] waypoints)
    {
        Vector3[] newWaypoints = new Vector3[waypoints.Length];

        for (int i = 0; i < waypoints.Length; i++)
        {
            newWaypoints[i] = transform.TransformPoint(waypoints[i].waypointPosition);
        }

        return newWaypoints;
    }

    private void OnDrawGizmos()
    {
        if (waypointSettings.waypoints.Length > 0)
        {
            Vector3[] newWorldSpacePoints = ConvertWaypointsToWorldSpace(waypointSettings.waypoints);

            Gizmos.color = Color.blue;
        
            for (int i = 0; i < newWorldSpacePoints.Length; i++)
            {
                Gizmos.DrawWireSphere(newWorldSpacePoints[i], 0.25f);
            }
        }    
    }
}

[System.Serializable]
public struct WaypointSettings
{
    [Header("Waypoint Settings")] 
    public WaypointInfo[] waypoints;

    [Space(10)] 
    public float waypointSpeed;
}

[System.Serializable]
public struct WaypointInfo
{
    [Header("Waypoint Info")] 
    public Vector3 waypointPosition;
}