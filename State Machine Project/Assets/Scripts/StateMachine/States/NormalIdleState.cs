using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalIdleState : EnemyStateBase
{
    private bool hasDistancedFromPlayer  = false;
    public override void OnStateEnter(params object[] obj)
    {
        base.OnStateEnter(obj);

        enemy.ChangeAnimationState(Enemy.AnimationStates.IdleNormal.ToString());

        hasDistancedFromPlayer = false;

        Debug.Log("entered normal idle");
    }

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        if (Vector3.Distance(enemy.transform.position, GameManager.Instance.Player.transform.position) > enemy.distanceToLook && !hasDistancedFromPlayer)
        {
            Debug.Log("Idle normal animation");

            hasDistancedFromPlayer = true;

            enemy.DistancedFromPlayer();       
        }
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}
