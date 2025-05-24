using UnityEngine;

public class PlayerStateMachine
{
    public PlayerStates CurrentState { get; private set; }

    public Idle idleState;
    public Moving movingState;

    public PlayerStateMachine(CharacterController controller,PlayerInput input,Animator animator,int idle,int move)
    {
        idleState = new Idle(this, input,animator,idle);
        movingState = new Moving(this, controller,input,animator,move);
        Initialize();
    }

    private void Initialize()
    {
        CurrentState = idleState;
        CurrentState.EnterState();
    }
    public void Update()
    {
        CurrentState.Update();
    }
    public void SwitchState(PlayerStates newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
}
