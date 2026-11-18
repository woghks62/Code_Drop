using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponChange : MonoBehaviour
{
    [SerializeField] private InputActionReference _weapon1Action;
    [SerializeField] private InputActionReference _weapon2Action;
    
    [SerializeField] private List<Gun> _gunList = new();
    
    [SerializeField] private PlayerShooter _playerShooter;

    private int _weapon1Capacity;
    private int _weapon1SpareMagazine;
    
    private int _weapon2Capacity;
    private int _weapon2SpareMagazine;

    [SerializeField] private bool _isPistol;

    private void Awake()
    {
        SetGunData();
        CacheComponents();
        InitWeapon();
    }
    private void OnEnable() => BindInputEvents();
    private void OnDisable() => UnBindInputEvents();

    private int tmp;

    private void SetGunData()
    {
        List<Gun> gunDatalists = GameManager.Instance.SetGameData();
        _gunList.Add(gunDatalists[0]);
        _gunList.Add(gunDatalists[1]);
    }

    private void OnChangeWeapon1Button(InputAction.CallbackContext context)
    {
        if(!_isPistol) return;
        if (_playerShooter.IsReloading)
        {
            _playerShooter.StopReloading();
        }
        
        _weapon2Capacity = _playerShooter.GunCapacity;
        _weapon2SpareMagazine = _playerShooter.SpareMagazine;
        ChangeWeapon(_gunList[0], _weapon1Capacity, _weapon1SpareMagazine);
        _isPistol =  false;
    }

    private void OnChangeWeapon2Button(InputAction.CallbackContext context)
    {
        if(_isPistol) return;
        
        if (_playerShooter.IsReloading)
        {
            _playerShooter.StopReloading();
        }
        
        _weapon1Capacity = _playerShooter.GunCapacity;
        _weapon1SpareMagazine = _playerShooter.SpareMagazine;
        ChangeWeapon(_gunList[1], _weapon2Capacity, _weapon2SpareMagazine);
        _isPistol =  true;
    }

    private void InitWeapon()
    {
        _playerShooter.SetCahngeWaeponData(_gunList[1], _weapon2Capacity, _weapon2SpareMagazine);
        _isPistol = true;
    }

    public void ChangeWeapon(Gun gun, int capacity, int spareMagazine)
    {
        _playerShooter.SetCahngeWaeponData(gun, capacity, spareMagazine);
        UIManager.Instance.ChangeGunImage(gun.Stats.GunSprite);
        UIManager.Instance.ReloadMagazine(spareMagazine);
        UIManager.Instance.UseMagazine(gun, capacity);
    }
    
    private void BindInputEvents()
    {
        _weapon1Action.action.Enable();
        _weapon2Action.action.Enable();
        _weapon1Action.action.performed += OnChangeWeapon1Button;
        _weapon2Action.action.performed += OnChangeWeapon2Button;
    }

    private void UnBindInputEvents()
    {
        _weapon1Action.action.Disable();
        _weapon2Action.action.Disable();
        _weapon1Action.action.performed -= OnChangeWeapon1Button;
        _weapon2Action.action.performed -= OnChangeWeapon2Button;
    }

    private void CacheComponents()
    {
        _weapon1Capacity = _gunList[0].Stats.Capacity;
        _weapon1SpareMagazine = _gunList[0].Stats.SpareMagazine;
        
        _weapon2Capacity = _gunList[1].Stats.Capacity;
        _weapon2SpareMagazine = _gunList[1].Stats.SpareMagazine;
    }
}
