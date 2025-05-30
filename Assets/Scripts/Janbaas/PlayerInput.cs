using UnityEngine;

public class PlayerInput
{
    Controls _controls;
    public Vector2 MoveInput { get; private set; }
    public bool JumpInput { get; private set; }
    public void Awake()
    {
        _controls = new Controls();
        _controls.Player.Enable();
        _controls.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        _controls.Player.Move.canceled += ctx => MoveInput = Vector2.zero;
        _controls.Player.Jump.started += ctx => JumpInput = true;
        _controls.Player.Jump.canceled += ctx => JumpInput = false;
    }
    
}
