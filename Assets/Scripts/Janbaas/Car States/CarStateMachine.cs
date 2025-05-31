using Unity.Hierarchy;
using UnityEngine;

public class CarStateMachine
{
    public States currentState { get; private set; }
    public States acceleratingState { get; private set; }
    public States reverseState { get; private set; }
    public States intersectingState { get; private set; }

    public States parkState { get; private set; }

    public CarStateMachine(Vehicles vehicle, float accel, float maxSpeed, float maxReverseSpeed, float rotRatio, Rigidbody rb, float rayDistfront, float rayDistBack,LayerMask intersectionMask)
    {
        acceleratingState = new Accelerating(this,vehicle, accel, maxSpeed, rotRatio, rb,rayDistfront,intersectionMask);
        reverseState = new Reverse(vehicle, maxReverseSpeed, rotRatio, rb);
        parkState = new Park(this,vehicle, rb,rayDistfront);
        intersectingState = new Intersecting(this, vehicle, rotRatio,maxSpeed,rb,rayDistfront,accel);
        Initialize();
    }
    private void Initialize()
    {
        currentState = parkState;
        currentState.EnterState();
    }
    public void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }
    public void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.FixedUpdate();
        }
    }
    public void ChangeState(States newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }
        currentState = newState;
        currentState.EnterState();
    }
}