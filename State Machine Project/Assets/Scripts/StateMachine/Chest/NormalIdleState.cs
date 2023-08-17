using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalIdleState : EnemyStateBase
{
    private bool lockState  = false;
    public override void OnStateEnter(params object[] obj)
    {
        base.OnStateEnter(obj);

        enemy.ChangeAnimationState(Enemy.AnimationStates.IdleNormal.ToString());

        lockState = false;
    }

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        if (Vector3.Distance(enemy.transform.position, GameManager.Instance.Player.transform.position) > enemy.distanceToLook && !lockState)
        {
            lockState = true;

            enemy.ChangeState(Enemy.EnemyStates.IDLE, enemy);
        }
        else if(Vector3.Distance(enemy.transform.position, GameManager.Instance.Player.transform.position) < enemy.attackDistance && lockState)
        {
            lockState = true;

            enemy.Attack();
        }

        enemy.transform.LookAt(GameManager.Instance.Player.transform.position);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}
