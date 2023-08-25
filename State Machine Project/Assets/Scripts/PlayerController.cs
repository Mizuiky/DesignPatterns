using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]

    public Rigidbody rb;
    public float defaultSpeed;
    public float runSpeed;
    private float _movementSpeed;
    public KeyCode _runKey;

    private Vector3 _movement;
    private float _horizontal;
    private float _vertical;

    [Header("Rotation")]

    public float rotationSpeed;
    private Quaternion newRotation;

    [Header("Animation")]

    public Animator animator;

    [Header("Attack")]

    public KeyCode attackKey;
    private int _attackCounter;
    private bool _isAttacking;

    [Header("Jump")]

    public float jumpForce;
    private float _ySpeed;
    public float gravitySpeed;
    public float _gravity;

    private bool _isGrounded;

    private bool _isDoubleJump;
    private bool _isJumping = true;

    public float maxDistance;

    public HealthBase playerHealth;

    private bool _isDead;

    void Start()
    {
        Init();
    }

    void Update()
    {
        if(!_isDead)
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");

            //Attack();

            if (Input.GetKeyDown(KeyCode.Space) && !_isJumping)
            {
                //Debug.Log(" 1 Jump");
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                playerHealth.OnDamage(2);
            }

            Attack();
        }       
    }

    public void FixedUpdate()
    {       
        if(!_isDead)
            Move();
    }

    private void Init()
    {
        _attackCounter = 0;
        _isAttacking = false;
        _isJumping = false;
        _isDead = false;

        playerHealth.onDamage += Damage;
        playerHealth.onKill += Kill;
    }

    public void OnDisable()
    {
        playerHealth.onDamage -= Damage;
        playerHealth.onKill -= Kill;
    }

    #region Movement 

    private void Move()
    {

        if (_isJumping)
        {
            _ySpeed += _gravity * gravitySpeed * Time.deltaTime;
            Debug.Log("IS jumping");
        }
        else
        {
            _ySpeed = 0f;
        }

        _movement = new Vector3(_horizontal, 0, _vertical);

        if (!_isJumping)
        {
            //Debug.Log("not jumping");

            if (_movement == Vector3.zero)
            {
                Debug.Log("idle");
                animator.Play(Animator.StringToHash("Idle_Normal_SwordAndShield"));
            }
            else if (Input.GetKey(_runKey))
            {
                _movementSpeed = runSpeed;
                animator.speed = 1.3f;

                Debug.Log("sprint");

                animator.Play(Animator.StringToHash("MoveFWD_Normal_InPlace_SwordAndShield"));
            }
            else
            {
                _movementSpeed = defaultSpeed;
                animator.speed = 1f;

                Debug.Log("run");
                animator.Play(Animator.StringToHash("SprintFWD_Battle_InPlace_SwordAndShield 0"));
            }

            _movement *= _movementSpeed;

            Rotate();

            //Debug.Log("movement" + _movement);
        }

        _movement.y = _ySpeed;

        rb.velocity = _movement;

    }

    #endregion

    #region Rotation

    private void Rotate()
    {
        if (_movement != Vector3.zero)
        {
            /**** creates a quaternion that will rotate in the movement direction using y axis ****/
            newRotation = Quaternion.LookRotation(_movement, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }
    }

    #endregion

    #region Attack

    private void Attack()
    {
        if (Input.GetKeyDown(attackKey))
        {
            _isAttacking = true;
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        animator.SetInteger("Counter", _attackCounter);
        animator.SetBool("isAttacking", _isAttacking);

        yield return new WaitForSeconds(.2f);

        _attackCounter++;

        if (_attackCounter >= 2)
        {
            _attackCounter = 0;
        }

        _isAttacking = false;
        animator.SetBool("isAttacking", _isAttacking);
    }
    #endregion

    private void Jump()
    {
        //Debug.Log(" 2 Jump");

        animator.SetBool("isJumping", true);

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        _isJumping = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION ENTER");

        if(collision.gameObject.CompareTag("Ground") && !_isDead)
        {
            Debug.Log("NOT jumping");
            _isJumping = false;
            animator.SetBool("isJumping", false);
        }
    }

    private void Damage()
    {
        Debug.Log("player controller damage");

        //damage animation
    }

    private void Kill()
    {
        animator.Play(Animator.StringToHash("Die01_SwordAndShield"));
        _isDead = true;
    }
}