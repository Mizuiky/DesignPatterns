using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IActivate
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
    public string currentAnimatorState;

    [Header("Enemy settings")]
    public float distanceToLook = 15;
    public float attackDistance = 2;
    public float timeToAttack = 1f;
    public PlayerController target;
    public bool isAttacking = false;

    [Header("Movement")]

    public bool isMoving = false;

    public HealthBase enemyHealth;

    public bool isAlive;

    public DropItem drop;

    public bool IsActive { get; set; }

    public Vector3 SetPosition { get { return transform.position; } set { transform.position = value; } }

    #endregion

    #region Private Fields

    [Header("Colliders")]
    [SerializeField]
    private GameObject _enemyCollider;

    private Rigidbody _rb;

    [SerializeField]
    private float _speed;

    private Vector3 _defaultVelocity = new Vector3(0,0,1);

    #endregion

    public void Start()
    {
        Init();
    }

    public void Update()
    {

        if(enemyMachine != null && isAlive)
        {
            enemyMachine.Update();
        }       
    }

    public void FixedUpdate()
    {
        if(isMoving && isAlive)
        {
            Debug.Log("is moving");

            Move();
        }
    }

    public void Init()
    {
        IsActive = false;
     
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

        target = GameManager.Instance.Player;

        isMoving = false;
        isAlive = true;

        gameObject.SetActive(false);
    }

    public void OnActivate()
    {
        gameObject.SetActive(true);

        enemyHealth.Reset();

        enemyHealth.onDamage += Damage;
        enemyHealth.onKill += Kill;

        GameManager.Instance.Player.onPlayerDeath += Stop;

        IsActive = true;
        isAlive = true;
        isMoving = false;

        enemyMachine.ChangeState(EnemyStates.IDLE, this);       
    }

    public void OnDeactivate()
    {
        enemyHealth.onDamage -= Damage;
        enemyHealth.onKill -= Kill;

        GameManager.Instance.Player.onPlayerDeath -= Stop;

        isMoving = false;
        isAlive = false;

        IsActive = false;
        gameObject.SetActive(false);
    }

    #region Change States

    public void ChangeAnimationState(string newState)
    {
        if (currentAnimatorState == newState) return;

        animator.Play(Animator.StringToHash(newState));

        currentAnimatorState = newState;
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

        isAttacking = true;

        if(target.playerHealth.CurrentLife <= 0)
        {
            animator.Play(Animator.StringToHash(AnimationStates.Victory.ToString()));
        }
        else
        {
            currentAnimatorState = AnimationStates.Attack01.ToString();

            animator.Play(Animator.StringToHash(AnimationStates.Attack01.ToString()));

            yield return new WaitForSeconds(timeToAttack);

            Debug.Log("after on attack");        

            attack?.Invoke();
        }

        isAttacking = false;
    }

    #endregion

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, _speed * Time.deltaTime);

        transform.LookAt(target.transform.position);
    }

    private void Stop()
    {
        enemyMachine.ChangeState(EnemyStates.IDLE, this);

        OnDeactivate();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(this.transform.position, new Vector3(1.5f, 0f, -4.28f));
    }

    #region Damage

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

    #endregion

    public void InvokeDeactivate()
    {
        Invoke(nameof(OnDeactivate), 1.5f);

        drop.OndropItem(transform);
    }
}
