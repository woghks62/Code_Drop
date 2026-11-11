using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private EnemyBase _enemyBase;
    
    private int _currentPatrolIndex = 0;

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

        if (_enemyBase.PatroltransfomList.Count == 0)
        {
            return;
        }
        
        Vector3 _target= _enemyBase.PatroltransfomList[_currentPatrolIndex].transform.position;
        
        _enemyBase.transform.LookAt(_target);
        _enemyBase.transform.position = Vector3.MoveTowards(_enemyBase.transform.position, _target, _enemyBase.Movespeed * Time.deltaTime);

        if (Vector3.Distance(_target, _enemyBase.transform.position) < 0.1f)
        {
            _currentPatrolIndex++;
        }

        if (_currentPatrolIndex >= _enemyBase.PatroltransfomList.Count)
        {
            _currentPatrolIndex = 0;
        }
    }

    public void Exit()
    {
        Debug.Log("Patrol 나감");
    }
}
