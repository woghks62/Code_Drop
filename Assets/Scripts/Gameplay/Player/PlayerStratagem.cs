using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStratagem : MonoBehaviour
{
    [SerializeField] private InputActionReference _stratagemAction;
    [SerializeField] private InputActionReference _upAction;
    [SerializeField] private InputActionReference _downAction;
    [SerializeField] private InputActionReference _leftAction;
    [SerializeField] private InputActionReference _rightAction;
    
    [SerializeField] private Stratagem _currentStratagem;
    public Stratagem CurrentStratagem => _currentStratagem;
    
    [SerializeField] private string _code;

    [SerializeField] private List<Stratagem> _stratagemlist;
    
    [SerializeField] private Transform _handTransform;
    
    private PlayerShooter _playerShooter;
    private void Awake() => _playerShooter = GetComponent<PlayerShooter>();
    private void OnEnable() => BindInputEvents();
    private void OnDisable() => UnBindInputEvents();

    private void OnStratagemButtonDown(InputAction.CallbackContext context)
    {
        StratagemOpen();
    }

    private void OnStratagemButtonUp(InputAction.CallbackContext context)
    {
        StratagemClose();

        for (int i = 0; i < _stratagemlist.Count; i++)
        {
            if (_code == _stratagemlist[i].StratagemEffect.Code)
            {
                Debug.Log($"같은 코드가 있어요!! : {_stratagemlist[i]}");
                _currentStratagem = _stratagemlist[i];
                _playerShooter.HasStratagem = true;
                _stratagemlist[i].gameObject.transform.SetParent(_handTransform);
                _stratagemlist[i].transform.position = _handTransform.position;
                _stratagemlist[i].gameObject.SetActive(true);
                _stratagemlist[i].gameObject.GetComponent<Rigidbody>().useGravity =  false;
            }
        }
        
        _code = null;
    }

    private void OnUpButtonDown(InputAction.CallbackContext context)
    {
        _code += 1;
    }

    private void OnDownButtonUp(InputAction.CallbackContext context)
    {
        _code += 2;
    }

    private void OnLeftButtonDown(InputAction.CallbackContext context)
    {
        _code += 3;
    }

    private void OnRightButtonDown(InputAction.CallbackContext context)
    {
        _code += 4;
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
