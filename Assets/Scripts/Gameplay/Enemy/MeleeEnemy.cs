using System;
using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    public MeleeAttackState MeleeAttackState{get; set;}
    public MeleeChaseState MeleeChaseState{get; set;}
    
    private void Awake()
    {
        base.Awake();
        CacheComponents();
    }

    private void Update()
    {
        EnemyStateMachine.Execute();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if ((_targetMask & (1 << other.gameObject.layer)) !=0)
        {
            SetTarget(other.gameObject);
            EnemyStateMachine.ChangeState(MeleeChaseState);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((_targetMask & (1 << other.gameObject.layer)) !=0)
        {
            EnemyStateMachine.ChangeState(EnemyPatrolState);
        }
    }

    private void CacheComponents()
    {
        MeleeChaseState =  new MeleeChaseState(this);
        MeleeAttackState = new MeleeAttackState(this);
    }
}
