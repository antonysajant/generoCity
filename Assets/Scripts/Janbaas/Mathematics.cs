using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public static class Mathematics
{
    public static float VelocityCalculator(float currentVelocity, float maxVelocity, float acceleration)
    {
        float velocity = Mathf.Lerp(currentVelocity, maxVelocity, Time.deltaTime);
        velocity = currentVelocity + acceleration * Time.deltaTime;
        return velocity;
    }
    public static float ClosestMultiple(float value, float roundTo)
    {
        if (roundTo == 0)
            return value;
        float remainder = value % roundTo;
        if (remainder < 0)
            remainder += roundTo;
        if (remainder < roundTo / 2)
            return value - remainder;
        else
            return value + (roundTo - remainder);
    }
    public static Vector3 GetAnchorPoint(Vector3 startPoint, Vector3 endPoint, Vehicles vehicle, bool leftTurn = true)
    {
        Vector3 midpoint = (startPoint + endPoint) * 0.5f;
        // Calculate the distance between points
        float chordLength = Vector3.Distance(startPoint, endPoint);
        float radius = chordLength / 1.414f;

        // Check if radius is large enough to create an arc
        float minRadius = chordLength * 0.5f;
        if (radius < minRadius)
        {
            Debug.LogWarning($"Radius {radius} too small for chord length {chordLength}. Using minimum radius {minRadius}");
            radius = minRadius;
        }

        // Calculate the distance from midpoint to circle center
        float distanceToCenter = Mathf.Sqrt(radius * radius - (chordLength * 0.5f) * (chordLength * 0.5f));

        // Get the direction perpendicular to the chord
        Vector3 chordDirection = (endPoint - startPoint).normalized;
        Vector3 perpendicular = Vector3.Cross(chordDirection, Vector3.up).normalized;

        // Choose side based on turn direction
        if (!leftTurn) perpendicular = -perpendicular;

        // Calculate anchor point
        Vector3 anchorPoint = midpoint + perpendicular * distanceToCenter;

        return anchorPoint;
    }
    public static Vector3 CalculateSimpleIntersectionAnchor(Vector3 startPoint, Vector3 endPoint,
                                                           IntersectionDirection direction, float offset = 5f)
    {
        Vector3 midpoint = (startPoint + endPoint) * 0.5f;
        Vector3 toEnd = (endPoint - startPoint).normalized;

        Vector3 offsetDirection = Vector3.zero;

        switch (direction)
        {
            case IntersectionDirection.Left:
                offsetDirection = Vector3.Cross(toEnd, Vector3.up).normalized;
                break;
            case IntersectionDirection.Right:
                offsetDirection = -Vector3.Cross(toEnd, Vector3.up).normalized;
                break;
            case IntersectionDirection.Forward:
                return midpoint; // No offset needed for straight
        }

        return midpoint + offsetDirection * offset;
    }
}
