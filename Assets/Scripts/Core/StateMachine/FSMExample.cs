using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.StateMachine;

public class FSMExample : MonoBehaviour
{
    public enum ExampleEnum
    {
        StateOne, 
        StateTwo,
        StateThree
    }
    
    public StateMachine<ExampleEnum> stateMachine;

    private void Start()
    {
        stateMachine = new StateMachine<ExampleEnum>();
        stateMachine.Init();
        stateMachine.RegisterStates(ExampleEnum.StateOne, new StateBase());
        stateMachine.RegisterStates(ExampleEnum.StateTwo, new StateBase());
    }
}
