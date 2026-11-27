using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance {get; private set;}
    
    [Header("적 죽이기 미션 관련")]
    [SerializeField] private int _enemyGoalCount;
    [SerializeField] private int _currentKillCount;
    
    [Header("탈출 비콘 호출하기 관련")]
    [SerializeField] private GameObject _escapeBeacon;
    [SerializeField] private Transform _escapeBeaconPosition;
    [SerializeField] private bool _isEscape;
    
    [SerializeField] EnemyBase[]  _enemies;
    
    [Header("미션 UI 관련")]
    [SerializeField] private MissionUI _missionUIPrefab;
    [SerializeField] private Transform _missionGrid;

    private MissionUI _currentMissionUI;
    
    private void Awake() => Instance = this;
    
    private void Start()
    {
        SoundManager.Instance?.BGMPlay(BGMType.Game);
        GameManager.Instance.GameData.IsCallEscape = false;
        
        _currentMissionUI = Instantiate(_missionUIPrefab, _missionGrid);
        _currentMissionUI.SetMission("적 10마리를 처치하세요");
        _currentMissionUI.SetCount(_currentKillCount, _enemyGoalCount);
        
        UpdateText();
    }
    

    private void EscapeRoutine()
    {
        _isEscape = true;
        Debug.Log("임무 완료 탈출을 준비하세요");
        
        Instantiate(_escapeBeacon, _escapeBeaconPosition);
        
        _currentMissionUI = Instantiate(_missionUIPrefab, _missionGrid);
        _currentMissionUI.SetMission("탈출 비콘을 호출하세요");
    }

    public void KillEnemy()
    {
        if (_currentKillCount >= _enemyGoalCount)
        {
            return;
        }
        
        _currentKillCount++;
        UpdateText();
        
        if (_currentKillCount >= _enemyGoalCount)
        {
            _currentMissionUI.Complete();
            EscapeRoutine();
        }
    }

    public void EscapeCall()
    {
        _currentMissionUI.Complete();

        _currentMissionUI = Instantiate(_missionUIPrefab, _missionGrid);
        _currentMissionUI.SetMission("헬기가 도착할 때까지 버티세요");
    }

    public void Arrive()
    {
        _currentMissionUI.Complete();
        _currentMissionUI = Instantiate(_missionUIPrefab, _missionGrid);
        _currentMissionUI.SetMission("헬기가 도착했습니다 탈출하세요");
    }

    public void Escape()
    {
        _currentMissionUI.Complete();
    }

    private void UpdateText()
    {
        _currentMissionUI.SetCount(_currentKillCount, _enemyGoalCount);
    }
}
