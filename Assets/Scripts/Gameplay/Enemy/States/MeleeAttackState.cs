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
    }

    public void Execute()
    {
        // 테스트
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _meleeEnemy.Attack();
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _meleeEnemy.AttackFinish();
        }
        
        if (_meleeEnemy.IsAttack) return;
        
        // 공격 애니메이션 실행
        Debug.Log("Attack 실행중");
        Debug.Log("공격 애니메이션 진행 중");
    }

    public void Exit()
    {
        Debug.Log("Attack 종료");
    }
}
