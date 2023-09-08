using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : IState
{
    public  virtual void OnStateEnter(params object[] obj) { }

    public  virtual void OnStateUpdate() { }

    public virtual void OnStateExit() { }
}
