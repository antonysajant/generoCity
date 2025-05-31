using UnityEngine;

public class Park : States
{
    private Vehicles vehicle;
    private Rigidbody rb;
    private float rayDistFront;
    private CarStateMachine carStateMachine;
    public Park(CarStateMachine carStateMachine,Vehicles vehicle, Rigidbody rb,float rayDistFront)
    {
        this.vehicle = vehicle;
        this.rb = rb;
        this.rayDistFront = rayDistFront;
        this.carStateMachine = carStateMachine;
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
        var hit = Physics.Raycast(vehicle.transform.position, vehicle.transform.forward, rayDistFront);
        if (!hit)
        {
            carStateMachine.ChangeState(carStateMachine.acceleratingState);
            return;
        }
        rb.linearVelocity = Vector3.zero;
    }
}
