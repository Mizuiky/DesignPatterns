using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackState : EnemyStateBase
{
    private bool lockState = false;

    public override void OnStateEnter(params object[] obj)
    {
        lockState = false;

        base.OnStateEnter(obj);
    }

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        if (Vector3.Distance(enemy.transform.position, GameManager.Instance.Player.transform.position) < enemy.attackDistance && !lockState)
        {
            Debug.Log("before attack");

            lockState = true;

            enemy.Attack(UnlockState);
        }
        else if(Vector3.Distance(enemy.transform.position, GameManager.Instance.Player.transform.position) > enemy.attackDistance && !lockState)
        {
            Debug.Log("attack to normal idle");

            lockState = true;

            enemy.ChangeState(Enemy.EnemyStates.NORMALIDLE);
        }
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public void UnlockState()
    {
        lockState = false;
    }
}
