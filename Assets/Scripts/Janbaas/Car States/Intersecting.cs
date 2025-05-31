using UnityEngine;

public class Intersecting : States
{
    private CarStateMachine _stateMachine;
    private Vehicles _vehicle;
    private float _rotRatio;
    private float _velocity;
    private Rigidbody _rb;
    private float initAngle;
    private IntersectionType intersectionType;

    public Intersecting(CarStateMachine stateMachine,Vehicles vehicle,float rotRatio,float velocity,Rigidbody rb)
    {
        _stateMachine = stateMachine;
        _vehicle = vehicle;
        _rotRatio = rotRatio;
        _velocity = velocity;
        _rb = rb;
    }
    public override void EnterState()
    {
        initAngle = _vehicle.transform.eulerAngles.y;
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
