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
        TAUT,
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
    public float timeToAttack = 1f;
    public GameObject target;

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

    public bool isMoving = false;

    private Rigidbody _rb;

    [SerializeField]
    private float speed;

    private Vector3 _defaultVelocity = new Vector3(0,0,1);

    public HealthBase enemyHealth;

    public bool isAlive;

    #endregion

    public void Start()
    {
        Init();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Insert))
        {
            enemyHealth.OnDamage(2f);
        }
        if(enemyMachine != null && isAlive)
        {
            enemyMachine.Update();
        }       
    }

    public void FixedUpdate()
    {
        if(isMoving && isAlive)
            Move();
    }

    private void Init()
    {
        isAlive = true;

        animator.SetBool("isAlive", true);

        enemyHealth.onDamage += Damage;
        enemyHealth.onKill += Kill;

        enemyMachine = new StateMachine();

        enemyMachine.Init();

        enemyMachine.RegisterState(EnemyStates.IDLE, new Idle());
        enemyMachine.RegisterState(EnemyStates.NORMALIDLE, new NormalIdleState());
        enemyMachine.RegisterState(EnemyStates.WALK, new WalkState());
        enemyMachine.RegisterState(EnemyStates.RUN, new Run());
        enemyMachine.RegisterState(EnemyStates.PATROL, new Patrol());
        enemyMachine.RegisterState(EnemyStates.ATTACK, new AttackState());
        enemyMachine.RegisterState(EnemyStates.TAUT, new TautState());
        enemyMachine.RegisterState(EnemyStates.DEATH, new Death());

        _rb = GetComponent<Rigidbody>();

        enemyCollider.SetActive(true);
        hitBox.SetActive(false);

        isMoving = false;

        target = GameManager.Instance.Player.gameObject;

        enemyMachine.ChangeState(EnemyStates.IDLE, this);
    }

    private void OnDisable()
    {
        enemyHealth.onDamage -= Damage;
        enemyHealth.onKill -= Kill;
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
        Debug.Log("before on attack");

        _currentAnimatorState = AnimationStates.Attack01.ToString();

        hitBox.SetActive(true);

        animator.Play(Animator.StringToHash(AnimationStates.Attack01.ToString()));

        yield return new WaitForSeconds(timeToAttack);       

        hitBox.SetActive(false);

        Debug.Log("after on attack");

        attack?.Invoke();
    }

    #endregion

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        transform.LookAt(target.transform.position);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(this.transform.position, new Vector3(1.5f, 0f, -4.28f));
    }

    private void Damage()
    {
        ChangeState(EnemyStates.TAUT);
        //enemy animation
    }

    private void Kill()
    {
        Debug.Log("kill");

        isAlive = false;

        isMoving = false;

        ChangeState(EnemyStates.DEATH);     
    }

    public void InvokeDisable()
    {
        Invoke(nameof(DisableEnemy), 1.5f);
    }

    private void DisableEnemy()
    {
        gameObject.SetActive(false);
    }
}
