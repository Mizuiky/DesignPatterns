using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : EnemyStateBase
{
    public override void OnStateEnter(params object[] obj)
    {
        base.OnStateEnter(obj);

        Debug.Log("death state enter");

        
    }

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}
