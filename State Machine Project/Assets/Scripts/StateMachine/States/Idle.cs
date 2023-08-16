using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Idle : EnemyStateBase
{
    private bool hasInitiated = false;
    private bool isLooking = false;

    public override void OnStateEnter(params object[] obj)
    {
        base.OnStateEnter(obj);

        if(!hasInitiated)
        {
            Debug.Log("Idle Enter");
            enemy.transform.DOScale(0, .3f).SetEase(Ease.OutBack).From();

            hasInitiated = true;
        }

        Debug.Log("entered idle");

        enemy.ChangeAnimationState(Enemy.AnimationStates.IdleChest.ToString());

        isLooking = false;       
    }

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        if (Vector3.Distance(enemy.transform.position, GameManager.Instance.Player.transform.position) < enemy.distanceToLook && !isLooking)
        { 
            Debug.Log("DISTANCE ENEMY pLAYER");

            isLooking = true;

            enemy.LookToPlayer();
        }
    }

    public override void OnStateExit()
    {
        base.OnStateExit();

        Debug.Log("IDLE EXIT");
    }
}
