using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;  // OnDrawGizmos

public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] private Animator _animator;
    public Animator Animator => _animator;
    
    [SerializeField] protected LayerMask _targetMask;
    
    [SerializeField] private int _currentHealth;
    [SerializeField] private bool _isDead;
    [SerializeField] private GameObject _target;
    [SerializeField] private List<Transform> Patroltransfom = new();
    [SerializeField] protected Enemy _enemy;
    [SerializeField] private bool _isAttack;
    [SerializeField] private bool _seeTarget;
    
    [Header("EnemyStats")]
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
    
    public int Id => _id;
    public string Name => _name;
    public int MaxHealth => _maxHealth;
    public float MoveSpeed => _moveSpeed;
    public int AngleRange => _angleRange;
    public float Radius => _radius;
    public float FramingInterval => _framingInterval;
    public int Damage => _damage;
    public float AttackRange => _attackRange;
    public float ProjectileSpeed => _projectileSpeed;
    
    public List<Transform> PatroltransfomList => Patroltransfom;
    public bool IsAttack { get => _isAttack; set => _isAttack = value; }
    public bool SeeTarget { get => _seeTarget; set => _seeTarget = value; }
    public GameObject Target => _target;
    public bool IsDead => _isDead;
    public float Movespeed => _moveSpeed;
    
    public EnemyStateMachine EnemyStateMachine{get; set;}
    public EnemyIdleState EnemyIdleState{get; set;}
    public EnemyPatrolState EnemyPatrolState{get; set;}
    public EnemyDeadState EnemyDeadState{get; set;}
    
    
    public virtual void Start()
    {
        Init();
        CacheComponents();
        EnemyStateMachine.Initialize(EnemyIdleState);
    }
    
    public void SetTarget(GameObject target)
    {
        _target = target;
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
    
    private void Init()
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
        
        _currentHealth =  _maxHealth;
    }
    
    private void CacheComponents()
    {
        EnemyStateMachine = GetComponent<EnemyStateMachine>();
        EnemyIdleState = new EnemyIdleState(this);
        EnemyPatrolState = new EnemyPatrolState(this);
        EnemyDeadState = new EnemyDeadState(this);
        _animator = GetComponent<Animator>();
    }
    
    // ------------------------------------ 에디터 확인용
    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);
    private void OnDrawGizmos()
    {
        Handles.color = SeeTarget ? _red : _blue;
        // DrawSolidArc(시작점, 노멀벡터(법선벡터), 그려줄 방향 벡터, 각도, 반지름)
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, AngleRange / 2, Radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -AngleRange / 2, Radius);
    }
}
