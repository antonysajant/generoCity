using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerInput _playerInput;
    PlayerStateMachine _playerStateMachine;
    int idle = Animator.StringToHash("Idle");
    int move = Animator.StringToHash("Walk");
    int jump = Animator.StringToHash("Jump");
    [SerializeField] float speed = 5f;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Awake();
        _playerStateMachine = new PlayerStateMachine(GetComponent<CharacterMovement>(), _playerInput, GetComponent<Animator>(),idle,move,jump,speed,this);
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
