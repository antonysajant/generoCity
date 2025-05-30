using UnityEngine;

public class PlayerStateMachine
{
    public PlayerStates CurrentState { get; private set; }

    public Idle idleState { get; private set; }
    public Moving movingState { get; private set; }
    public Jump jumpingState { get; private set; }
    public Falling fallingState { get; private set; }

    public PlayerStateMachine(CharacterMovement movement,PlayerInput input,Animator animator,int idle,int move,int jump,int fall,float speed,Player player)
    {
        idleState = new Idle(this, movement,input,animator,idle,player);
        movingState = new Moving(this, movement,input,animator,move,speed,player);
        jumpingState = new Jump(this, movement, input, animator, jump, player,speed);
        fallingState = new Falling(this, movement, input, animator, fall, player, speed);
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
