using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private float JumpForce = 10f;
    private Vector3 _moveDirection;
    [SerializeField]private float gravity = -9.81f;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    public void Move(Vector2 moveVector,float speed,bool Jump)
    {
        _moveDirection = moveVector.x * transform.right + moveVector.y * transform.forward ;
        
        if (!_controller.isGrounded)
        {
            _moveDirection.y += gravity * Time.deltaTime;
        }
        else
        {
            _moveDirection.y = 0f;
        }
        if (Jump)
        {
            Jump = false;
            _moveDirection.y = JumpForce;
        }
        _controller.Move(_moveDirection * speed * Time.deltaTime);
    }
    public bool IsGrounded()
    {
        return _controller.isGrounded;
    }
}
