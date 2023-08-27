using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackState : EnemyStateBase
{
    private bool _lockState = false;

    public override void OnStateEnter(params object[] obj)
    {
        base.OnStateEnter(obj);

        _lockState = false;

        enemy.isMoving = false;     
    }

    public override void OnStateUpdate()
    {

        base.OnStateUpdate();

        if (Vector3.Distance(enemy.transform.position, enemy.target.transform.position) < enemy.attackDistance && !_lockState)
        {
            Debug.Log("before attack");

            _lockState = true;

            enemy.Attack(UnlockState);
        }
        else if(Vector3.Distance(enemy.transform.position, enemy.target.transform.position) > enemy.attackDistance && !_lockState)
        {
            Debug.Log("attack to normal idle");

            _lockState = true;

            enemy.ChangeState(Enemy.EnemyStates.WALK);
        }

        enemy.transform.LookAt(enemy.target.transform.position);
    }

    public override void OnStateExit()
    {

        base.OnStateExit();
    }

    public void UnlockState()
    {
        Debug.Log("unlock");
        _lockState = false;
    }
}
