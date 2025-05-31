using System;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]private float JumpForce = 10f;
    private Vector3 _moveDirection;
    [SerializeField] private float gravity = -9.81f;

    [Header("Ground Check")]
    [SerializeField]
    [Range(0f,3f)]float Raydistance = 0f;
    [SerializeField] LayerMask ground;
    [SerializeField] float smoothRot = 0.1f;
    [SerializeField] Transform targetLock;
    float prevVelocity = 0f;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    public void Move(Vector2 moveVector,float speed,bool Jump)
    {
        float targetAngle = Mathf.Atan2(moveVector.x, moveVector.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothRot, 0.1f);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        int canMove = moveVector.magnitude > 0.1f ? 1 : 0;
        _moveDirection = transform.forward*canMove;
        _moveDirection *= speed;
        _moveDirection.y = prevVelocity;
        

        if (!IsGrounded())
        {
            _moveDirection.y += gravity * Time.deltaTime;
        }
        else
        {
            _moveDirection.y = 0f;
        }
        if (Jump)
        {
            _moveDirection.y = JumpForce;
        }
        prevVelocity = _moveDirection.y;
        _controller.Move(_moveDirection* Time.deltaTime);
    }
    public bool IsGrounded()
    {
        return Physics.Raycast(targetLock.position, Vector3.down, Raydistance, ground);
    }
    public Vector3 GetVelocity()
    {
        return _controller.velocity;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(targetLock.position, targetLock.position + Vector3.down * Raydistance);
    }
}
