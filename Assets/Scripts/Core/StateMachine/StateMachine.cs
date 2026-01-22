using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Core.StateMachine
{
public class StateMachine<T> where T : System.Enum 
{
    //key
    public Dictionary<T, StateBase> dictionaryState;
    
    private StateBase _currentState;

    public StateBase CurrentState
    {
        get {return _currentState;}
    }
    
    public void Init()
    {
        dictionaryState = new Dictionary<T, StateBase>();
    }
    
    public void RegisterStates(T typeEnum, StateBase state)
    {
        dictionaryState.Add(typeEnum, state);
    }
    
    public void SwitchState(T state)
    {
        if (_currentState != null) _currentState.OnStateExit();
        
        _currentState = dictionaryState[state];
        
        _currentState.OnStateEnter();
    }

    public void Update()
    {
        if (_currentState != null) _currentState.OnStateStay();
    }
}
}