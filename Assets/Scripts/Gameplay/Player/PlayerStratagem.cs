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
    [SerializeField] private InputActionReference _throwAction;
    
    [SerializeField] private Stratagem _currentStratagem;
    public Stratagem CurrentStratagem => _currentStratagem;
    
    [SerializeField] private string _code;
    
    [SerializeField] private List<Stratagem> _stratagemlist;
    
    [SerializeField] private Transform _handTransform;
    
    private PlayerShooter _playerShooter;
    
    public string Code => _code;
    
    private void Awake() => _playerShooter = GetComponent<PlayerShooter>();

    private void Start()
    {
        for (int i = 0; i < _stratagemlist.Count; i++)
        {
            _stratagemlist[i].StratagemEffect = GameManager.Instance.GameData.StratagemEffectList[i];
        }
    }
    private void OnEnable() => BindInputEvents();
    private void OnDisable() => UnBindInputEvents();

    private void HoldStratagem(int index)
    {
        _currentStratagem = _stratagemlist[index];
        _playerShooter.HasStratagem = true;
        _stratagemlist[index].gameObject.transform.SetParent(_handTransform);
        _stratagemlist[index].transform.position = _handTransform.position;
        _currentStratagem.Child.SetActive(true);
        _stratagemlist[index].gameObject.GetComponentInChildren<Rigidbody>().useGravity = false;
        _stratagemlist[index].gameObject.GetComponentInChildren<Rigidbody>().isKinematic = true;
        _currentStratagem.CanExecute = true;
    }

    private void DownStratagem()
    {
        if(_currentStratagem == null) return;
        Debug.Log("Down Stratagem 실행");
        
        _currentStratagem.CanExecute = false;
        _currentStratagem.Child.gameObject.SetActive(false);
        _currentStratagem.transform.SetParent(null);
        _currentStratagem = null;
        _playerShooter.HasStratagem = false;
    }

    private void OnStratagemButtonDown(InputAction.CallbackContext context)
    {
        UIManager.Instance.OpenStratagemPanel();
        StratagemOpen();
    }

    private void OnStratagemButtonUp(InputAction.CallbackContext context)
    {
        UIManager.Instance.CloseStratagemPanel();
        StratagemClose();

        for (int i = 0; i < _stratagemlist.Count; i++)
        {
            if (_code == _stratagemlist[i].StratagemEffect.Code)
            {
                // 해당 스트라타젬 쿨타임이 지나지 않았으면 발동 X
                if (_stratagemlist[i].CoolTime < _stratagemlist[i].StratagemEffect.Cooldown)
                {
                    Debug.Log("쿨타임이 안됐어요");
                    DownStratagem();
                    _code = null;
                    return;
                }
                Debug.Log($"같은 코드가 있어요!! : {_stratagemlist[i]}");

                // 사용가능한 횟수가 0이 아니라면
                if (_stratagemlist[i].StratagemEffect.UseLimit != 0)
                {
                    // 사용 가능한 횟수가 0보다 작다면 (무한 사용 가능)(내가 정한 룰)
                    if (_stratagemlist[i].StratagemEffect.UseLimit < 0)
                    {
                        DownStratagem();
                        HoldStratagem(i);
                    }
                    // 사용 가능한 횟수가 0보다 크다면 (사용 횟수 정해져 있음)
                    else
                    {
                        // 사용한 횟수가 사용 가능한 횟수보다 작으면 실행
                        if (_stratagemlist[i].UseCount < _stratagemlist[i].StratagemEffect.UseLimit)
                        {
                            // _stratagemlist[i].UseCount++; 나중에 던졌을때 거기서 Count 늘리는 방식으로 변경
                            DownStratagem();
                            HoldStratagem(i);

                        }
                        else
                        {
                            Debug.Log("사용 횟수 다 사용했어요");
                        }
                    }
                }
            }
        }

        if (_currentStratagem == null)
        {
            Debug.Log("같은 코드가 없어요");
        }
        
        _code = null;
    }

    private void OnThrowButtonDown(InputAction.CallbackContext context)
    {
        if (!_playerShooter.HasStratagem)
        {
            return;
        }

        Rigidbody _rigidStratagem = _currentStratagem.GetComponentInChildren<Rigidbody>();
        _currentStratagem.transform.SetParent(null);
        _currentStratagem.CoolTime = 0;
        _rigidStratagem.isKinematic = false;
        _rigidStratagem.AddForce(_handTransform.forward * 10, ForceMode.Impulse);
        _rigidStratagem.useGravity = true;
        _playerShooter.HasStratagem = false;
        _currentStratagem = null;
        GameManager.Instance.GameData.UseStratagemCount++;
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
        _throwAction.action.Enable();
        _stratagemAction.action.performed += OnStratagemButtonDown;
        _stratagemAction.action.canceled += OnStratagemButtonUp;
        _throwAction.action.performed += OnThrowButtonDown;
    }

    private void UnBindInputEvents()
    {
        _stratagemAction.action.Disable();
        _upAction.action.Disable();
        _downAction.action.Disable();
        _leftAction.action.Disable();
        _rightAction.action.Disable();
        _throwAction.action.Disable();
        _stratagemAction.action.performed -= OnStratagemButtonDown;
        _stratagemAction.action.canceled -= OnStratagemButtonUp;
        _throwAction.action.performed -= OnThrowButtonDown;
    }
}
