using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TautState : EnemyStateBase
{
    private float nextAttackCooldown = 2f;
    private float time;

    private bool lockState;

    public override void OnStateEnter(params object[] obj)
    {
        base.OnStateEnter(obj);

        enemy.ChangeAnimationState(Enemy.AnimationStates.Taunting.ToString());

        enemy.isMoving = false;

        time = 0;

        lockState = false;
    }

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        if(time < nextAttackCooldown && !lockState)
        {
            time += Time.deltaTime;

        }
        else
        {
            lockState = true;
            Debug.Log("taut to attack");
            enemy.ChangeState(Enemy.EnemyStates.ATTACK);
        }
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}
