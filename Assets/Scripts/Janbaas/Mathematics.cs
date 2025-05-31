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
    public static float RotationCalculator(float currentRotation, float targetRotation, float rotationRatio)
    {
        float rotation = Mathf.LerpAngle(currentRotation, targetRotation, rotationRatio * Time.deltaTime);
        return rotation;
    }
}
