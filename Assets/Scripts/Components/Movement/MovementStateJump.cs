using UnityEngine;

public class MovementStateJump : MovementBaseState
{
    public MovementStateJump(MovementStateMachine currentContext, MovementStateFactory movementStateFactory) : base(currentContext, movementStateFactory)
    {
        _isRootState = true;
    }

    bool _jumping;
    float _jumpElapsed;

    public override void EnterState()
    {
        InitializeSubState();

        // Decrement jumps remaining
        _ctx.Jumps--;

        // Initialize jumping variables
        _jumping = true;
        _jumpElapsed = 0;

        _ctx.PlayerAnimationController.Particles(2);
    }

    public override void UpdateState()
    {
        // End jump?
        if (!_ctx.JumpIsPressed || _jumpElapsed >= _ctx.jumpDuration || _ctx.IsHeadClipping())
            _jumping = false;

        // Jump velocity
        if (_jumping)
        {
            _ctx.Rigidbody.velocity = new Vector2(_ctx.Rigidbody.velocity.x, _ctx.jumpVelocity * Mathf.Clamp((_ctx.jumpDuration - _jumpElapsed) / _ctx.jumpDuration, 0.05f, 1));
            _jumpElapsed += Time.deltaTime;
        }

        // Increment time since grounded
        _ctx.TimeSinceGrounded += Time.deltaTime;

        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Reset jumping variables
        _jumping = false;
        _jumpElapsed = 0;
    }

    public override void CheckSwitchState()
    {
        // Enter grounded?
        if (_ctx.IsGrounded() && _ctx.Rigidbody.velocity.y <= 0)
            SwitchState(_factory.Grounded());
        // Enter fall?
        if (!_jumping)
            SwitchState(_factory.Fall());
    }

    public override void InitializeSubState()
    {
        if (_ctx.MoveDirection != Vector2.zero)
            SetSubState(_factory.Move());
        else
            SetSubState(_factory.Idle());
    }
}