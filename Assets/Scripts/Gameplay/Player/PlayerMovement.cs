using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveInputAction;
    [SerializeField] private float _moveSpeed;

    [SerializeField] private Vector2 _moveInput;

    private void OnEnable() => BindInputEvents();

    private void Update()
    {
        Move();
    }

    private void OnDisable() => UnBindInputEvents();

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
    
    private void Move()
    {
        Vector3 direction = new Vector3(_moveInput.x, 0f, _moveInput.y);

        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    }


    private void BindInputEvents()
    {
        _moveInputAction.action.performed += OnMove;
        _moveInputAction.action.canceled += OnMove;
        _moveInputAction.action.Enable();
    }

    private void UnBindInputEvents()
    {
        _moveInputAction.action.performed -= OnMove;
        _moveInputAction.action.canceled -= OnMove;
        _moveInputAction.action.Disable();

        _moveInput = Vector2.zero;
    }
}
