using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable _damageable))
        {
            _damageable.TakeDamage(50);
            Destroy(gameObject);
        }
    }
}
