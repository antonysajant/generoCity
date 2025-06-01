using UnityEngine;

public class Moving : States
{
    private PlayerStateMachine _stateMachine;
    private CharacterMovement _movement;
    private PlayerInput _playerInput;
    private Animator _animator;
    private int _moveHash;
    private float _speed;
    Player _player;
    public Moving(PlayerStateMachine stateMachine,CharacterMovement movement, PlayerInput playerInput, Animator animator, int moveHash,float speed,Player player)
    {
        _stateMachine = stateMachine;
        _movement = movement;
        _playerInput = playerInput;
        _animator = animator;
        _moveHash = moveHash;
        _speed = speed;
        _player = player; 
    }
    public override void EnterState()
    {
        _animator.CrossFade(_moveHash, 0.2f);
    }

    public override void ExitState()
    {
       
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        if(_playerInput.MoveInput == Vector2.zero)
        {
            _stateMachine.SwitchState(_stateMachine.idleState);
            return;
        }
        if (_playerInput.JumpInput && _movement.IsGrounded())
        {
            _stateMachine.SwitchState(_stateMachine.jumpingState);
            return;
        }
        if (!_movement.IsGrounded())
        {
            _stateMachine.SwitchState(_stateMachine.fallingState);
            return;
        }
        _movement.Move(_playerInput.MoveInput, _speed, false);
    }
}
