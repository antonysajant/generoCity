using UnityEngine;

public class Idle : PlayerStates
{
    private PlayerStateMachine _stateMachine;
    private PlayerInput _playerInput;
    private Animator _animator;
    int _idleHash;
    public Idle(PlayerStateMachine stateMachine, PlayerInput _playerInput, Animator animator, int idleHash)
    {
        _stateMachine = stateMachine;
        this._playerInput = _playerInput;
        _animator = animator;
        _idleHash = idleHash;
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
    }
}
