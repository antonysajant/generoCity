using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public static class Mathematics
{
    private static float _smoothTime = 0.1f;
    public static float VelocityCalculator(float currentVelocity,float maxVelocity,float acceleration)
    {
        float velocity = Mathf.Lerp(currentVelocity,maxVelocity,Time.deltaTime);
        velocity = currentVelocity + acceleration * Time.deltaTime;
        return velocity;
    } 
    public static float ClosestMultiple(float value,float roundTo)
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
}
