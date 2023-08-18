using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackState : EnemyStateBase
{
    private bool _lockState = false;

    public override void OnStateEnter(params object[] obj)
    {
        _lockState = false;

        base.OnStateEnter(obj);
    }

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        if (Vector3.Distance(enemy.transform.position, GameManager.Instance.Player.transform.position) < enemy.attackDistance && !_lockState)
        {
            Debug.Log("before attack");

            _lockState = true;

            enemy.Attack(UnlockState);
        }
        else if(Vector3.Distance(enemy.transform.position, GameManager.Instance.Player.transform.position) > enemy.attackDistance && !_lockState)
        {
            Debug.Log("attack to normal idle");

            _lockState = true;

            enemy.ChangeState(Enemy.EnemyStates.NORMALIDLE);
        }
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public void UnlockState()
    {
        _lockState = false;
    }
}
