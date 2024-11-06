using System.Collections.Generic;

public class MovementStateFactory
{
    MovementStateMachine _context;
    Dictionary<string, MovementBaseState> _states = new Dictionary<string, MovementBaseState>();

    public MovementStateFactory(MovementStateMachine currentContext)
    {
        _context = currentContext;
        _states["grounded"] = new MovementStateGrounded(_context, this);
        _states["jump"] = new MovementStateJump(_context, this);
        _states["fall"] = new MovementStateFall(_context, this);
        _states["idle"] = new MovementStateIdle(_context, this);
        _states["move"] = new MovementStateMove(_context, this);
    }

    public MovementBaseState Grounded()
    {
        return _states["grounded"];
    }
    public MovementBaseState Jump()
    {
        return _states["jump"];
    }
    public MovementBaseState Fall()
    {
        return _states["fall"];
    }
    public MovementBaseState Idle()
    {
        return _states["idle"];
    }
    public MovementBaseState Move()
    {
        return _states["move"];
    }
}