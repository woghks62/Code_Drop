using UnityEngine;

public class EnemyDeadState : IEnemyState
{
    private EnemyBase _enemyBase;

    public EnemyDeadState(EnemyBase enemyBase)
    {
        _enemyBase = enemyBase;
    }
    
    public void Enter()
    {
        Debug.Log("Dead 진입");
        _enemyBase.Animator.SetBool("IsDead", true);
    }

    public void Execute()
    {
        Debug.Log("Dead 실행중");
    }

    public void Exit()
    {
        Debug.Log("Dead 종료");
    }
}
