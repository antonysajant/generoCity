using UnityEngine;
using UnityEngine.AI;

public class Vehicles : MonoBehaviour
{
    CarStateMachine stateMachine;
    [SerializeField]float acceleration = 10f;
    [SerializeField] float maxSpeed = 20f;
    [SerializeField] float rotationRatio = 0.1f;
    [SerializeField] float rayDistFront = 5f;
    [SerializeField] float maxBackwardSpeed = 5f;
    [SerializeField] float rayDistBack = 5f;
    private Rigidbody rb;
    [SerializeField] private bool leftLane = true;
    public Intersection intersection;
    public WayPoint currentWayPoint;

    public bool LeftLane
    {
        get => leftLane;
        set => leftLane = value;
    }
    public BoxCollider col { get; private set; }

    [Header("Rotate Around")]
    [SerializeField]private Transform rotateAroundLeft;
    [SerializeField] private Transform rotateAroundRight;
    [SerializeField] private Transform rotateAroundUTurn;
    [SerializeField] private LayerMask intersectionLayers;
    [SerializeField] private States currentStates;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stateMachine = new CarStateMachine(this,acceleration,maxSpeed,maxBackwardSpeed,rotationRatio,rb,rayDistFront,rayDistBack,intersectionLayers);
        Vector3 tpLoc = WayPointManager.instance.FindClosestWayPoint(transform.position,this).transform.position;
        transform.position = new Vector3(tpLoc.x,0,tpLoc.z);
        currentWayPoint = WayPointManager.instance.FindClosestWayPoint(transform.position, this);
        col = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        stateMachine.Update();
        currentStates = stateMachine.currentState;
    }
    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistFront);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, -transform.forward * rayDistBack);
    }
}
