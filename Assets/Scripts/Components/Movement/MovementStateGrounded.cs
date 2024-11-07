using System.Collections;
using UnityEngine;

public class MovementStateGrounded : MovementBaseState
{
    public MovementStateGrounded(MovementStateMachine currentContext, MovementStateFactory movementStateFactory) : base(currentContext, movementStateFactory)
    {
        _isRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();

        _ctx.Jumps = _ctx.maxJumps;
        _ctx.TimeSinceGrounded = 0;

        _ctx.PlayerAnimationController.Particles(1);
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchState()
    {
        // Enter jump?
        if (_ctx.JumpWasPressed)
            SwitchState(_factory.Jump());
        // Enter fall?
        if (!_ctx.IsGrounded())
            SwitchState(_factory.Fall());
    }

    public override void InitializeSubState()
    {
        if (_ctx.MoveDirection.x != 0)
            SetSubState(_factory.Move());
        else
            SetSubState(_factory.Idle());
    }
}