using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    private const int SHOTGUN_BULLET_AMOUNT = 10;
    
    [SerializeField] private InputActionReference _fireAction;
    [SerializeField] private InputActionReference _reloadAction;
    
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private SniperBullet _sniperbulletPrefab;
    [SerializeField] private Transform _firePoint;
    
    [SerializeField] private bool _isReloading;
    [SerializeField] private bool _hasStratagem;
    private Animator _animator;
    public bool HasStratagem
    {
        get => _hasStratagem;
        set => _hasStratagem = value;
    }
    
    [Header("현재 들고 있는 총")]
    [SerializeField] private int _id;
    [SerializeField] private string _currentGunName;
    [SerializeField] private int _damage;
    [SerializeField] private float _framingInterval;
    [SerializeField] private int _guncapacity; // 현재 탄창
    [SerializeField] private int _spareMagazine;                 // 리로드 가능 횟수
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _bulletSpread;
        
    private Gun _gun;
    private bool _isShooting;
    private float _time;

    public string CurrentGunName => _currentGunName;
    public int GunCapacity  => _guncapacity;
    public int SpareMagazine {get => _spareMagazine; set => _spareMagazine = value;}
    public bool IsReloading =>  _isReloading;
    
    private void OnEnable()
    {
        CacheComponents();
        BindInputEvents();
    }
    private void Update()
    {
        _time += Time.deltaTime;
        
        if (_isReloading) return;
        if (_hasStratagem) return;
        
        if (_isShooting && _guncapacity > 0)
        {
            if (_currentGunName == "ShotGun" && _time > _framingInterval)
            {
                FireShotGun();
            }
            else if (_currentGunName == "Sniper" && _time > _framingInterval)
            {
                FireSniper();
            }
            else if (_time > _framingInterval)
            {
                Fire();
            }
            UIManager.Instance.UseMagazine(_gun, _guncapacity);
        }
    }
    private void OnDisable() => UnBindInputEvents();

    public void StopReloading()
    {
        Debug.Log("재장전 멈춤");
        StopCoroutine("ReloadRoutine");
        _isReloading = false;
        _animator.SetBool("IsReloading", false);
    }

    public void SetCahngeWaeponData(Gun gun, int capacity, int spareMagazine)
    {
        _gun = gun;
        _id = gun.Stats.Id;
        _currentGunName = gun.Stats.Name;
        _damage = gun.Stats.Damage;
        _framingInterval = gun.Stats.FramingInterval;
        _guncapacity = capacity;
        _spareMagazine = spareMagazine;
        _reloadTime = gun.Stats.ReloadTime;
        _bulletSpread = gun.Stats.BulletSpread;
    }
    
    private void Fire()
    {
        _animator.SetBool("IsFiring", true);
        Bullet bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        bullet.Damage = _damage;
        _guncapacity--;
        _time = 0f;
    }

    private void FireShotGun()
    {
        for (int i = 0; i < SHOTGUN_BULLET_AMOUNT; i++)
        {
            float spreadAngle = Random.Range(-_bulletSpread, _bulletSpread);

            Quaternion spreadRotation =
                Quaternion.Euler(0f, spreadAngle, 0f) * _firePoint.rotation;

            Bullet bullet = Instantiate(
                _bulletPrefab,
                _firePoint.position,
                spreadRotation
            );
            bullet.Damage = _damage;    
        }
        _guncapacity--;
        _time = 0f;
    }

    private void FireSniper()
    {
        Bullet bullet = Instantiate(_sniperbulletPrefab, _firePoint.position, _firePoint.rotation);
        bullet.Damage = _damage;
        _guncapacity--;
        _time = 0f;
    }

    private void OnMouseLeftButtonDown(InputAction.CallbackContext context)
    {
        _isShooting = true;
    }

    private void OnMouseLeftButtonUp(InputAction.CallbackContext context)
    {
        _isShooting = false;
        _animator.SetBool("IsFiring", false);
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
        StartCoroutine("ReloadRoutine");
    }

    private IEnumerator ReloadRoutine()
    {
        Debug.Log("장전 시작");
        _animator.SetBool("IsReloading", true);
        _isReloading = true;
        yield return new WaitForSeconds(_reloadTime);
        
        _guncapacity = _gun.Stats.Capacity;
        _spareMagazine--;
        Debug.Log("장전 완료");
        _isReloading = false;
        _animator.SetBool("IsReloading", false);
        UIManager.Instance.ChangeMagazine(_spareMagazine);
        UIManager.Instance.UseMagazine(_gun, _guncapacity);
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
        _animator = GetComponentInChildren<Animator>();
        Debug.Log($"장착한 무기 : {_gun.Stats.Name}");
    }
    
}
