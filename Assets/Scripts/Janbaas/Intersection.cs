using System;
using UnityEngine;

public class Intersection : MonoBehaviour
{
    Array directions = Enum.GetValues(typeof(IntersectionDirection));
    [SerializeField] IntersectionType _intersectionType = IntersectionType.Way3;
    public IntersectionType intersectionType
    {
        get => _intersectionType;
        set => _intersectionType = value;
    }
    [SerializeField] private Transform Left;
    [SerializeField] private Transform Right;
    [SerializeField] private Transform Backward;
    public Transform left
    {
        get => Left;
        set => Left = value;
    }
    public Transform right
    {
        get => Right;
        set => Right = value;
    }
    public Transform backward
    {
        get => Backward;
        set => Backward = value;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public IntersectionDirection GetDirection()
    {
        int index = 0;
        switch (intersectionType)
        {
            case IntersectionType.Way3:
                index = UnityEngine.Random.Range(0, 3);
                break;
            case IntersectionType.Way4:
                index = UnityEngine.Random.Range(0, 4);
                break;
            case IntersectionType.UTurn:
                index = 3; // U-turn is always backward
                break;

        }

        return (IntersectionDirection)directions.GetValue(index);
    }
}
    public enum IntersectionType
{
    Way3,
    Way4,
    UTurn
}
public enum IntersectionDirection
{
    Left,
    Right,
    Forward,
    Backward
}
