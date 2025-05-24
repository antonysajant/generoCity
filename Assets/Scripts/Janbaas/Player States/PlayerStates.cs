using UnityEngine;

public abstract class PlayerStates
{
    abstract public void EnterState();
    abstract public void Update();
    abstract public void FixedUpdate();
    abstract public void ExitState();
    
}
