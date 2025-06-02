using System.Net;
using Unity.VisualScripting;
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
    bool _turningFinished;
    private float _finalAngle;
    private float currentVel;
    private WayPoint currentWayPoint;
    private Transform nextWayPoint;
    private float _accel;
    private float _rotatedSoFar;
    private Vector3 _anchorPoint;
    LayerMask _inetsectionMask;
    public Intersecting(CarStateMachine stateMachine,Vehicles vehicle,float velocity,Rigidbody rb, float rayDistFront,float _accel,LayerMask _inetsectionMask)
    {
        _stateMachine = stateMachine;
        _vehicle = vehicle;
        _velocity = velocity;
        _rb = rb;
        _rayDistFront = rayDistFront;
        this._accel = _accel;
        this._inetsectionMask = _inetsectionMask;
    }
    public override void EnterState()
    {
        // Find the closest waypoint and determine the next waypoint
        currentWayPoint = _vehicle.currentWayPoint;
        if (currentWayPoint == null)
        {
            _stateMachine.ChangeState(_stateMachine.parkState);
            return;
        }

        // Determine the direction and next waypoint
        Intersection intersection = _vehicle.intersection;
        IntersectionDirection direction = intersection.GetDirection();
        switch (direction)
        {
            case IntersectionDirection.Left:
                nextWayPoint = currentWayPoint.Leftpoint;
                _finalAngle = -90f;
                break;
            case IntersectionDirection.Right:
                nextWayPoint = currentWayPoint.Rightpoint;
                _finalAngle = 90f;
                break;
            case IntersectionDirection.Forward:
                nextWayPoint = currentWayPoint.Nextpoint;
                _finalAngle = 0f;
                break;
        }

        if (nextWayPoint == null)
        {
            _stateMachine.ChangeState(_stateMachine.parkState);
            return;
        }

        // Calculate the center of the arc (_anchorPoint)
        Vector3 startToNext = (nextWayPoint.position - currentWayPoint.transform.position).normalized;
        Vector3 perpendicular = Vector3.Cross(startToNext, Vector3.up).normalized;
        float radius = Vector3.Distance(currentWayPoint.transform.position, nextWayPoint.position) / Mathf.Sqrt(2);
        _anchorPoint = currentWayPoint.transform.position + perpendicular * radius;

        // Initialize rotation and movement variables
        _rotatedSoFar = 0f;
        _rotRatio = _velocity / radius; // Adjust rotation speed based on radius
        _turningFinished = false;
    }

    public override void ExitState()
    {
        
    }

    public override void FixedUpdate()
    {
        if (_turningFinished)
            return;

        // Calculate the step angle for this frame
        float rotateStep = _rotRatio * Time.fixedDeltaTime * Mathf.Sign(_finalAngle);
        float remainingAngle = _finalAngle - _rotatedSoFar;

        // Clamp the rotation step to avoid overshooting
        if (Mathf.Abs(rotateStep) > Mathf.Abs(remainingAngle))
            rotateStep = remainingAngle;

        // Calculate the new position along the arc
        Vector3 startToAnchor = _vehicle.transform.position - _anchorPoint;
        Quaternion rotation = Quaternion.AngleAxis(rotateStep, Vector3.up);
        _rb.MovePosition(rotation * (_rb.transform.position - nextWayPoint.position) + nextWayPoint.position);
        _rb.MoveRotation(_rb.transform.rotation * rotation);

        // Update the rotated angle
        _rotatedSoFar += rotateStep;

        // Check if the turn is complete
        if (Mathf.Abs(_rotatedSoFar) >= Mathf.Abs(_finalAngle))
        {
            _turningFinished = true;

            // Snap to the final position and rotation
            _vehicle.transform.position = nextWayPoint.position;
            _vehicle.transform.rotation = Quaternion.LookRotation(nextWayPoint.position - _anchorPoint, Vector3.up);

            // Transition to the next state
            _stateMachine.ChangeState(_stateMachine.acceleratingState);
        }

    }
     
    public override void Update()
    {
        if (_turningFinished)
        {
            _stateMachine.ChangeState(_stateMachine.parkState);
        }
    }
    private void OnDrawGizmos()
    {
        if (currentWayPoint == null || nextWayPoint == null) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(currentWayPoint.transform.position, _anchorPoint);
        Gizmos.DrawLine(nextWayPoint.position, _anchorPoint);

        Gizmos.color = Color.green;
        Vector3 startToAnchor = currentWayPoint.transform.position - _anchorPoint;
        for (float angle = 0; angle <= Mathf.Abs(_finalAngle); angle += 1f)
        {
            Quaternion rotation = Quaternion.AngleAxis(angle * Mathf.Sign(_finalAngle), Vector3.up);
            Vector3 point = _anchorPoint + rotation * startToAnchor;
            Gizmos.DrawSphere(point, 0.1f);
        }
    }
}
