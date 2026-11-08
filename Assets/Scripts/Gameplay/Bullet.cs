using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    
    private Vector3 _moveDirection;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }
    
    private void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime *  _speed);
    }
    
    
}
