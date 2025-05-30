using UnityEngine;

public class Idle : PlayerStates
{
    private PlayerStateMachine _stateMachine;
    private PlayerInput _playerInput;
    private Animator _animator;
    int _idleHash;
    private Player _player;
    public Idle(PlayerStateMachine stateMachine, PlayerInput _playerInput, Animator animator, int idleHash,Player player)
    {
        _stateMachine = stateMachine;
        this._playerInput = _playerInput;
        _animator = animator;
        _idleHash = idleHash;
        _player = player;
    }
    public override void EnterState()
    {
        Debug.Log("Entering Idle");
        _animator.CrossFade(_idleHash, 0.2f);
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Idle");
    }

    public override void FixedUpdate()
    {
       
    }

    public override void Update()
    {
        if (_playerInput.MoveInput != Vector2.zero)
        {
            _stateMachine.SwitchState(_stateMachine.movingState);
            return;
        }
        if (_playerInput.JumpInput)
        {
            _stateMachine.SwitchState(_stateMachine.jumpingState);
            return;
        }
    }
}
