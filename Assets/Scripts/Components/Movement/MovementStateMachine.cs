using System.Collections;
using UnityEngine;

public class MovementStateMachine : MonoBehaviour
{
    [Header("Settings")]
    [Header("Movement")]
    [Range(0f, 20f)] public float maxSpeed; // Maximum horizontal velocity
    [Range(0f, 500f)] public float acceleration; // Force multiplyer
    [Range(0.0f, 1.0f)] public float groundDamping; // Deceleration constant when grounded
    [Range(0.0f, 1.0f)] public float airDamping; // Deceleration constant when airborne
    [Range(0.0f, 1.0f)] public float turnAroundDamping; // Deceleration constant when turning around

    [Header("Jump")]
    public int maxJumps = 1;
    public int Jumps { get { return _jumps; } set { _jumps = value; } } // Getter and setter for current jumps remaining
    int _jumps; // Current jumps remaining
    public float jumpVelocity; // Initial jump velocity
    public float jumpDuration;
    public float jumpCheckTime = 0.08f; // Time tolerance when player presses jump too early
    public float coyoteTime = 0.06f; // Time tolerance when player presses jump too late
    public float TimeSinceGrounded { get { return _timeSinceGrounded; } set { _timeSinceGrounded = value; } }
    float _timeSinceGrounded;
    [Range(0.0f, 1.0f)] public float jumpHeightCut; // Vertical velocity multiplyer when jump released
    public float fallMultiplyer; // Gravity multiplyer
    public float maxFallVelocity; // Max negative vertical velocity

    [Header("Collision Checks")]
    public LayerMask groundMask;
    public Vector2 groundCheckPosition;
    public Vector2 groundCheckSize;
    public LayerMask ceilingMask;
    public Vector2 ceilingCheckPosition;
    public Vector2 ceilingCheckSize;
    public LayerMask platformMask;
    public float platformCheckDistance;

    // Input Variables
    public Vector2 MoveDirection { get { return _moveDirection; } set { _moveDirection = value; } }
    Vector2 _moveDirection;
    public bool JumpIsPressed { get { return _jumpIsPressed; } set { _jumpIsPressed = value; } }
    bool _jumpIsPressed;
    public bool JumpWasPressed { get { return _jumpWasPressed; } set { _jumpWasPressed = value; } }
    bool _jumpWasPressed;

    // State variables
    public MovementBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    MovementBaseState _currentState;
    MovementStateFactory _states;

    // Components
    public Rigidbody2D Rigidbody { get { return _rb; } }
    Rigidbody2D _rb;
    public Collider2D Collider { get { return _col; } }
    Collider2D _col;

    public bool IsGrounded()
    {
        return Physics2D.OverlapCapsule((Vector2)transform.position + groundCheckPosition, groundCheckSize, CapsuleDirection2D.Horizontal, 0, groundMask);
    }

    public bool IsHeadClipping()
    {
        return Physics2D.OverlapCapsule((Vector2)transform.position + ceilingCheckPosition, ceilingCheckSize, CapsuleDirection2D.Horizontal, 0, ceilingMask);
    }

    void Start()
    {
        _states = new MovementStateFactory(this);
        // Initial state
        _currentState = _states.Grounded();
        _currentState.EnterState();

        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponentInChildren<Collider2D>();
    }

    void Update()
    {
        _currentState.UpdateStates();
    }
}
