using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateBase : State
{
    protected Enemy enemy;

    public override void OnStateEnter(params object[] obj) 
    {
        enemy = (Enemy)obj[0];
    }

    public override void OnStateUpdate() { }

    public override void OnStateExit() { }
}
