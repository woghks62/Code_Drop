using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private const int MAX_HEALTH = 100;
    
    [SerializeField] private int _currentHealth;
    [SerializeField] private Image _healthBar;

    private void Start()
    {
        _currentHealth = MAX_HEALTH;
    }

    private void Update()
    {
        _healthBar.fillAmount = (float)_currentHealth / MAX_HEALTH;
    }
    
    public void TakeDamage(int damage)
    {
        _currentHealth = _currentHealth - damage < 0 ? 0 : _currentHealth - damage;
        Debug.Log($"남은 피: {_currentHealth}");
        
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        _currentHealth = _currentHealth + amount > MAX_HEALTH ? MAX_HEALTH : _currentHealth + amount;
        Debug.Log($"현재 피: {_currentHealth}");
    }
    
    private void Die()
    {
        Debug.Log("Die 실행");
        UIManager.Instance.MissionFail();
    }
}
