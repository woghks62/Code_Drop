using UnityEngine;

public class MeleeAttackState : IEnemyState
{
    private EnemyBase _enemyBase;

    public MeleeAttackState(EnemyBase enemyBase)
    {
        _enemyBase = enemyBase;
    }
    
    public void Enter()
    {
        Debug.Log("Attack 진입");
    }

    public void Execute()
    {
        Debug.Log("Attack 실행중");
    }

    public void Exit()
    {
        Debug.Log("Attack 종료");
    }
}
