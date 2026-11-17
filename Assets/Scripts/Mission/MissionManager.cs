using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance {get; private set;}
    
    [SerializeField] private int _enemyGoalCount;
    [SerializeField] private int _currentKillCount;
    
    [SerializeField] private GameObject _escapeBeacon;
    [SerializeField] private bool _isEscape;
    
    [SerializeField] EnemyBase[]  _enemies;
    private void Awake() => Instance = this;
    
    private void Start()
    {
        UpdateText();
    }

    private void Update()
    {
        if (_isEscape)
        {
            return;
        }
        
        if (_currentKillCount >= _enemyGoalCount)
        {
            UIManager.Instance.CheckImage.enabled = true;
            EscapeRoutine();
        }
    }

    private void EscapeRoutine()
    {
        _isEscape = true;
        Debug.Log("임무 완료 탈출을 준비하세요");
        
        Instantiate(_escapeBeacon, _escapeBeacon.transform.position, _escapeBeacon.transform.rotation);
    }

    public void KillEnemy()
    {
        _currentKillCount++;
        UpdateText();
    }

    private void UpdateText()
    {
        UIManager.Instance.EnemyCountText.text = $"{_currentKillCount} / {_enemyGoalCount}";
    }
}
