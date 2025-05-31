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

    [Header("Rotate Around")]
    [SerializeField]private Transform rotateAroundLeft;
    [SerializeField] private Transform rotateAroundRight;
    [SerializeField] private Transform rotateAroundUTurn;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stateMachine = new CarStateMachine(this,acceleration,maxSpeed,maxBackwardSpeed,rotationRatio,rb,rayDistFront,rayDistBack);
    }

    private void Update()
    {
        stateMachine.Update();
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
