using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatSystem : CombatSystemBase
{
    public Animator animator;

    [Header("Attack")]

    public KeyCode attackKey;
    private int _attackCounter;

    public void Start()
    {
        _attackCounter = 0;
    }

    public override void Attack()
    {
        base.Attack();

        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        Debug.Log("attack coroutine");

        animator.SetInteger("Counter", _attackCounter);
        animator.SetBool("isAttacking", true);

        yield return new WaitForSeconds(.2f);

        _attackCounter++;

        if (_attackCounter >= 2)
        {
            _attackCounter = 0;
        }

        animator.SetBool("isAttacking", false);
    }

    public override void BeforeAttack()
    {
        if (Input.GetKeyDown(attackKey))
        {
            Debug.Log("attack");

            Attack();

            attackTime = 0;
        }
    }
}
