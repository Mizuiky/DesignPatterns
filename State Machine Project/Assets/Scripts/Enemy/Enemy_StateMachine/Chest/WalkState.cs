using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : EnemyStateBase
{
    private bool _lockState = false;
    public override void OnStateEnter(params object[] obj)
    {
        _lockState = false;

        base.OnStateEnter(obj);

        enemy.ChangeAnimationState(Enemy.AnimationStates.WalkBWD.ToString());
    }

    public override void OnStateUpdate()
    {
        enemy.isMoving = true;

        base.OnStateUpdate();

        if(Vector3.Distance(enemy.transform.position, enemy.target.transform.position) < enemy.attackDistance && !_lockState)
        {
            enemy.isMoving = false;
            _lockState = true;

            enemy.ChangeState(Enemy.EnemyStates.ATTACK);
        }
        else if(Vector3.Distance(enemy.transform.position, enemy.target.transform.position) > enemy.distanceToLook && !_lockState)
        {
            enemy.isMoving = false;

            _lockState = true;
            enemy.ChangeState(Enemy.EnemyStates.IDLE);
        }
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}
