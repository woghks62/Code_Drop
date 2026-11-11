using UnityEngine;

public class RangeChaseState : IEnemyState
{
    private RangeEnemy _rangeEnemy;
    
    public RangeChaseState(RangeEnemy rangeEnemy)
    {
        _rangeEnemy = rangeEnemy;
    }
    
    public void Enter()
    {
        Debug.Log("RangeChaseState 진입");
    }

    public void Execute()
    {
        Vector3 direction = _rangeEnemy.Target.transform.position - _rangeEnemy.gameObject.transform.position;

        direction.y = 0f;
        direction.Normalize();
        
        float distance = Vector3.Distance(_rangeEnemy.transform.position, _rangeEnemy.Target.transform.position);
        if (distance >= 1)
        {
            _rangeEnemy.transform.LookAt(_rangeEnemy.Target.transform);
            _rangeEnemy.transform.position += direction * _rangeEnemy.Movespeed * Time.deltaTime;
        }
    }

    public void Exit()
    {
        Debug.Log("RangeChaseState 종료");
    }
}
