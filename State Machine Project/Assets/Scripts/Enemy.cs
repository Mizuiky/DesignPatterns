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

    public float distanceToLook = 15;
    public float attackDistance = 8;
    public int attackNumber = 3;

    public float timeToAttack = 1f;

    [SerializeField]
    private GameObject enemyCollider;

    [SerializeField]
    public GameObject hitBox;

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
        enemyMachine.RegisterState(EnemyStates.ATTACK, new AttackState());
        enemyMachine.RegisterState(EnemyStates.DIZZ, new Dizzy());
        enemyMachine.RegisterState(EnemyStates.DEATH, new Death());

        enemyCollider.SetActive(true);
        hitBox.SetActive(false);

        enemyMachine.ChangeState(EnemyStates.IDLE, this);
    }

    public void ChangeAnimationState(string newState)
    {
        if (_currentAnimatorState == newState) return;

        animator.Play(Animator.StringToHash(newState));

        _currentAnimatorState = newState;
    }

    public void ChangeState(System.Enum state)
    {
        enemyMachine.ChangeState(state , this);
    }

    public void LookToPlayer()
    {
        StartCoroutine(Look());
    }

    public IEnumerator Look()
    {

        ChangeAnimationState(AnimationStates.SenseSomethingRPT.ToString());

        yield return new WaitForSeconds(5f);

        ChangeState(EnemyStates.NORMALIDLE);
    }

    public void Attack(Action attack = null)
    {
        StartCoroutine(OnAttack(attack));
    }

    public IEnumerator OnAttack(Action attack = null)
    {

        enemyCollider.SetActive(false);
        hitBox.SetActive(true);

        for (int i = 0; i < attackNumber; i++)
        {

            ChangeAnimationState(AnimationStates.Attack01.ToString());

            yield return new WaitForSeconds(timeToAttack);
        }

        enemyCollider.SetActive(true);
        hitBox.SetActive(false);

        attack?.Invoke();
    }


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(this.transform.position, new Vector3(0, 0, 12.72f));
    }
}
