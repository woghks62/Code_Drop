using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private EnemyBase _enemyBase;

    public EnemyPatrolState(EnemyBase enemyBase)
    {
        _enemyBase = enemyBase;
    }
    
    public void Enter()
    {
        Debug.Log("Patrol 진입");
    }

    public void Execute()
    {
        Debug.Log("Patrol 실행 중");
    }

    public void Exit()
    {
        Debug.Log("Patrol 나감");
    }
}
