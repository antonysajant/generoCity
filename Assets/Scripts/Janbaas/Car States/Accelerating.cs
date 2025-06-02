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
    private LayerMask _intersectionMask;
    private Transform nextWayPoint;
    private WayPoint currentWayPoint;

    public Accelerating(CarStateMachine stateMachine,Vehicles vehicle, float accel, float maxSpeed, float rotRatio, Rigidbody rb,float rayDistFront,LayerMask _intersectionMask)
    {
        this.vehicle = vehicle;
        this.accel = accel;
        this.maxSpeed = maxSpeed;
        this.rotRatio = rotRatio;
        this.rb = rb;
        this.rayDistFront = rayDistFront;
        this.stateMachine = stateMachine;
        this._intersectionMask = _intersectionMask;
    }

    public override void EnterState()
    {
       velocity = 0f;
        currentWayPoint = vehicle.currentWayPoint;
       nextWayPoint = currentWayPoint.Nextpoint;
    }

    public override void ExitState()
    {
        
    }

    public override void FixedUpdate()
    {
        Vector3 direction = nextWayPoint.position - currentWayPoint.transform.position;
        direction = direction.normalized;
        velocity = Mathematics.VelocityCalculator(velocity,maxSpeed,accel);
        rb.linearVelocity = direction * velocity;
    }

    public override void Update()
    {
        var hit = Physics.BoxCast(vehicle.transform.position,vehicle.col.bounds.size/2,vehicle.transform.forward,out RaycastHit rayHit,Quaternion.identity,rayDistFront,_intersectionMask);
        

        if (hit)
        {
            if (rayHit.collider.CompareTag("Intersection"))
            {
                vehicle.intersection = rayHit.collider.GetComponent<Intersection>();
                stateMachine.ChangeState(stateMachine.intersectingState);
                return;
            }
            stateMachine.ChangeState(stateMachine.parkState);
            return;
        }
        if ((nextWayPoint.position - vehicle.transform.position).magnitude < 0.2f)
        {
            currentWayPoint = WayPointManager.instance.GetWayPoint(nextWayPoint,vehicle);
            nextWayPoint = currentWayPoint.Nextpoint;
        }

    }
}
