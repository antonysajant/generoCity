using UnityEngine;

public class Moving : PlayerStates
{
    private PlayerStateMachine _stateMachine;
    private CharacterController _controller;
    private PlayerInput _playerInput;
    private Animator _animator;
    private int _moveHash;
    private float speed;
    private Player _player;
    private Vector2 _moveDirection;
    public Moving(PlayerStateMachine stateMachine,CharacterController controller, PlayerInput playerInput, Animator animator, int moveHash,Player player)
    {
        _stateMachine = stateMachine;
        _controller = controller;
        _playerInput = playerInput;
        _animator = animator;
        _moveHash = moveHash;
        _player = player;
    }
    public override void EnterState()
    {
        Debug.Log("Entering MoveState");
        _animator.CrossFade(_moveHash, 0.2f);
        speed = _player.Speed;
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
        Debug.Log(_playerInput.MoveInput);
        _moveDirection = _player.transform.forward*_playerInput.MoveInput.y+_player.transform.right*_playerInput.MoveInput.x;
        _moveDirection.Normalize();
        _moveDirection *= speed * Time.deltaTime;
        _controller.Move(_moveDirection);
    }
}
