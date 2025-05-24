using UnityEngine;

public class Moving : PlayerStates
{
    private PlayerStateMachine _stateMachine;
    private CharacterController _controller;
    private PlayerInput _playerInput;
    private Animator _animator;
    private int _moveHash;
    public Moving(PlayerStateMachine stateMachine,CharacterController controller, PlayerInput playerInput, Animator animator, int moveHash)
    {
        _stateMachine = stateMachine;
        _controller = controller;
        _playerInput = playerInput;
        _animator = animator;
        _moveHash = moveHash;
    }
    public override void EnterState()
    {
        Debug.Log("Entering MoveState");
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
    }
}
