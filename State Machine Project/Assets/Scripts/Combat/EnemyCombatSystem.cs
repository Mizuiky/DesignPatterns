using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatSystem : CombatSystemBase
{
    public Enemy enemy;

    public override void BeforeAttack()
    {
        if(enemy.isAlive && enemy.isAttacking)
        {
            Debug.Log("attack player");

            Attack();

            attackTime = 0;
        }
    }
}
