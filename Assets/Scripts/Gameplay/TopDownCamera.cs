using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Quaternion _rotation;
    
    [SerializeField] private float _speed;

    private void Start()
    {
        transform.rotation = _rotation;
    }

    private void LateUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(
            transform.position, 
            _playerTransform.position + _offset, 
            Time.deltaTime * _speed);
    }
}
