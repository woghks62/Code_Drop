using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    
    [SerializeField] private bool _isDead;
    
    
    public EnemyStateMachine EnemyStateMachine{get; set;}
    public EnemyIdleState EnemyIdleState{get; set;}
    public EnemyPatrolState EnemyPatrolState{get; set;}
    public EnemyDeadState EnemyDeadState{get; set;}

    private void Awake()
    {
        EnemyStateMachine = GetComponent<EnemyStateMachine>();
        EnemyIdleState = new EnemyIdleState(this);
        EnemyPatrolState = new EnemyPatrolState(this);
        EnemyDeadState = new EnemyDeadState(this);
        _currentHealth =  _maxHealth;
    }

    private void Start()
    {
        EnemyStateMachine.Initialize(EnemyIdleState);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnemyStateMachine.ChangeState(EnemyPatrolState);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(30);
        }
    }
    
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
}
