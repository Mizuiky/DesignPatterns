using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]

    public Rigidbody rb;
    public float defaultSpeed;
    public float runSpeed;
    public KeyCode _runKey;

    private Vector3 _movement;
    private float _horizontal;
    private float _vertical;

    [Header("Rotation")]

    public float rotationSpeed;
    private Quaternion newRotation;

    [Header("Animation")]

    public Animator animator;

    private int _idle = Animator.StringToHash("Idle_Normal_SwordAndShield");
    private int _move = Animator.StringToHash("MoveFWD_Normal_InPlace_SwordAndShield");
    private int _run = Animator.StringToHash("SprintFWD_Battle_InPlace_SwordAndShield");
    private int _attack1 = Animator.StringToHash("Attack01_SwordAndShiled");
    private int _attack2 = Animator.StringToHash("Attack02_SwordAndShiled");

    [Header("Attack")]

    public KeyCode attackKey;
    private int _attackCounter;
    private bool _isAttacking;

    void Start()
    {
        Init();
    }
 
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        Attack();
    }

    public void FixedUpdate()
    {
        Move();
    }

    private void Init()
    {
        _attackCounter = 0;
        _isAttacking = false;
    }

    #region Movement 

    private void Move()
    {

        _movement = new Vector3(_horizontal, 0, _vertical);

        if(_movement != Vector3.zero)
        {
            if(Input.GetKey(_runKey))
            {
                _movement *= runSpeed;

                animator.Play(_run);
            }
            else
            {
                _movement *= defaultSpeed;

                animator.Play(_move);
            }
                
            rb.velocity = _movement;

            Rotate();          
        }  
        else
        {
            rb.velocity = Vector3.zero;
            animator.Play(_idle);
        }
    }

    #endregion

    #region Rotation

    private void Rotate()
    {

        /**** creates a quaternion that will rotate in the movement direction using y axis ****/
        newRotation = Quaternion.LookRotation(_movement , Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
    }

    #endregion

    #region Attack

    private void Attack()
    {
        if(Input.GetKeyDown(attackKey))
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

        if(_attackCounter >= 2)
        {
            _attackCounter = 0;
        }

        _isAttacking = false;
        animator.SetBool("isAttacking", _isAttacking);
    }
    #endregion
}
