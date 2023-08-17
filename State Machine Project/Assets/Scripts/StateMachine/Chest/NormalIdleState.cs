using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalIdleState : EnemyStateBase
{
    private bool lockState  = false;
    public override void OnStateEnter(params object[] obj)
    {
        Debug.Log("normal state");

        base.OnStateEnter(obj);

        enemy.ChangeAnimationState(Enemy.AnimationStates.IdleNormal.ToString());

        lockState = false;
    }

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        Debug.Log("distance idle normal" + Vector3.Distance(enemy.transform.position, GameManager.Instance.Player.transform.position));

        if (Vector3.Distance(enemy.transform.position, GameManager.Instance.Player.transform.position) > enemy.distanceToLook && !lockState)
        {
            lockState = true;

            enemy.ChangeState(Enemy.EnemyStates.IDLE);
        }
        else if(Vector3.Distance(enemy.transform.position, GameManager.Instance.Player.transform.position) < enemy.attackDistance && !lockState)
        {
            Debug.Log("attack state");

            lockState = true;

            enemy.ChangeState(Enemy.EnemyStates.ATTACK);
        }

        enemy.transform.LookAt(GameManager.Instance.Player.transform.position);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}
