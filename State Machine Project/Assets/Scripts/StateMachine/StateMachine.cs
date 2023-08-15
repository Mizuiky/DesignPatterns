using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State _currenState;

    public Dictionary<System.Enum, State> stateMachine;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        stateMachine = new Dictionary<System.Enum, State>();
    }

    public void Update()
    {
        if (_currenState != null)
            _currenState.OnStateUpdate();
    }

    public void RegisterState(System.Enum stateName, State state)
    {
        stateMachine.Add(stateName, state);
    }

    public void ChangeState(System.Enum state)
    {
        if (_currenState != null)
            _currenState.OnStateExit();
              
        _currenState = stateMachine[state];
    
        _currenState.OnStateEnter();
    }
}
