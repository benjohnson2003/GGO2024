using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    public Vector2 Movement { get; private set; }
    public bool JumpWasPressed { get; private set; }
    public bool JumpIsPressed { get; private set; }
    public bool KickWasPressed { get; private set; }
    public bool KickIsPressed { get; private set; }

    PlayerInput playerInput;

    InputAction _movement;
    InputAction _jump;
    InputAction _kick;

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
        _jump = playerInput.actions["Jump"];
        _kick = playerInput.actions["Kick"];
    }

    void UpdateInputs()
    {
        Movement = _movement.ReadValue<Vector2>();
        JumpWasPressed = _jump.WasPressedThisFrame();
        JumpIsPressed = _jump.IsPressed();
        KickWasPressed = _kick.WasPressedThisFrame();
        KickIsPressed = _kick.IsPressed();
    }
}
