using UnityEngine;

public class RangeAttackState : IEnemyState
{
    private RangeEnemy _rangeEnemy;
    
    public RangeAttackState(RangeEnemy rangeEnemy)
    {
        _rangeEnemy = rangeEnemy;
    }
    
    public void Enter()
    {
        Debug.Log("RangeAttack 진입");
        _rangeEnemy.Agent.isStopped = true;
        _rangeEnemy.Animator.SetBool("IsAttacking", true);
    }

    public void Execute()
    {
        _rangeEnemy.Shoot();
    }

    public void Exit()
    {
        Debug.Log("RangeAttack 종료");
        _rangeEnemy.Animator.SetBool("IsAttacking", false);
    }
}
