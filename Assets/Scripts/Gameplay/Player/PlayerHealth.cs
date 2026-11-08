using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private const int MAX_HEALTH = 100;
    
    [SerializeField] private int _currentHealth;

    private void Start()
    {
        _currentHealth = MAX_HEALTH;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TakeDamage(30);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Heal(15);
        }
    }
    
    public void TakeDamage(int damage)
    {
        _currentHealth = _currentHealth - damage < 0 ? 0 : _currentHealth - damage;
        Debug.Log($"TakeDamage: {_currentHealth}");
        
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        _currentHealth = _currentHealth + amount > MAX_HEALTH ? MAX_HEALTH : _currentHealth + amount;
        Debug.Log($"Heal: {_currentHealth}");
    }
    
    private void Die()
    {
        Debug.Log("Die 실행");
    }
}
