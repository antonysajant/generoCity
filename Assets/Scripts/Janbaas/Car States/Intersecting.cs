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
    private Transform _anchorPoint;
    private float _startAngle;
    private float _rotatedSoFar;
    private LayerMask _intersectionMask;
    public Intersecting(CarStateMachine stateMachine,Vehicles vehicle,float velocity,Rigidbody rb, float rayDistFront,float _accel,LayerMask _intersectionMask)
    {
        _stateMachine = stateMachine;
        _vehicle = vehicle;
        _velocity = velocity;
        _rb = rb;
        _rayDistFront = rayDistFront;
        this._accel = _accel;
        this._intersectionMask = _intersectionMask;
    }
    public override void EnterState()
    {
        _turningfinished = false;
        var isHit = Physics.Raycast(_vehicle.transform.position, _vehicle.transform.forward, out RaycastHit hit,_rayDistFront);
        hit.collider.gameObject.TryGetComponent<Intersection>(out Intersection intersection);
        direction = intersection.GetDirection();
        Debug.Log(direction);
        switch (direction)
        {
            case IntersectionDirection.Left:
                _finalAngle = -90f;
                _anchorPoint = _vehicle.RotateAroundLeft;
                break;
            case IntersectionDirection.Right:
                _finalAngle = 90f;
                _anchorPoint = _vehicle.RotateAroundRight;
                break;
            case IntersectionDirection.Backward:
                _finalAngle = 180f;
                _anchorPoint = _vehicle.RotateAroundUTurn;
                break;
            case IntersectionDirection.Forward:
                _finalAngle = 0f;
                break;
        }
        float d = Vector3.Distance(_anchorPoint.position, _rb.transform.position);
        _rotRatio = _velocity * d;
        if(direction == IntersectionDirection.Backward) _rotRatio *= 2f;
        currentVel = _rb.linearVelocity.magnitude;
        _rb.linearVelocity = Vector3.zero;
        _startAngle = _vehicle.transform.eulerAngles.y;
        _rotatedSoFar = 0f;
    }

    public override void ExitState()
    {
        
    }

    public override void FixedUpdate()
    {
        if (_turningfinished)
        {
            return;
        }
        if(direction == IntersectionDirection.Forward)
        {
            _rb.linearVelocity = _vehicle.transform.forward * currentVel;
            if(!Physics.Raycast(_vehicle.transform.position,_vehicle.transform.forward,_rayDistFront,_intersectionMask))
                _turningfinished = true;
            return;
        }

        float rotateStep = _rotRatio * Time.fixedDeltaTime * Mathf.Sign(_finalAngle);
        float remaining = _finalAngle - _rotatedSoFar;
        Debug.Log(remaining + " "+rotateStep);
        if (Mathf.Abs(rotateStep) > Mathf.Abs(remaining))
            rotateStep = remaining;

        // Apply the incremental rotation
        Quaternion q = Quaternion.AngleAxis(rotateStep, _anchorPoint.up);
        _rb.MovePosition(q * (_rb.transform.position - _anchorPoint.position) + _anchorPoint.position);
        _rb.MoveRotation(q * _rb.transform.rotation);

        _rotatedSoFar += rotateStep;
        if (Mathf.Abs(_rotatedSoFar) >= Mathf.Abs(_finalAngle) - 0.5f) // 0.5f is a small threshold
        {
            Debug.Log("Turning finished");
            _turningfinished = true;
        }
    }
     
    public override void Update()
    {
        if (_turningfinished)
        {
            _stateMachine.ChangeState(_stateMachine.acceleratingState);
        }
        float currentDelta = Mathf.DeltaAngle(_vehicle.transform.eulerAngles.y, _startAngle + _finalAngle);
        if (Mathf.Abs(currentDelta) < 2f)
        {
            Debug.Log("Turning finished");
            _turningfinished = true;
            return;
        }
    }
}
