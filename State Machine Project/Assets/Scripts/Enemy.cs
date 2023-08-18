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

    #region Public Fields

    [Header("State machine")]
    [HideInInspector]
    public StateMachine enemyMachine;

    [Header("Animator")]
    public Animator animator;
    public string _currentAnimatorState;

    [Header("Enemy settings")]
    public float distanceToLook = 15;
    public float attackDistance = 8;
    public int attackNumber = 3;
    public float timeToAttack = 1f;

    [Header("Patrol")]
    public Transform patrolLocations;

    #endregion

    #region Private Fields

    [Header("Colliders")]
    [SerializeField]
    private GameObject enemyCollider;

    [SerializeField]
    public GameObject hitBox;

    [Header("Movement")]
    private Rigidbody _rb;

    [SerializeField]
    private float speed;

    private Vector3 _defaultVelocity = new Vector3(0,0,1);

    #endregion

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
        Move();
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

        _rb = GetComponent<Rigidbody>();

        enemyCollider.SetActive(true);
        hitBox.SetActive(false);

        enemyMachine.ChangeState(EnemyStates.IDLE, this);
    }

    #region Change States

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

    #endregion

    #region Search For Player

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

    #endregion

    #region Attack

    public void Attack(Action attack = null)
    {
        StartCoroutine(OnAttack(attack));
    }

    public IEnumerator OnAttack(Action attack = null)
    {
        hitBox.SetActive(true);

        for (int i = 0; i < attackNumber; i++)
        {

            ChangeAnimationState(AnimationStates.Attack01.ToString());

            yield return new WaitForSeconds(timeToAttack);
        }

        hitBox.SetActive(false);

        attack?.Invoke();
    }

    #endregion

    private void Move()
    {
        _rb.velocity = _defaultVelocity * speed * Time.deltaTime;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(this.transform.position, new Vector3(1.5f, 0f, -4.28f));
    }
}
