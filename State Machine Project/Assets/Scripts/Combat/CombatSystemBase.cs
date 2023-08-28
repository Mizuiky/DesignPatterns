using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystemBase : MonoBehaviour
{

    public float attackRange;
    public string enemiesLayer;
    public Transform attackPoint;

    public float attackCooldown;
    public float damage;

    protected float attackTime = 0;

    public Color gizsmoColor;

    public void Update()
    {
        if(attackTime < attackCooldown)
        {
            Debug.Log("time");

            attackTime += Time.deltaTime;
        }
        else
        {
            BeforeAttack();
        }
    }

    public virtual void BeforeAttack()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("attack enemy");

            Attack();

            attackTime = 0;
        }
    }

    public virtual void Attack()
    {

        Collider [] enemiesHit = Physics.OverlapSphere(attackPoint.position, attackRange, LayerMask.GetMask(enemiesLayer));

        if(enemiesHit.Length > 0)
        {
            foreach(Collider enemy in enemiesHit)
            {
                Debug.Log("hit enemy");

                var opponent = enemy.gameObject.GetComponent<IDamageable>();

                if(opponent != null)
                {
                    opponent.OnDamage(damage);
                }
            }
        }   
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = gizsmoColor;

        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
}
