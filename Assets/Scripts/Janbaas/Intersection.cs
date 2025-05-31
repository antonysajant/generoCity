using System;
using UnityEngine;

public class Intersection : MonoBehaviour
{
    Array directions = Enum.GetValues(typeof(IntersectionDirection));
    [SerializeField] IntersectionType intersectionType = IntersectionType.Way3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public IntersectionDirection GetDirection(IntersectionType inter)
    {
        int index = 0;
        switch (inter)
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
