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
    public WayPoint FindClosestWayPoint(Vector3 position)
    {
        WayPoint closestWayPoint = null;
        float distance = float.MaxValue;
        foreach (var wayPoint in wayPoints)
        {
            float currentDistance = Vector3.Distance(position, wayPoint.transform.position);
            if (currentDistance < distance)
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

}
