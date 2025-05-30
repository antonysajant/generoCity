using UnityEngine;

public class Jump : PlayerStates
{
    private PlayerStateMachine _stateMachine;
    private CharacterMovement _movement;
    private PlayerInput _playerInput;
    private Animator _animator;
    private int _jumpHash;
    private float _jumpForce;
    private Player _player;
    private float _speed;
    public Jump(PlayerStateMachine stateMachine, CharacterMovement movement, PlayerInput playerInput, Animator animator, int jumpHash, float jumpForce, Player player,float speed)
    {
        _stateMachine = stateMachine;
        _movement = movement;
        _playerInput = playerInput;
        _animator = animator;
        _jumpHash = jumpHash;
        _jumpForce = jumpForce;
        _player = player;
        _speed = speed;
    }

    public override void EnterState()
    {
        Debug.Log("Entering JumpState");
        _movement.Move(_playerInput.MoveInput, _speed, true);
    }

    public override void ExitState()
    {
        Debug.Log("Exiting JumpState");
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        if (_movement.IsGrounded())
        {
            _stateMachine.SwitchState(_stateMachine.idleState);
        }
        _movement.Move(_playerInput.MoveInput, _speed, false);
    }
}
