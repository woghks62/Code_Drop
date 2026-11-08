using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private InputActionReference _fireAction;

    private void OnEnable() => BindInputEvents();
    private void OnDisable() => UnBindInputEvents();
    
    private void OnMouseLeftButtonDown(InputAction.CallbackContext context)
    {
        Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
    }
    
    private void BindInputEvents()
    {
        _fireAction.action.Enable();
        _fireAction.action.performed += OnMouseLeftButtonDown;
    }

    private void UnBindInputEvents()
    {
        _fireAction.action.Disable();
        _fireAction.action.performed -= OnMouseLeftButtonDown;
    }
}
