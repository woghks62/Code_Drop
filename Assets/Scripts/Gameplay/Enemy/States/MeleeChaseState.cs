using UnityEngine;

public class MeleeChaseState : IEnemyState
{
    private EnemyBase _enemyBase;

    public MeleeChaseState(EnemyBase enemyBase)
    {
        _enemyBase = enemyBase;
    }
    
    public void Enter()
    {
        Debug.Log("Chase 진입");
    }

    public void Execute()
    {
        Debug.Log("Chase 실행중");
        Vector3 direction = _enemyBase.Target.transform.position - _enemyBase.gameObject.transform.position;

        direction.y = 0f;
        direction.Normalize();
        
        float distance = Vector3.Distance(_enemyBase.transform.position, _enemyBase.Target.transform.position);
        if (distance >= 1)
        {
            _enemyBase.transform.LookAt(_enemyBase.Target.transform);
            _enemyBase.transform.position += direction * _enemyBase.Movespeed * Time.deltaTime;
        }
    }

    public void Exit()
    {
        Debug.Log("Chase 종료");
    }
}
