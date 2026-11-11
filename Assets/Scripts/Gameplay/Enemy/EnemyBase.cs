using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected LayerMask _targetMask;
    
    [SerializeField] private int _currentHealth;
    //[SerializeField] private int _movespeed;
    [SerializeField] private bool _isDead;
    [SerializeField] private GameObject _target;
    [SerializeField] private List<Transform> Patroltransfom = new();

    [SerializeField] protected Enemy _enemy = new();

    [Header("현재 EnemyStats")]
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _angleRange;
    [SerializeField] private float _radius;
    [SerializeField] private float _framingInterval;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _projectileSpeed;
    
    private void tmp()
    {
        _id = _enemy.EnemyStats.Id;
        _name = _enemy.EnemyStats.Name;
        _maxHealth = _enemy.EnemyStats.MaxHealth;
        _moveSpeed = _enemy.EnemyStats.MoveSpeed;
        _angleRange = _enemy.EnemyStats.AngleRange;
        _radius = _enemy.EnemyStats.Radius;
        _framingInterval = _enemy.EnemyStats.FramingInterval;
        _damage = _enemy.EnemyStats.Damage;
        _attackRange = _enemy.EnemyStats.AttackRange;
        _projectileSpeed = _enemy.EnemyStats.ProjectileSpeed;
    }
    
    public GameObject Target => _target;
    public bool IsDead => _isDead;
    public float Movespeed => _moveSpeed;
    public List<Transform> PatroltransfomList => Patroltransfom;
    public void SetTarget(GameObject target)
    {
        _target = target;
    }
    
    [SerializeField] private bool _isAttack;

    public bool IsAttack
    {
        get => _isAttack;
        set => _isAttack = value;
    }
    
    public EnemyStateMachine EnemyStateMachine{get; set;}
    public EnemyIdleState EnemyIdleState{get; set;}
    public EnemyPatrolState EnemyPatrolState{get; set;}
    public EnemyDeadState EnemyDeadState{get; set;}

    [SerializeField] private bool _seeTarget;
    public bool SeeTarget { get => _seeTarget; set => _seeTarget = value; }
    
    public void OnEnable()
    {
        tmp();
        CacheComponents();
    }

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
    
    public void OnTriggerEnter(Collider other)
    {
        if(IsAttack) return;
        
        if ((_targetMask & (1 << other.gameObject.layer)) !=0)
        {
            SetTarget(other.gameObject);
            EnemyStateMachine.ChangeState(EnemyPatrolState);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if ((_targetMask & (1 << other.gameObject.layer)) !=0)
        {
            SetTarget(null);
            _seeTarget = false;
            EnemyStateMachine.ChangeState(EnemyPatrolState);
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
