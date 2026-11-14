using System;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out PlayerShooter playerShooter))
        {
            return;
        }

        if (playerShooter.CurrentGunName == "Pistol")
        {
            return;
        }

        playerShooter.SpareMagazine += 3;
        Destroy(gameObject);
    }
}
