using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveInputAction;
    [SerializeField] private float _moveSpeed;

    [SerializeField] private Vector2 _moveInput;
    private Animator _animator;
    private Rigidbody _rigidbody;
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody =  GetComponent<Rigidbody>();
    }

    private void OnEnable() => BindInputEvents();

    private void Update()
    {
        bool isMoving = _moveInput != Vector2.zero;
        _animator.SetBool("IsMoving", isMoving);
    }

    private void OnDisable() => UnBindInputEvents();

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
    
    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(_moveInput.x, 0f, _moveInput.y);
        direction = Vector3.ClampMagnitude(direction, 1f);

        Vector3 velocity = direction * _moveSpeed;
        velocity.y = _rigidbody.linearVelocity.y;

        _rigidbody.linearVelocity = velocity;
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
