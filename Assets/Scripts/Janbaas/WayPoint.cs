using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] private Transform nextPoint;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    public bool leftLane = false;
    public Transform Nextpoint
    {
        get { return nextPoint; }
        set { nextPoint = value; }
    }
    public Transform Leftpoint
    {
        get { return leftPoint; }
        set { leftPoint = value; }
    }
    public Transform Rightpoint
    {
        get { return rightPoint; }
        set { rightPoint = value; }
    }
}
