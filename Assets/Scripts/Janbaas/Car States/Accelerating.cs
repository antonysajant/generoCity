using UnityEngine;

public class Accelerating : States
{
    private CarStateMachine stateMachine;
    private Vehicles vehicle;
    private float accel;
    private float maxSpeed;
    private float rotRatio;
    private Rigidbody rb;
    private float velocity;
    private float rayDistFront;

    public Accelerating(CarStateMachine stateMachine,Vehicles vehicle, float accel, float maxSpeed, float rotRatio, Rigidbody rb,float rayDistFront)
    {
        this.vehicle = vehicle;
        this.accel = accel;
        this.maxSpeed = maxSpeed;
        this.rotRatio = rotRatio;
        this.rb = rb;
        this.rayDistFront = rayDistFront;
        this.stateMachine = stateMachine;
    }

    public override void EnterState()
    {
       velocity = 0f;   
    }

    public override void ExitState()
    {
        
    }

    public override void FixedUpdate()
    {
        velocity = Mathematics.VelocityCalculator(velocity,maxSpeed,accel);
        rb.linearVelocity = vehicle.transform.forward * velocity;
    }

    public override void Update()
    {
        var hit = Physics.Raycast(vehicle.transform.position, vehicle.transform.forward,out RaycastHit rayHit ,rayDistFront);
        if(rayHit.collider.gameObject.TryGetComponent<Intersection>(out Intersection inter))
        {
            stateMachine.ChangeState(stateMachine.intersectingState);
            return;
        }
        if (hit)
        {
            stateMachine.ChangeState(stateMachine.parkState);
            return;
        }
    }
}
