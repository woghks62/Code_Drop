using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Data/EnemyStats")]
public class EnemyStats : ScriptableObject
{
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

    public void SetData(
        int id, string name, int maxHealth, float moveSpeed, 
        int angleRange, float radius, float framingInterval,
        int damage, float attackRange,  float projectileSpeed)
    {
        _id = id;
        _name = name;
        _maxHealth = maxHealth;
        _moveSpeed = moveSpeed;
        _angleRange = angleRange;
        _radius = radius;
        _framingInterval = framingInterval;
        _damage = damage;
        _attackRange = attackRange;
        _projectileSpeed = projectileSpeed;
    }
}
