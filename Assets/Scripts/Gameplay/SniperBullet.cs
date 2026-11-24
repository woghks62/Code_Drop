using UnityEngine;

public class SniperBullet : Bullet
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IDamageable>()?.TakeDamage(Damage);

        if (((1 << other.gameObject.layer) & TargetLayer) != 0)
        {
            Destroy(gameObject);
        }
    }
}
