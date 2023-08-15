using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyStates
    {
        IDLE_1,
        IDLE_2,
        WALK,
        RUN,
        PATROL,
        ATTACK,
        DIZZ,
        DEATH
    }

    private StateMachine enemyMachine;

    public Transform patrolLocations;

    public void Start()
    {
        
    }

    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
        
    }

    private void Init()
    {
        enemyMachine = new StateMachine();

        

        
    }
}
