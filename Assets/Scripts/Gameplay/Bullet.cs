using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private int _damage;
    
    public int Damage { get => _damage; set => _damage = value; }
    
    private Vector3 _moveDirection;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }
    
    private void Update()
    {
        MoveForward();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<IDamageable>() != null)
        {
            Debug.Log($"피격 대상 : {other.gameObject.name}");
            IDamageable damageable = other.gameObject.GetComponentInParent<IDamageable>();
            
            damageable.TakeDamage(_damage);
        }
        Destroy(gameObject);
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime *  _speed);
    }
    
    
}
