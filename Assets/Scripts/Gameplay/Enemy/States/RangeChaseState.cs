using UnityEngine;

public class RangeChaseState : IEnemyState
{
    private RangeEnemy _rangeEnemy;
    
    public RangeChaseState(RangeEnemy rangeEnemy)
    {
        _rangeEnemy = rangeEnemy;
    }
    
    public void Enter()
    {
        Debug.Log("RangeChaseState 진입");
        _rangeEnemy.Animator.SetBool("IsChasing", true);
        
        _rangeEnemy.Agent.isStopped = false;
        _rangeEnemy.Agent.speed = _rangeEnemy.Movespeed;
        _rangeEnemy.Agent.stoppingDistance = _rangeEnemy.IsWall() ? 1f : _rangeEnemy.AttackRange;
    }

    public void Execute()
    {
        _rangeEnemy.Agent.SetDestination(_rangeEnemy.Target.transform.position);
    }

    public void Exit()
    {
        Debug.Log("RangeChaseState 종료");
        _rangeEnemy.Animator.SetBool("IsChasing", false);
        
        _rangeEnemy.Agent.ResetPath();
    }
}
