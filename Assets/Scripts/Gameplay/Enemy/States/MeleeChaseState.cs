using UnityEngine;

public class MeleeChaseState : IEnemyState
{
    private EnemyBase _enemyBase;

    public MeleeChaseState(EnemyBase enemyBase)
    {
        _enemyBase = enemyBase;
    }
    
    public void Enter()
    {
        Debug.Log("Chase 진입");
        _enemyBase.Animator.SetBool("IsChasing", true);
        
        _enemyBase.Agent.isStopped = false;
        _enemyBase.Agent.speed = _enemyBase.Movespeed;
        _enemyBase.Agent.stoppingDistance = 1f;
    }

    public void Execute()
    {
        _enemyBase.Agent.SetDestination(_enemyBase.Target.transform.position);
    }

    public void Exit()
    {
        Debug.Log("Chase 종료");
        _enemyBase.Animator.SetBool("IsChasing", false);
        
        _enemyBase.Agent.ResetPath();
    }
}
