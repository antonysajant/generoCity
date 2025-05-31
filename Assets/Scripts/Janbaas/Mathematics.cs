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
}
