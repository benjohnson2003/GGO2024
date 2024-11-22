using benjohnson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : Singleton<TurnManager>
{
    // States
    public Dictionary<string, TurnBaseState> states;
    public TurnBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    TurnBaseState _currentState;

    protected override void Awake()
    {
        base.Awake();

        InitializeStates();
        _currentState = states["Idle"];
    }

    void Start()
    {
        _currentState.EnterState();
    }

    void Update()
    {
        if (_currentState != null)
            _currentState.UpdateState();
    }

    void InitializeStates()
    {
        states = new Dictionary<string, TurnBaseState>();

        states.Add("Idle", new TurnStateIdle(this));
        states.Add("Player", new TurnStatePlayer(this));
        states.Add("Enemy", new TurnStateEnemy(this));
    }
}