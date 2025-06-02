using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    [SerializeField] List<WayPoint> wayPoints;
    public static WayPointManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<WayPoint> WayPoints
    {
        get { return wayPoints; }
        set { wayPoints = value; }
    }
    public WayPoint FindClosestWayPoint(Vector3 position,Vehicles vehicles)
    {
        WayPoint closestWayPoint = null;
        float distance = float.MaxValue;
        foreach (var wayPoint in wayPoints)
        {
            float currentDistance = Vector3.Distance(position, wayPoint.transform.position);
            bool flag = wayPoint.leftLane == vehicles.LeftLane;
            if (currentDistance < distance&&flag)
            {
                distance = currentDistance;
                closestWayPoint = wayPoint;
            }
        }
        return closestWayPoint;
    }
    public void AddToList(WayPoint wayPoint)
    {
        if (wayPoints.Contains(wayPoint)) return;
        wayPoints.Add(wayPoint);
    }
    public WayPoint GetWayPoint(Transform way,Vehicles vehicle)
    {
        return FindClosestWayPoint(way.position,vehicle); 
    }
    private void OnDrawGizmos()
    {
        
        foreach(var wayPoint in WayPoints)
        {
            Gizmos.color = Color.black;
            if (wayPoint.Nextpoint == null) continue;
            Gizmos.DrawLine(wayPoint.transform.position, wayPoint.Nextpoint.position);
            if(wayPoint.Leftpoint != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(wayPoint.transform.position, wayPoint.Leftpoint.position);
            }
            if (wayPoint.Rightpoint != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(wayPoint.transform.position, wayPoint.Rightpoint.position);
            }
        }
    }

}
