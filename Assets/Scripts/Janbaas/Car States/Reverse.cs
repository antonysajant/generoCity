using UnityEngine;

public class Reverse : States
{
    private Vehicles vehicle;
    private float maxReverseSpeed;
    private float rotRatio;
    private Rigidbody rb;

    public Reverse(Vehicles vehicle, float maxReverseSpeed, float rotRatio, Rigidbody rb)
    {
        this.vehicle = vehicle;
        this.maxReverseSpeed = maxReverseSpeed;
        this.rotRatio = rotRatio;
        this.rb = rb;
    }

    public override void EnterState()
    {
       
    }

    public override void ExitState()
    {
        
    }

    public override void FixedUpdate()
    {
       
    }

    public override void Update()
    {
       
    }
}
