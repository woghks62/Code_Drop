using System;
using UnityEngine;
using System.Collections.Generic;

public class GunTableParser : MonoBehaviour
{
    private const int COLUMN_COUNT = 8;
    
    [SerializeField] private TextAsset _gunDataFile;

    [SerializeField] private List<GunStats> _gunStatsList = new();

    private void Awake()
    {
        LoadTable();
    }

    private void LoadTable()
    {
        string[] lines = _gunDataFile.text.Split(
            new char[] { '\r', '\n' }, 
            StringSplitOptions.RemoveEmptyEntries
            );

        for (int i = 3; i < lines.Length; i++)  
        {  
            string[] cells = lines[i].Split(',');
            
            if (cells.Length != COLUMN_COUNT)  
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
}
