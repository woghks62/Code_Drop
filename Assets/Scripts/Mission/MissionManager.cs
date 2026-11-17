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

    //[SerializeField] private TextMeshProUGUI _missonText;
    
    
    [SerializeField] private GameObject _escapeObject;
    [SerializeField] private bool _isEscape;
    
    [SerializeField] EnemyBase[]  _enemies;

    [Header("처치 미션")]
    [SerializeField] private TextMeshProUGUI _enemyCountText;
    [SerializeField] private Image _checkImage;
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
            _checkImage.enabled = true;
            StartCoroutine(EscapeRoutine());
        }
    }

    private IEnumerator EscapeRoutine()
    {
        _isEscape = true;
        Debug.Log("임무 완료 탈출을 준비하세요");
        yield return new WaitForSeconds(60);
        
        Instantiate(_escapeObject, _escapeObject.transform.position, _escapeObject.transform.rotation);
    }

    public void KillEnemy()
    {
        _currentKillCount++;
        UpdateText();
    }

    private void UpdateText()
    {
        _enemyCountText.text = $"{_currentKillCount} / {_enemyGoalCount}";
    }
}
