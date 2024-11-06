using UnityEngine;

public class MovementStateFall : MovementBaseState
{
    public MovementStateFall(MovementStateMachine currentContext, MovementStateFactory movementStateFactory) : base(currentContext, movementStateFactory)
    {
        _isRootState = true;
    }


    float _timeSinceJumpPressed = 0;
    bool coyoteTimeJumpRemoved = false;

    public override void EnterState()
    {
        InitializeSubState();

        // Height cut
        _ctx.Rigidbody.velocity = new Vector2(_ctx.Rigidbody.velocity.x, _ctx.Rigidbody.velocity.y * _ctx.jumpHeightCut);

        // Initialize jump variables
        _timeSinceJumpPressed = float.MaxValue;
        if (_ctx.TimeSinceGrounded == 0)
            coyoteTimeJumpRemoved = false;
        else
            coyoteTimeJumpRemoved = true;

    }

    public override void UpdateState()
    {
        // Apply gravity
        _ctx.Rigidbody.velocity += Vector2.down * 9.81f * _ctx.fallMultiplyer * Time.deltaTime;
        // Clamp max fall speed
        _ctx.Rigidbody.velocity = new Vector2(_ctx.Rigidbody.velocity.x, Mathf.Clamp(_ctx.Rigidbody.velocity.y, -_ctx.maxFallVelocity, float.MaxValue));

        // Jump input check
        if (_ctx.JumpWasPressed)
            _timeSinceJumpPressed = 0;
        else
            _timeSinceJumpPressed += Time.deltaTime;

        // Increment time since grounded, remove coyote time jump
        _ctx.TimeSinceGrounded += Time.deltaTime;
        if (_ctx.TimeSinceGrounded > _ctx.coyoteTime && !coyoteTimeJumpRemoved && _ctx.Jumps > 0)
        {
            _ctx.Jumps--;
            coyoteTimeJumpRemoved = true;
        }

        CheckSwitchState();
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchState()
    {
        // Enter jump, CHECK TIME SINCE JUMP PRESSED
        if (_timeSinceJumpPressed <= _ctx.jumpCheckTime && _ctx.IsGrounded())
        {
            // Called if player presses jump just too soon (within _ctx.jumpCheckTime)
            // Switches to grounded state to reset jump logic
            // Then, instantly switches to jump state
            SwitchState(_factory.Grounded());
            SwitchState(_factory.Jump());
            return;
        }
        // Enter jump, MID AIR JUMP
        if (_ctx.JumpWasPressed && _ctx.Jumps > 0 && !_ctx.IsGrounded())
        {
            // Mid air jump, does not ground the player
            SwitchState(_factory.Jump());
            return;
        }
        // Enter grounded?
        if (_ctx.IsGrounded())
        {
            SwitchState(_factory.Grounded());
            return;
        }
    }

    public override void InitializeSubState()
    {
        if (_ctx.MoveDirection != Vector2.zero)
        {
            SetSubState(_factory.Move());
            return;
        }
        SetSubState(_factory.Idle());
    }
}