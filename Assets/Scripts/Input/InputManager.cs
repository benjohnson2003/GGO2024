using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    public Vector2 Movement { get; private set; }
    public bool Action1 { get; private set; }
    public bool Action2 { get; private set; }
    public bool Action3 { get; private set; }

    PlayerInput playerInput;

    InputAction _movement;
    InputAction _action1;
    InputAction _action2;
    InputAction _action3;

    protected override void Awake()
    {
        base.Awake();

        playerInput = GetComponent<PlayerInput>();
        SetupInputActions();
    }

    void Update()
    {
        UpdateInputs();
    }

    void SetupInputActions()
    {
        _movement = playerInput.actions["Movement"];
        _action1 = playerInput.actions["Action 1"];
        _action2 = playerInput.actions["Action 2"];
        _action3 = playerInput.actions["Action 3"];
    }

    void UpdateInputs()
    {
        Movement = _movement.ReadValue<Vector2>();
        Action1 = _action1.WasPressedThisFrame();
        Action2 = _action2.WasPressedThisFrame();
        Action3 = _action3.WasPressedThisFrame();
    }
}
