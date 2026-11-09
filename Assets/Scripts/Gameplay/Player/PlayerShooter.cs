using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    private const int SHOTGUN_BULLET_AMOUNT = 10;
    
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private InputActionReference _fireAction;

    private Gun _gun;
    [SerializeField] private bool _isShooting;

    private float _time;
    [SerializeField] private int _guncapacity; // 현재 탄창

    [SerializeField] private string _currentGunName;
    
    private void Awake() => CacheComponents();
    private void OnEnable() => BindInputEvents();
    private void Update()
    {
        _time += Time.deltaTime;
        Debug.Log($"Time : {_time}");
        
        if (_isShooting && _guncapacity > 0)
        {
            if (_currentGunName == "ShotGun" && _time > _gun.Stats.FramingInterval)
            {
                Debug.Log("shotgun 실행");
                ShootShotGun();
            }
            else if (_time > _gun.Stats.FramingInterval)
            {
                Bullet bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
                bullet.Damage = _gun.Stats.Damage;
                _guncapacity--;
                _time = 0f;
            }
        }
    }
    private void OnDisable() => UnBindInputEvents();

    private void ShootShotGun()
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
    
    private void BindInputEvents()
    {
        _fireAction.action.Enable();
        _fireAction.action.performed += OnMouseLeftButtonDown;
        _fireAction.action.canceled += OnMouseLeftButtonUp;
    }

    private void UnBindInputEvents()
    {
        _fireAction.action.Disable();
        _fireAction.action.performed -= OnMouseLeftButtonDown;
        _fireAction.action.canceled -= OnMouseLeftButtonUp;
    }
    
    private void CacheComponents()
    {
        _gun = GetComponentInChildren<Gun>();
        Debug.Log($"장착한 무기 : {_gun.Stats.Name}");
        _guncapacity = _gun.Stats.Capacity;
        _currentGunName = _gun.Stats.Name;
    }
}
