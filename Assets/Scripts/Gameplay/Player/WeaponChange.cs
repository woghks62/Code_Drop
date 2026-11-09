using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponChange : MonoBehaviour
{
    [SerializeField] private InputActionReference _weapon1Action;
    [SerializeField] private InputActionReference _weapon2Action;
    
    [SerializeField] private List<Gun> _gunList = new();
    
    [SerializeField] private PlayerShooter _playerShooter;


    private void OnEnable()
    {
        BindInputEvents();
    }

    private void OnDisable()
    {
        UnBindInputEvents();
    }

    private void OnChangeWeapon1Button(InputAction.CallbackContext context)
    {
        _playerShooter.ChangeWeapon(_gunList[0]);
    }

    private void OnChangeWeapon2Button(InputAction.CallbackContext context)
    {
        _playerShooter.ChangeWeapon(_gunList[1]);
    }

    public void Save()
    {
        
    }

    public void Load()
    {
        
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
        
    }
}
