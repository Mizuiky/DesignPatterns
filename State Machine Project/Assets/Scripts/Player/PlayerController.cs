using System;
using System.Collections;
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

    [Header("Jump")]

    public float jumpForce;
    private float _ySpeed;
    public float gravitySpeed;
    public float _gravity;

    private bool _isJumping = true;

    public float maxDistance;

    public float _movimentConst;
    public float _jumpConst;

    public HealthBase playerHealth;

    public Action onPlayerDeath;

    public Vector3 resetPosition;

    public Quaternion resetRotation;

    private bool _isAlive;
    private bool _canJump;

    private Animator animator;

    void Start()
    {
        Init();
    }

    void Update()
    {
        if(_isAlive)
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");

            //Attack();

            if (Input.GetKeyDown(KeyCode.Space) && !_isJumping)
            {
                //Debug.Log(" 1 Jump");
                Jump();
                //_canJump = true;
            }

            //Attack();
        }       
    }

    public void FixedUpdate()
    {       
        if(_isAlive)
        {
            Move();

            if(_canJump && !_isJumping)
            {
                Jump();
            }
        }
    }

    private void Init()
    {
        animator = GetComponent<Animator>();

        _isJumping = false;

        _isAlive = true;

        animator.SetBool("isAlive", true);

        _canJump = false;

        playerHealth.onDamage += Damage;
        playerHealth.onKill += Kill;

        resetPosition = transform.position;
        resetRotation = transform.rotation;

        Debug.Log("initial position" + resetPosition);
    }

    public void Reset()
    {
    
        _isAlive = true;

        transform.position = resetPosition;

        transform.rotation = resetRotation;

        Invoke(nameof(EnableAnimator), 0.2f);

        _isJumping = false;
        
        _canJump = false;

        playerHealth.Reset();       
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

            if (_movement == Vector3.zero)
            {
                animator.Play(Animator.StringToHash("Idle_Normal_SwordAndShield"));
            }
            else if (Input.GetKey(_runKey))
            {
                _movementSpeed = runSpeed;
                animator.speed = 1.3f;

                animator.Play(Animator.StringToHash("MoveFWD_Normal_InPlace_SwordAndShield"));
            }
            else
            {
                _movementSpeed = defaultSpeed;
                animator.speed = 1f;

                animator.Play(Animator.StringToHash("SprintFWD_Battle_InPlace_SwordAndShield 0"));
            }

            _movement *= _movementSpeed;

            Rotate();
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

    private void Jump()
    {
        //Debug.Log(" 2 Jump");

        animator.SetBool("isJumping", true);

        Debug.Log("before jump velocity" + rb.velocity);

        //_movement.y = jumpForce;

        rb.velocity += (_movement + (Vector3.up * jumpForce)) * _jumpConst;

        Debug.Log("jump velocity" + rb.velocity);

        //rb.AddForce(((_movement * _movimentConst) + (Vector3.up * jumpForce)) * _jumpConst, ForceMode.VelocityChange);

        _isJumping = true;
        _canJump = false;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.CompareTag("Ground") && !_isAlive)
        {
            _isJumping = false;
            _canJump = false;

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
       
        _isAlive = false;

        animator.SetBool("isAlive", false);

        Invoke(nameof(DisableAnimator), 0.5f);

        onPlayerDeath?.Invoke();
    }

    public void KillPlayer()
    {
        playerHealth.KillPlayer();
    }

    private void DisableAnimator()
    {
        animator.enabled = false;
    }

    private void EnableAnimator()
    {
        animator.enabled = true;

        animator.SetBool("isAlive", true);
    }
}