using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected LayerMask _targetMask;
    
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    
    [SerializeField] private bool _isDead;

    private GameObject _target;
    public GameObject Target => _target;

    public void SetTarget(GameObject target)
    {
        _target = target;
    }
    
    
    public EnemyStateMachine EnemyStateMachine{get; set;}
    public EnemyIdleState EnemyIdleState{get; set;}
    public EnemyPatrolState EnemyPatrolState{get; set;}
    public EnemyDeadState EnemyDeadState{get; set;}

    public void Awake() => CacheComponents();

    private void Start() => EnemyStateMachine.Initialize(EnemyIdleState);
    
    public void TakeDamage(int damage)
    {
        if (_isDead) return;
        
        _currentHealth = _currentHealth - damage < 0 ? 0 : _currentHealth - damage;
        if (_currentHealth <= 0)
        {
            _isDead = true;
            EnemyStateMachine.ChangeState(EnemyDeadState);    
        }
    }

    private void CacheComponents()
    {
        EnemyStateMachine = GetComponent<EnemyStateMachine>();
        EnemyIdleState = new EnemyIdleState(this);
        EnemyPatrolState = new EnemyPatrolState(this);
        EnemyDeadState = new EnemyDeadState(this);
        _currentHealth =  _maxHealth;
    }
}
