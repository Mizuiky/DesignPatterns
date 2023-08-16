using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyStates
    {
        IDLE,
        NORMALIDLE,
        WALK,
        RUN,
        PATROL,
        ATTACK,
        DIZZ,
        DEATH
    }

    public enum AnimationStates
    {
        IdleChest,
        IdleNormal,
        SenseSomethingRPT,
        WalkBWD,
        Run,
        Attack01,
        Dizzy,
        Taunting,
        Die,
        Victory
    }

    [HideInInspector]
    public StateMachine enemyMachine;

    public Transform patrolLocations;

    public Animator animator;
    public string _currentAnimatorState;

    public float distanceToLook = 13;

    public void Start()
    {
        Init();
    }

    public void Update()
    {
        if(enemyMachine != null)
        {
            enemyMachine.Update();
        }       
    }

    public void FixedUpdate()
    {
        
    }

    private void Init()
    {
        enemyMachine = new StateMachine();

        enemyMachine.Init();

        enemyMachine.RegisterState(EnemyStates.IDLE, new Idle());
        enemyMachine.RegisterState(EnemyStates.NORMALIDLE, new NormalIdleState());
        enemyMachine.RegisterState(EnemyStates.WALK, new Walk());
        enemyMachine.RegisterState(EnemyStates.RUN, new Run());
        enemyMachine.RegisterState(EnemyStates.PATROL, new Patrol());
        enemyMachine.RegisterState(EnemyStates.ATTACK, new Attack());
        enemyMachine.RegisterState(EnemyStates.DIZZ, new Dizzy());
        enemyMachine.RegisterState(EnemyStates.DEATH, new Death());

        enemyMachine.ChangeState(EnemyStates.IDLE, this);
    }

    public void ChangeAnimationState(string newState)
    {
        if (_currentAnimatorState == newState) return;

        animator.Play(Animator.StringToHash(newState));

        _currentAnimatorState = newState;
    }

    public void ChangeState(System.Enum state, params object[] obj)
    {
        enemyMachine.ChangeState(state , obj);
    }

    public void LookToPlayer()
    {
        StartCoroutine(Look());
    }

    public IEnumerator Look()
    {
        Debug.Log("LOOK");

        ChangeAnimationState(AnimationStates.SenseSomethingRPT.ToString());

        yield return new WaitForSeconds(5f);

        ChangeState(EnemyStates.NORMALIDLE, this);
    }

    public void DistancedFromPlayer()
    {
        ChangeState(EnemyStates.IDLE, this);
    }


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(this.transform.position, new Vector3(0, 0, 12.72f));
    }
}
