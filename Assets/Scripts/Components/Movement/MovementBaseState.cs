public abstract class MovementBaseState
{
    protected bool _isRootState = false;
    protected MovementStateMachine _ctx;
    protected MovementStateFactory _factory;
    protected MovementBaseState _currentSubState;
    protected MovementBaseState _currentSuperState;
    public MovementBaseState(MovementStateMachine currentContext, MovementStateFactory movementStateFactory)
    {
        _ctx = currentContext;
        _factory = movementStateFactory;
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchState();

    public abstract void InitializeSubState();

    public void UpdateStates()
    {
        UpdateState();
        if (_currentSubState != null)
            _currentSubState.UpdateStates();
    }

    protected void SwitchState(MovementBaseState newState)
    {
        ExitState();
        newState.EnterState();
        if (_isRootState)
            _ctx.CurrentState = newState;
        else
            _currentSuperState.SetSubState(newState);
    }

    protected void SetSuperState(MovementBaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(MovementBaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
