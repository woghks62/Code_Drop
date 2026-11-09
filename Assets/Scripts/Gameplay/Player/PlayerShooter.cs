using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    private const int SHOTGUN_BULLET_AMOUNT = 10;
    
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private InputActionReference _fireAction;
    [SerializeField] private InputActionReference _reloadAction;

    private Gun _gun;
    private bool _isShooting;

    private float _time;
    
    [SerializeField] private string _currentGunName;
    [SerializeField] private int _guncapacity; // 현재 탄창
    [SerializeField] private int _spareMagazine;                 // 리로드 가능 횟수
    
    [SerializeField] private bool _isReloading;
    
    private void OnEnable()
    {
        CacheComponents();
        BindInputEvents();    
    }
    private void Update()
    {
        _time += Time.deltaTime;

        if (_isReloading) return;
        
        if (_isShooting && _guncapacity > 0)
        {
            if (_currentGunName == "ShotGun" && _time > _gun.Stats.FramingInterval)
            {
                FireShotGun();
            }
            else if (_time > _gun.Stats.FramingInterval)
            {
                Fire();
            }
        }
    }
    private void OnDisable() => UnBindInputEvents();

    private void Fire()
    {
        Vector3 spredDirection = GetSpredDirection((_firePoint.position - transform.position).normalized, _gun.Stats.BulletSpread);
        Vector3 spread = spredDirection + _firePoint.position;
        Bullet bullet = Instantiate(_bulletPrefab, spread, _firePoint.rotation);
        bullet.Damage = _gun.Stats.Damage;
        _guncapacity--;
        _time = 0f;
    }

    private void FireShotGun()
    {
        for (int i = 0; i < SHOTGUN_BULLET_AMOUNT; i++)
        {
            Vector3 spredDirection = GetSpredDirection((_firePoint.position - transform.position).normalized, _gun.Stats.BulletSpread * 30);
            Vector3 spread = spredDirection + _firePoint.position;
            Bullet bullet = Instantiate(_bulletPrefab, spread, _firePoint.rotation);
            bullet.Damage = _gun.Stats.Damage;    
        }
        _guncapacity--;
        _time = 0f;
    }

    private Vector3 GetSpredDirection(Vector3 forwardDirection, float spreadAngle)
    {
        float spreadX = Random.Range(-spreadAngle, spreadAngle);
        float spreadY = Random.Range(-spreadAngle, spreadAngle);

        Vector3 spreadDirection = Quaternion.Euler(spreadX, spreadY, 0) * forwardDirection;
        return spreadDirection;
    }
    
    private void OnMouseLeftButtonDown(InputAction.CallbackContext context)
    {
        _isShooting = true;
    }

    private void OnMouseLeftButtonUp(InputAction.CallbackContext context)
    {
        _isShooting = false;
    }

    private void OnReloadButtonDown(InputAction.CallbackContext context)
    {
        if (_guncapacity == _gun.Stats.Capacity || _spareMagazine == 0)
        {
            return;
        }

        if (_isReloading)
        {
            return;
        }
        
        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        Debug.Log("장전 시작");
        _isReloading = true;
        yield return new WaitForSeconds(_gun.Stats.ReloadTime);
        
        _guncapacity = _gun.Stats.Capacity;
        _spareMagazine--;
        Debug.Log("장전 완료");
        _isReloading = false;
    }
    
    private void BindInputEvents()
    {
        _fireAction.action.Enable();
        _reloadAction.action.Enable();
        _fireAction.action.performed += OnMouseLeftButtonDown;
        _fireAction.action.canceled += OnMouseLeftButtonUp;
        _reloadAction.action.performed += OnReloadButtonDown;
    }

    private void UnBindInputEvents()
    {
        _fireAction.action.Disable();
        _reloadAction.action.Disable();
        _fireAction.action.performed -= OnMouseLeftButtonDown;
        _fireAction.action.canceled -= OnMouseLeftButtonUp;
        _reloadAction.action.performed -= OnReloadButtonDown;
    }
    
    private void CacheComponents()
    {
        _gun = GetComponentInChildren<Gun>();
        Debug.Log($"장착한 무기 : {_gun.Stats.Name}");
        _guncapacity = _gun.Stats.Capacity;
        _currentGunName = _gun.Stats.Name;
        _spareMagazine = _gun.Stats.SpareMagazine;
    }
}
