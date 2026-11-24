using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private EnemyBase _enemyBase;
    
    private int _currentPatrolIndex = 0;

    public EnemyPatrolState(EnemyBase enemyBase)
    {
        _enemyBase = enemyBase;
    }
    
    public void Enter()
    {
        Debug.Log("Patrol 진입");
        _enemyBase.Animator.SetBool("IsPatrolling", true);
        
        _enemyBase.Agent.isStopped = false;
        _enemyBase.Agent.speed = _enemyBase.Movespeed;
        _enemyBase.Agent.stoppingDistance = 0.1f;

        _enemyBase.Agent.SetDestination(
            _enemyBase.PatroltransfomList[_currentPatrolIndex].position);
    }

    public void Execute()
    {
        if (_enemyBase.PatroltransfomList.Count == 0)
        {
            return;
        }
        
        if (_enemyBase.Agent.remainingDistance <= _enemyBase.Agent.stoppingDistance)
        {
            _currentPatrolIndex++;

            if (_currentPatrolIndex >= _enemyBase.PatroltransfomList.Count)
            {
                _currentPatrolIndex = 0;
            }

            _enemyBase.Agent.SetDestination(
                _enemyBase.PatroltransfomList[_currentPatrolIndex].position);
        }
    }

    public void Exit()
    {
        Debug.Log("Patrol 나감");
        _enemyBase.Animator.SetBool("IsPatrolling", false);
        
        _enemyBase.Agent.ResetPath();
    }
}
