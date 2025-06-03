using System.Net;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Intersecting : States
{
    private CarStateMachine _stateMachine;
    private Vehicles _vehicle;
    private float _rotRatio;
    private float _velocity;
    private Rigidbody _rb;
    private float _rayDistFront;
    private IntersectionDirection direction;
    private bool _turningFinished;
    private float _finalAngle;
    private WayPoint currentWayPoint;
    private Transform nextWayPoint;
    private WayPoint nextWayPointComponent; // Store the WayPoint component
    private float _accel;
    private float _rotatedSoFar;
    private Vector3 _anchorPoint;
    private LayerMask _intersectionMask;
    private Vector3 _startToAnchor;
    private float _radius;
    private WayPoint _initialWayPoint;
    private bool _isInitialized = false;

    public Intersecting(CarStateMachine stateMachine, Vehicles vehicle, float velocity, Rigidbody rb, float rayDistFront, float accel, LayerMask intersectionMask)
    {
        _stateMachine = stateMachine;
        _vehicle = vehicle;
        _velocity = velocity;
        _rb = rb;
        _rayDistFront = rayDistFront;
        _accel = accel;
        _intersectionMask = intersectionMask;
    }

    public override void EnterState()
    {
        Debug.Log("Entering Intersecting State");

        // Reset state
        _turningFinished = false;
        _isInitialized = false;
        _rotatedSoFar = 0f;

        // Validate intersection exists
        if (_vehicle.intersection == null)
        {
            Debug.LogError("No intersection found on vehicle!");
            _stateMachine.ChangeState(_stateMachine.parkState);
            return;
        }

        // Find the intersection waypoint
        currentWayPoint = _vehicle.currentWayPoint;
        _initialWayPoint = currentWayPoint;

        if (currentWayPoint == null)
        {
            Debug.LogError("No current waypoint found!");
            _stateMachine.ChangeState(_stateMachine.parkState);
            return;
        }

        // Find the waypoint that has intersection options (left/right points)
        int safetyCounter = 0;
        const int maxIterations = 20; // Prevent infinite loops

        while ((currentWayPoint.Leftpoint == null && currentWayPoint.Rightpoint == null) && safetyCounter < maxIterations)
        {
            if (currentWayPoint.Nextpoint == null)
            {
                Debug.LogWarning("Reached end of waypoints without finding intersection point");
                _stateMachine.ChangeState(_stateMachine.parkState);
                return;
            }

            currentWayPoint = WayPointManager.instance.GetWayPoint(currentWayPoint.Nextpoint, _vehicle);

            if (currentWayPoint == null || currentWayPoint == _initialWayPoint)
            {
                Debug.LogWarning("No valid intersection waypoint found or returned to start");
                _stateMachine.ChangeState(_stateMachine.parkState);
                return;
            }

            safetyCounter++;
        }

        if (safetyCounter >= maxIterations)
        {
            Debug.LogError("Safety counter exceeded while finding intersection waypoint");
            _stateMachine.ChangeState(_stateMachine.parkState);
            return;
        }

        // Determine direction and set up turn parameters
        Intersection intersection = _vehicle.intersection;
        IntersectionDirection direction = intersection.GetDirection();
        Debug.Log($"Intersection direction: {direction}");

        switch (direction)
        {
            case IntersectionDirection.Left:
                nextWayPoint = currentWayPoint.Leftpoint;
                nextWayPointComponent = GetWayPointComponent(nextWayPoint);
                _anchorPoint = Mathematics.CalculateLeftTurnAnchor(_vehicle.transform.position,nextWayPoint.position,_vehicle.transform.right*-1f,_vehicle.transform.forward*-1f);
                _finalAngle = -90f;
                break;

            case IntersectionDirection.Right:
                nextWayPoint = currentWayPoint.Rightpoint;
                nextWayPointComponent = GetWayPointComponent(nextWayPoint);
                _anchorPoint = Mathematics.CalculateSimpleIntersectionAnchor(currentWayPoint.transform.position, nextWayPoint.position, direction);
                _finalAngle = 90f;
                break;

            case IntersectionDirection.Forward:
                nextWayPoint = currentWayPoint.Nextpoint;
                nextWayPointComponent = GetWayPointComponent(nextWayPoint);
                _finalAngle = 0f;
                // For straight movement, we might not need an anchor point
                break;
        }

        if (nextWayPoint == null)
        {
            Debug.LogError($"No waypoint found for direction: {direction}");
            _stateMachine.ChangeState(_stateMachine.parkState);
            return;
        }

        // Set up arc movement (skip for forward movement)
        if (direction != IntersectionDirection.Forward)
        {
            _startToAnchor = _vehicle.transform.position - _anchorPoint;
            _radius = _startToAnchor.magnitude/1.414f;

            if (_radius < 0.1f) // Prevent division by zero
            {
                Debug.LogError("Radius too small for arc movement");
                _stateMachine.ChangeState(_stateMachine.parkState);
                return;
            }

            _rotRatio = _velocity * _radius; // Angular velocity = linear velocity / radius
        }

        _isInitialized = true;
        Debug.Log($"Intersection initialized - Direction: {direction}, Final Angle: {_finalAngle}");
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Intersecting State");

        // Update vehicle's current waypoint to the next waypoint
        if (nextWayPointComponent != null)
        {
            _vehicle.currentWayPoint = nextWayPointComponent;
            Debug.Log($"Updated vehicle waypoint to: {nextWayPointComponent.name}");
        }
    }

    public override void FixedUpdate()
    {
        if (!_isInitialized || _turningFinished)
            return;
        Vector3 newPosition;
        // Handle forward movement (no turning)
        if (_finalAngle == 0f)
        {
            Vector3 direction = (nextWayPoint.position - _vehicle.transform.position).normalized;
            newPosition = _vehicle.transform.position + direction * _velocity * Time.fixedDeltaTime;
            _rb.MovePosition(newPosition);

            // Face the movement direction
            if (direction.sqrMagnitude > 0.001f)
                _rb.MoveRotation(Quaternion.LookRotation(direction, Vector3.up));

            // Check if we've reached the waypoint
            if (Vector3.Distance(_vehicle.transform.position, nextWayPoint.position) < 1f)
            {
                _turningFinished = true;
                _vehicle.transform.position = nextWayPoint.position;
            }
            return;
        }

        // Handle arc movement (left/right turns)
        float rotateStep = _rotRatio * Time.fixedDeltaTime * Mathf.Sign(_finalAngle);
        float remainingAngle = _finalAngle - _rotatedSoFar;

        // Clamp rotation step to avoid overshooting
        if (Mathf.Abs(rotateStep) > Mathf.Abs(remainingAngle))
            rotateStep = remainingAngle;

        // Calculate new position along the arc
        Quaternion totalRotation = Quaternion.AngleAxis(_rotatedSoFar + rotateStep, Vector3.up);
        newPosition = _anchorPoint + totalRotation * _startToAnchor;

        // Move the car
        _rb.MovePosition(newPosition);

        // Orient the car tangent to the arc
        Vector3 tangent = Vector3.Cross(Vector3.up, newPosition - _anchorPoint).normalized;
        if (Mathf.Sign(_finalAngle) < 0) tangent = -tangent; // Reverse for left turns

        if (tangent.sqrMagnitude > 0.001f)
            _rb.MoveRotation(Quaternion.LookRotation(tangent, Vector3.up));

        // Update rotated angle
        _rotatedSoFar += rotateStep;

        // Check if turn is complete
        if (Mathf.Abs(_rotatedSoFar) >= Mathf.Abs(_finalAngle) - 0.5f)
        {
            _turningFinished = true;

            // Snap to final position and rotation
            _vehicle.transform.position = nextWayPoint.position;

            Debug.Log("Turn completed");
        }
    }

    public override void Update()
    {
        if (_turningFinished && _isInitialized)
        {
            Debug.Log("Intersection complete, switching to accelerating state");
            _stateMachine.ChangeState(_stateMachine.acceleratingState);
        }
    }

    public override void OnDrawGizmos()
    {
        if (_isInitialized && _finalAngle != 0f)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_anchorPoint, 2f);

            // Draw the arc path
            Gizmos.color = Color.yellow;
            for (int i = 0; i <= 20; i++)
            {
                float angle = (_finalAngle / 20f) * i;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
                Vector3 arcPoint = _anchorPoint + rotation * _startToAnchor;
                Gizmos.DrawSphere(arcPoint, 0.5f);
            }
        }
    }

    // Helper method to get WayPoint component from Transform
    private WayPoint GetWayPointComponent(Transform wayPointTransform)
    {
        if (wayPointTransform == null) return null;
        return wayPointTransform.GetComponent<WayPoint>();
    }
}