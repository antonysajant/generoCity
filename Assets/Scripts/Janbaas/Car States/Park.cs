using UnityEngine;

public class Park : States
{
    private Rigidbody rb;
    private CarStateMachine carStateMachine;
    private Vehicles vehicle;
    private float rayDistFront;

    public Park(CarStateMachine carStateMachine,Vehicles vehicle,Rigidbody rb,float rayDistFront)
    {
        this.rb = rb;
        this.carStateMachine = carStateMachine;
        this.vehicle = vehicle;
        this.rayDistFront = rayDistFront;
    }

    public override void EnterState()
    {
        // Stop the car immediately
        if (rb != null)
            rb.linearVelocity = Vector3.zero;
    }

    public override void ExitState()
    {
        // No special logic needed on exit
    }

    public override void FixedUpdate()
    {
        // Keep the car stopped
        if (rb != null)
            rb.linearVelocity = Vector3.zero;
    }

    public override void Update()
    {
        var hit = Physics.Raycast(vehicle.transform.position, vehicle.transform.forward, rayDistFront);
        if (!hit)
        {
            carStateMachine.ChangeState(carStateMachine.acceleratingState);
        }

    }
}
