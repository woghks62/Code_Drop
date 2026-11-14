using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private LayerMask _beaconLayer;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private List<EnemyBase> _enemies = new();
    [SerializeField] private EnemyBase _currentTarget;
    
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _cooldown;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private Transform _muzzlePoint;

    [SerializeField] private bool _canAttack;
    
    private float _currentCooldown;
    private bool _isReadyToFire => _currentCooldown >= _cooldown;

    private void Start()
    {
        StartCoroutine(TurretRoutine());
    }

    public IEnumerator TurretRoutine()
    {
        yield return new WaitForSeconds(90);
        Destroy(gameObject);
    }
    
    private void Update()
    {
        UpdateCurrentCooldown();
        UpdateTarget();
        Rotate();
        Fire();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if((_beaconLayer.value &  (1 << other.gameObject.layer)) == 0)
        {
            _canAttack = true;
        }
        
        if ((_targetLayer.value & (1 << other.gameObject.layer)) == 0)
        {
            return;
        }

        EnemyBase enemy = other.GetComponentInParent<EnemyBase>();

        if (enemy == null || _enemies.Contains(enemy))  //
        {
            return;
        }

        _enemies.Add(enemy);
    }
    
    private void OnTriggerExit(Collider other)
    {
        EnemyBase enemy = other.GetComponentInParent<EnemyBase>();

        if (enemy == null)
        {
            return;
        }

        _enemies.Remove(enemy);
    }
    
    private void UpdateTarget()
    {
        _currentTarget = null;
        float closestDistance = float.MaxValue;

        for (int i = _enemies.Count - 1; i >= 0; i--)
        {
            EnemyBase enemy = _enemies[i];

            if (enemy == null || enemy.IsDead)
            {
                _enemies.RemoveAt(i);
                continue;
            }

            Vector3 direction = enemy.transform.position - _muzzlePoint.position;

            float distance = direction.magnitude;

            RaycastHit hit;

            if (Physics.Raycast(_muzzlePoint.position, direction, out hit))
            {
                EnemyBase hitEnemy = hit.collider.GetComponentInParent<EnemyBase>();

                if (hitEnemy != enemy)
                {
                    continue;
                }

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    _currentTarget = enemy;
                }
            }
        }
    }
    
    private void UpdateCurrentCooldown()
    {
        if (_isReadyToFire) return;

        _currentCooldown += Time.deltaTime;
    }

    private void Rotate()
    {
        if (_currentTarget == null)
        {
            _headTransform.Rotate(
                Vector3.up,
                _rotateSpeed * Time.deltaTime,
                Space.World
            );

            return;
        }

        Vector3 lookPosition = _currentTarget.transform.position;
        lookPosition.y = _headTransform.position.y;

        _headTransform.LookAt(lookPosition);
    }

    private void Fire()
    {
        if (_currentTarget == null || !_isReadyToFire || !_canAttack)
        {
            return;
        }
        
        Instantiate(
            _bulletPrefab,
            _muzzlePoint.position,
            _muzzlePoint.rotation
        );

        _currentCooldown = 0f;
    }
}
