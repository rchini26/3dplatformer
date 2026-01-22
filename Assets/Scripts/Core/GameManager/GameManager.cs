using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using Core.StateMachine;
using UnityEditorInternal;

public class GameManager : Singleton<GameManager>
{
    public enum GameStates
    {
        Intro,
        Gameplay,
        Pause,
        Win,
        Lose
    }
    
    public StateMachine<GameStates> stateMachine;

    void Start()
    {
        Init();
    }

    void Init()
    {
        stateMachine = new StateMachine<GameStates>();
        stateMachine.Init();
        stateMachine.RegisterStates(GameStates.Intro, new GMStateIntro());
        stateMachine.RegisterStates(GameStates.Gameplay, new StateBase());
        stateMachine.RegisterStates(GameStates.Pause, new StateBase());
        stateMachine.RegisterStates(GameStates.Win, new StateBase());
        stateMachine.RegisterStates(GameStates.Lose, new StateBase());
        
        stateMachine.SwitchState(GameStates.Intro);
    }

    public void InitGame()
    {
        
    }
}
