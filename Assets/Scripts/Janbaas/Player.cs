using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerInput _playerInput;
    PlayerStateMachine _playerStateMachine;
    int idle = Animator.StringToHash("Idle");
    int move = Animator.StringToHash("Walk");
    int jump = Animator.StringToHash("Jump");
    int fall = Animator.StringToHash("Fall");
    private CharacterMovement _characterMovement;
    [SerializeField] float speed = 5f;

    private void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
        _playerInput = new PlayerInput();
        _playerInput.Awake();
        _playerStateMachine = new PlayerStateMachine(_characterMovement, _playerInput, GetComponent<Animator>(),idle,move,jump,fall,speed,this);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _playerStateMachine.Update();
    }
}
