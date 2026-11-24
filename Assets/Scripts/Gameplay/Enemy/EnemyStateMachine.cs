using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private IEnemyState _currentState;
    
    public IEnemyState CurrentState => _currentState;
    private EnemyBase _enemybase;

    private void Awake()
    {
        _enemybase = GetComponent<EnemyBase>();
    }
    
    public void Initialize(IEnemyState startState)
    { 
        _currentState = startState;
        _currentState.Enter();
    }

    public void ChangeState(IEnemyState newState)
    {
        if(_currentState == newState) return;
        if (_enemybase.IsDead) return;
        
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void Execute()
    {
        _currentState.Execute();
    }
}
