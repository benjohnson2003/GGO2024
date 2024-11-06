using UnityEngine;

public class MovementStateIdle : MovementBaseState
{
    public MovementStateIdle(MovementStateMachine currentContext, MovementStateFactory movementStateFactory) : base(currentContext, movementStateFactory)
    {

    }

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        // Damping
        Vector2 velocity = _ctx.Rigidbody.velocity;
        if (_ctx.IsGrounded())
        {
            // Ground damping
            velocity.x *= Mathf.Pow(1f - _ctx.groundDamping, Time.deltaTime * 10f);
        }
        else
        {
            // Air damping
            velocity.x *= Mathf.Pow(1f - _ctx.airDamping, Time.deltaTime * 10f);
        }

        _ctx.Rigidbody.velocity = velocity;

        CheckSwitchState();
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchState()
    {
        if (_ctx.MoveDirection != Vector2.zero)
            SwitchState(_factory.Move());
    }

    public override void InitializeSubState()
    {

    }
}
