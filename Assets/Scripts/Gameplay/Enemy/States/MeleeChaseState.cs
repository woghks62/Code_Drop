using UnityEngine;

public class MeleeChaseState : IEnemyState
{
    private EnemyBase _enemyBase;
    private int _moveSpeed = 5;

    public MeleeChaseState(EnemyBase enemyBase)
    {
        _enemyBase = enemyBase;
    }
    
    public void Enter()
    {
        Debug.Log("Chase 진입");
        //Execute();
    }

    public void Execute()
    {
        Debug.Log("Chase 실행중");
        Vector3 direction =
            _enemyBase.Target.transform.position - _enemyBase.transform.position;

        direction.y = 0f;
        direction.Normalize();
        
        float distance = Vector3.Distance(_enemyBase.transform.position, _enemyBase.Target.transform.position);
        if (distance >= 1)
        {
            _enemyBase.transform.position += direction * _moveSpeed * Time.deltaTime;
        }
    }

    public void Exit()
    {
        Debug.Log("Chase 종료");
    }
}
