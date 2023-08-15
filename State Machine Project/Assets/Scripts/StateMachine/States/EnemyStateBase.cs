using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateBase : State
{
    protected Enemy enemy;

    public virtual void OnStateEnter(params object[] obj) 
    {
        enemy = (Enemy)obj[0];
    }

    public virtual void OnStateUpdate() { }

    public virtual void OnStateExit() { }
}
