using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stratagem : MonoBehaviour
{
    [SerializeField] private InputActionReference _stratagemAction;
    [SerializeField] private InputActionReference _upAction;
    [SerializeField] private InputActionReference _downAction;
    [SerializeField] private InputActionReference _leftAction;
    [SerializeField] private InputActionReference _rightAction;
    
    [SerializeField] private List<String> _stratagemslist;
    
    private PlayerShooter _playerShooter;
    private void Awake() => _playerShooter = GetComponent<PlayerShooter>();
    private void OnEnable() => BindInputEvents();
    private void OnDisable() => UnBindInputEvents();

    private void OnStratagemButtonDown(InputAction.CallbackContext context)
    {
        _playerShooter.IsStratagem = true;
        StratagemOpen();
        Debug.Log(_playerShooter.IsStratagem);
    }

    private void OnStratagemButtonUp(InputAction.CallbackContext context)
    {
        _playerShooter.IsStratagem = false;
        StratagemClose();
        
        Debug.Log(_playerShooter.IsStratagem);

        for (int i = 0; i < _stratagemslist.Count; i++)
        {
            Debug.Log(_stratagemslist[i]);    
        }
        _stratagemslist.Clear();
    }

    private void OnUpButtonDown(InputAction.CallbackContext context)
    {
        _stratagemslist.Add("up");
    }

    private void OnDownButtonUp(InputAction.CallbackContext context)
    {
        _stratagemslist.Add("down");
    }

    private void OnLeftButtonDown(InputAction.CallbackContext context)
    {
        _stratagemslist.Add("left");
    }

    private void OnRightButtonDown(InputAction.CallbackContext context)
    {
        _stratagemslist.Add("right");
    }

    private void StratagemOpen()
    {
        _upAction.action.performed += OnUpButtonDown;
        _downAction.action.performed += OnDownButtonUp;
        _leftAction.action.performed += OnLeftButtonDown;
        _rightAction.action.performed += OnRightButtonDown;
    }

    private void StratagemClose()
    {
        _upAction.action.performed -= OnUpButtonDown;
        _downAction.action.performed -= OnDownButtonUp;
        _leftAction.action.performed -= OnLeftButtonDown;
        _rightAction.action.performed -= OnRightButtonDown;
    }

    private void BindInputEvents()
    {
        _stratagemAction.action.Enable();
        _upAction.action.Enable();
        _downAction.action.Enable();
        _leftAction.action.Enable();
        _rightAction.action.Enable();
        _stratagemAction.action.performed += OnStratagemButtonDown;
        _stratagemAction.action.canceled += OnStratagemButtonUp;
    }

    private void UnBindInputEvents()
    {
        _stratagemAction.action.Disable();
        _upAction.action.Disable();
        _downAction.action.Disable();
        _leftAction.action.Disable();
        _rightAction.action.Disable();
        _stratagemAction.action.performed -= OnStratagemButtonDown;
        _stratagemAction.action.canceled -= OnStratagemButtonUp;
    }
}
