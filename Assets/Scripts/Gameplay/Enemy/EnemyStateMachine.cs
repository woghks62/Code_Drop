using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private IEnemyState _currentState;
    
    public IEnemyState CurrentState => _currentState;
    
    public void Initialize(IEnemyState startState)
    { 
        _currentState = startState;
        _currentState.Enter();
    }

    public void ChangeState(IEnemyState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void Execute()
    {
        _currentState.Execute();
    }
}
