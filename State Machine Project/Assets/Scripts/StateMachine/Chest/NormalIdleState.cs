using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalIdleState : EnemyStateBase
{
    private bool _lockState  = false;

    public override void OnStateEnter(params object[] obj)
    {

        base.OnStateEnter(obj);

        enemy.ChangeAnimationState(Enemy.AnimationStates.IdleNormal.ToString());

        _lockState = false;
    }

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        if (Vector3.Distance(enemy.transform.position, enemy.target.transform.position) > enemy.distanceToLook && !_lockState)
        {
            _lockState = true;

            enemy.ChangeState(Enemy.EnemyStates.IDLE);
        }
        else if(Vector3.Distance(enemy.transform.position, enemy.target.transform.position) < enemy.attackDistance && !_lockState)
        {

            _lockState = true;

            enemy.ChangeState(Enemy.EnemyStates.ATTACK);
        }

        enemy.transform.LookAt(enemy.target.transform.position);
    }

    public override void OnStateExit()
    {

        base.OnStateExit();
    }
}
