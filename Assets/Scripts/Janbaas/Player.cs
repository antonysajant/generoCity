using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerInput _playerInput;
    PlayerStateMachine _playerStateMachine;
    int idle = Animator.StringToHash("Idle");
    int move = Animator.StringToHash("Walk");

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Awake();
        _playerStateMachine = new PlayerStateMachine(GetComponent<CharacterController>(), _playerInput, GetComponent<Animator>(),idle,move);
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
