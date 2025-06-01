using UnityEngine;

public class Intersecting : States
{
    private CarStateMachine _stateMachine;
    private Vehicles _vehicle;
    private float _rotRatio;
    private float _velocity;
    private Rigidbody _rb;
    private float _initAngle;
    private float _rayDistFront;
    private IntersectionDirection direction;
    bool _turningfinished;
    private float _finalAngle;
    private float currentVel;
    private float _accel;
    public Intersecting(CarStateMachine stateMachine,Vehicles vehicle,float rotRatio,float velocity,Rigidbody rb, float rayDistFront,float _accel)
    {
        _stateMachine = stateMachine;
        _vehicle = vehicle;
        _rotRatio = rotRatio;
        _velocity = velocity;
        _rb = rb;
        _rayDistFront = rayDistFront;
        this._accel = _accel;
    }
    public override void EnterState()
    {
        _turningfinished = false;
        var isHit = Physics.Raycast(_vehicle.transform.position, _vehicle.transform.forward, out RaycastHit hit,_rayDistFront);
        hit.collider.gameObject.TryGetComponent<Intersection>(out Intersection intersection);
        direction = intersection.GetDirection();
        _initAngle = 0;
        switch (direction)
        {
            case IntersectionDirection.Left:
                _finalAngle = -90f;
                break;
            case IntersectionDirection.Right:
                _finalAngle = 90f;
                break;
            case IntersectionDirection.Backward:
                _finalAngle = 180f;
                break;
            case IntersectionDirection.Forward:
                _finalAngle = 0f;
                break;
        }
        currentVel = _rb.linearVelocity.magnitude;

    }

    public override void ExitState()
    {
        
    }

    public override void FixedUpdate()
    {
        
        
    }
     
    public override void Update()
    {
        if (_turningfinished)
        {
            _stateMachine.ChangeState(_stateMachine.parkState);
        }
    }
}
