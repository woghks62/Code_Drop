using System;
using UnityEngine;
using System.Collections.Generic;

public class TableParser : MonoBehaviour
{
    private const int GUN_COLUMN_COUNT = 8;
    private const int ENEMY_COLUMN_COUNT = 10;
    
    [Header("gunData")]
    [SerializeField] private TextAsset _gunDataFile;
    [SerializeField] private List<GunStats> _gunStatsList = new();

    [Header("EnemyData")] 
    [SerializeField] private TextAsset _enemyDataFile;

    [SerializeField] private List<EnemyStats> _enemyStatsList = new();

    private void Awake()
    {
        LoadGunTable();
        LoadEnemyTable();
    }

    private void LoadGunTable()
    {
        string[] lines = _gunDataFile.text.Split(
            new char[] { '\r', '\n' }, 
            StringSplitOptions.RemoveEmptyEntries
            );

        for (int i = 3; i < lines.Length; i++)  
        {  
            string[] cells = lines[i].Split(',');
            
            if (cells.Length != GUN_COLUMN_COUNT)  
            {  
                Debug.Log($"cells.Length != COLUMN_COUNT");  
                continue;  
            }
            
            int id, damage, capacity, spareMagazine;
            string name;
            float framingInterval, reloadTime, bulletSpread;
            
            if (!int.TryParse(cells[0], out id)
                || !int.TryParse(cells[2], out damage)
                || !float.TryParse(cells[3], out framingInterval)
                || !int.TryParse(cells[4], out capacity)
                || !int.TryParse(cells[5], out spareMagazine)
                || !float.TryParse(cells[6], out reloadTime)
                || !float.TryParse(cells[7], out bulletSpread))
            {
                Debug.LogError("여기도 뭔가 이상하다");
                continue;
            }

            name = cells[1];
            
            _gunStatsList[i-3].SetData(
                id,
                name,
                damage, 
                framingInterval, 
                capacity, 
                spareMagazine, 
                reloadTime, 
                bulletSpread
                );
        }  
    }

    private void LoadEnemyTable()
    {
        string[] lines = _enemyDataFile.text.Split(
            new char[] { '\r', '\n' }, 
            StringSplitOptions.RemoveEmptyEntries
        );

        for (int i = 3; i < lines.Length; i++)  
        {  
            string[] cells = lines[i].Split(',');
            
            if (cells.Length != ENEMY_COLUMN_COUNT)  
            {  
                Debug.Log($"cells.Length != COLUMN_COUNT");  
                continue;  
            }
            
            int id, maxHealth, damage, angleRange;
            string name;
            float moveSpeed, radius, framingInterval, attackRange, projectileSpeed;
            
            if (!int.TryParse(cells[0], out id)
                || !int.TryParse(cells[2], out maxHealth)
                || !float.TryParse(cells[3], out moveSpeed)
                || !int.TryParse(cells[4], out angleRange)
                || !float.TryParse(cells[5], out radius)
                || !float.TryParse(cells[6], out framingInterval)
                || !int.TryParse(cells[7], out damage)
                || !float.TryParse(cells[8], out attackRange)
                || !float.TryParse(cells[9], out projectileSpeed))
            {
                Debug.LogError("여기도 뭔가 이상하다");
                continue;
            }

            name = cells[1];
            
            _enemyStatsList[i-3].SetData(
                id,
                name,
                maxHealth,
                moveSpeed,
                angleRange,
                radius,
                framingInterval,
                damage,
                attackRange,
                projectileSpeed
            );
        }
    }
}
