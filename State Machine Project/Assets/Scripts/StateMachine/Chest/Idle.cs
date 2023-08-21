using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Idle : EnemyStateBase
{
    private bool _hasInitiated = false;
    private bool _isLooking = false;

    public override void OnStateEnter(params object[] obj)
    {

        base.OnStateEnter(obj);

        if(!_hasInitiated)
        {

            enemy.transform.DOScale(0, .3f).SetEase(Ease.OutBack).From();

            _hasInitiated = true;
        }

        enemy.ChangeAnimationState(Enemy.AnimationStates.IdleChest.ToString());

        _isLooking = false;       
    }

    public override void OnStateUpdate()
    {

        base.OnStateUpdate();

        if (Vector3.Distance(enemy.transform.position, enemy.target.transform.position) < enemy.distanceToLook && !_isLooking)
        { 

            _isLooking = true;

            enemy.LookToPlayer();
        }
    }

    public override void OnStateExit()
    {

        base.OnStateExit();
    }
}
