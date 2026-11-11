using UnityEngine;

public class MeleeKnife : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private LayerMask _targetLayer;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }

    public void Attack()
    {
        _collider.enabled = true;
    }

    public void Finish()
    {
        _collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((_targetLayer & (1 << other.gameObject.layer)) != 0)
        {
            if (other.TryGetComponent<IDamageable>(out IDamageable _damageable))
            {
                _damageable.TakeDamage(10);
            }    
        }
    }
}
