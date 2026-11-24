using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField] private bool _isAttacked;
    
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
    
    private NavMeshAgent _agent;
    public NavMeshAgent Agent => _agent;
    
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
    public GameObject Target {get => _target; set => _target = value; }
    public bool IsDead => _isDead;
    public float Movespeed => _moveSpeed;
    public bool IsAttacked => _isAttacked;
    
    public EnemyStateMachine EnemyStateMachine{get; set;}
    public EnemyIdleState EnemyIdleState{get; set;}
    public EnemyPatrolState EnemyPatrolState{get; set;}
    public EnemyDeadState EnemyDeadState{get; set;}
    
    
    
    public virtual void Start()
    {
        Init();
        CacheComponents();
        EnemyStateMachine.Initialize(EnemyIdleState);
        _agent = GetComponent<NavMeshAgent>();
    }
    
    public void SetTarget(GameObject target)
    {
        _target = target;
    }
    
    public void TakeDamage(int damage)
    {
        if (_isDead) return;

        _isAttacked = true;
        _currentHealth = _currentHealth - damage < 0 ? 0 : _currentHealth - damage;
        if (_currentHealth <= 0)
        {
            _isDead = true;
            EnemyStateMachine.ChangeState(EnemyDeadState);
            GameManager.Instance.GameData.KillCount++;
            MissionManager.Instance.KillEnemy();
            StartCoroutine(DeleteRoutine());
        }
    }

    private IEnumerator DeleteRoutine()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
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
}
