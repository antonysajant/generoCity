using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public static class Mathematics
{
    public static float VelocityCalculator(float currentVelocity,float maxVelocity,float acceleration)
    {
        float velocity = Mathf.Lerp(currentVelocity,maxVelocity,Time.deltaTime);
        velocity = currentVelocity + acceleration * Time.deltaTime;
        return velocity;
    } 
<<<<<<< HEAD
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
=======
    public static float RotationCalculator(float currentRotation, float targetRotation, float rotationRatio)
    {
        float rotation = Mathf.LerpAngle(currentRotation, targetRotation, rotationRatio * Time.deltaTime);
        return rotation;
>>>>>>> parent of 79bc7a7 (Very close to haveing the car ai)
    }
}
