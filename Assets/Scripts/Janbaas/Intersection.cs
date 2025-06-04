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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public IntersectionDirection GetDirection()
    {
        int index = 0;
        switch (intersectionType)
        {
            case IntersectionType.Way3:
                index = UnityEngine.Random.Range(0, 2);
                break;
            case IntersectionType.Way4:
                index = UnityEngine.Random.Range(0, 3);
                break;
            case IntersectionType.UTurn:
                index = 3; // U-turn is always backward
                break;
            case IntersectionType.Way2:
                index = UnityEngine.Random.Range(0, 1);
                break;

        }

        return (IntersectionDirection)directions.GetValue(index);
    }
}
    public enum IntersectionType
{
    Way3,
    Way4,
    UTurn,
    Way2
}
public enum IntersectionDirection
{
    Left,
    Right,
    Forward,
}
