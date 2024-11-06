using UnityEngine;

public class MovementStateMove : MovementBaseState
{
    public MovementStateMove(MovementStateMachine currentContext, MovementStateFactory movementStateFactory) : base(currentContext, movementStateFactory)
    {

    }

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        // Move force
        Vector2 dir = _ctx.MoveDirection;
        if (Mathf.Abs(_ctx.Rigidbody.velocity.x) < _ctx.maxSpeed)
        {
            _ctx.Rigidbody.AddForce(new Vector2(_ctx.acceleration * dir.x * Time.fixedDeltaTime, 0f));
        }

        // Damping
        Vector2 velocity = _ctx.Rigidbody.velocity;
        if (Mathf.Abs(velocity.x) > _ctx.maxSpeed)
        {
            // Over max speed, slow down
            velocity.x *= Mathf.Pow(1f - _ctx.groundDamping, Time.deltaTime * 10f);
        }
        if (dir.x != 0 && Utilities.OppositeSigns(velocity.x, dir.x) && _ctx.IsGrounded())
        {
            // Changing direction
            velocity.x *= Mathf.Pow(1f - _ctx.turnAroundDamping, Time.deltaTime * 10f);
        }
        _ctx.Rigidbody.velocity = velocity;

        CheckSwitchState();
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchState()
    {
        if (_ctx.MoveDirection == Vector2.zero)
            SwitchState(_factory.Idle());
    }

    public override void InitializeSubState()
    {

    }
}
