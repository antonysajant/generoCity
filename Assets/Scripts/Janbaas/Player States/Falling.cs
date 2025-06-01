using UnityEngine;

public class Falling : States
{
    private PlayerStateMachine _stateMachine;
    private CharacterMovement _movement;
    private PlayerInput _playerInput;
    private Animator _animator;
    private int _fallHash;
    private Player _player;
    private float _speed;
    public Falling(PlayerStateMachine stateMachine,CharacterMovement movement, PlayerInput input,Animator animator,int fallHash,Player player,float speed)
    {
        _stateMachine = stateMachine;
        _movement = movement;
        _playerInput = input;
        _animator = animator;
        _fallHash = fallHash;
        _player = player;
        _speed = speed;
    }
    public override void EnterState()
    {


    }

    public override void ExitState()
    {
        
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        if (_movement.IsGrounded())
        {
            _stateMachine.SwitchState(_stateMachine.idleState);
            return;
        }
        _movement.Move(_playerInput.MoveInput, _speed, false);
    }
}
