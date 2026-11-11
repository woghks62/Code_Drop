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
    }

    public void Execute()
    {
        Debug.Log("RangeAttack 실행중");
        // 애니메이션 재생 시키고 발사 쪽에서 Shoot 애니메이션 이벤트 함수로 사용
        _rangeEnemy.Shoot();
    }

    public void Exit()
    {
        Debug.Log("RangeAttack 종료");
    }
}
