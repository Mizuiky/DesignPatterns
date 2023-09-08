using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void OnStateEnter(params object [] obj);

    public void OnStateUpdate();

    public void OnStateExit();   
}
