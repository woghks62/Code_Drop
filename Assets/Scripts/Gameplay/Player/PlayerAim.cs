using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayerMask;
    
    [SerializeField] private InputActionReference _aimInput;
    [SerializeField] private Vector2 _aimPoint;
    [SerializeField] private Image _aimTarget;

    private Camera _camera;

    private void Awake() => CacheComponents();
    private void OnEnable() => BindInputEvents();
    
    private void Update()
    {
        SetAim();
    }
    
    private void OnDisable() => UnBindInputEvents();


    private void SetAim()
    {
        _aimPoint = _aimInput.action.ReadValue<Vector2>();
        Debug.Log("Aim Point: " + _aimPoint);
        _aimTarget.transform.position = _aimPoint;

        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(_aimPoint);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _targetLayerMask))
        {
            transform.LookAt(new Vector3(
                hit.point.x,
                transform.position.y,
                hit.point.z
                ));
        }

    }


    private void BindInputEvents()
    {
        _aimInput.action.Enable();
    }
    
    private void UnBindInputEvents()
    {
        _aimInput.action.Disable();
    }
    
    private void CacheComponents()
    {
        _camera = Camera.main;
    }
        
    
}
