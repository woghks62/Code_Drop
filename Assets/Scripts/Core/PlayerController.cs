using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController Instance;

    private PlayerMovement _playerMovement;
    private PlayerHealth _playerHealth;
    private PlayerShooter _playerShooter;
    private WeaponChange _playerWeaponChange;
    private PlayerStratagem _playerStratagem;
    
    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerHealth PlayerHealth => _playerHealth;
    public PlayerShooter PlayerShooter => _playerShooter;
    private WeaponChange WeaponChange => _playerWeaponChange;
    public PlayerStratagem PlayerStratagem => _playerStratagem;
    
    private void Awake()
    {
        Instance = this;
        _playerMovement = GetComponent<PlayerMovement>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerShooter = GetComponent<PlayerShooter>();
        _playerWeaponChange = GetComponent<WeaponChange>();
        _playerStratagem = GetComponent<PlayerStratagem>();
    }
}
