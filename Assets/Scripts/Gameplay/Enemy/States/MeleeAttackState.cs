using UnityEngine;

public class MeleeAttackState : IEnemyState
{
    private MeleeEnemy _meleeEnemy;
    
    public MeleeAttackState(MeleeEnemy meleeEnemy)
    {
        _meleeEnemy = meleeEnemy;
    }
    
    public void Enter()
    {
        Debug.Log("Attack 진입");
        _meleeEnemy.Animator.SetBool("IsAttacking", true);

    }

    public void Execute()
    {
        if (_meleeEnemy.IsAttack) return;
        _meleeEnemy.transform.LookAt(_meleeEnemy.Target.transform);
    }

    public void Exit()
    {
        Debug.Log("Attack 종료");
        _meleeEnemy.Animator.SetBool("IsAttacking", false);

    }
}
