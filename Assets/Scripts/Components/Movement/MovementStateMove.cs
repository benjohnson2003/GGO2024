using UnityEngine;

public class MovementStateMove : MovementBaseState
{
    public MovementStateMove(MovementStateMachine currentContext, MovementStateFactory movementStateFactory) : base(currentContext, movementStateFactory)
    {

    }

    // Variables
    float _particleElapsed;

    public override void EnterState()
    {
        _particleElapsed = 0;
        _ctx.PlayerAnimationController.TreadsMoving(true);
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

        if (_ctx.IsGrounded() && _particleElapsed > _ctx.particleInterval)
        {
            _ctx.PlayerAnimationController.Particles(0);
            _particleElapsed = 0;
        }
        else
        {
            _particleElapsed += Time.deltaTime;
        }
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchState()
    {
        if (_ctx.MoveDirection.x == 0)
            SwitchState(_factory.Idle());
    }

    public override void InitializeSubState()
    {

    }
}
