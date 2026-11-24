using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private EnemyBase _enemyBase;

    public EnemyIdleState(EnemyBase enemyBase)
    {   
        _enemyBase = enemyBase;
    }
    
    public void Enter()
    {
        //Debug.Log("Idle 진입");
    }

    public void Execute()
    {
        //Debug.Log("Idle 실행중");
    }

    public void Exit()
    {
        //Debug.Log("Idle 종료");
    }
}
