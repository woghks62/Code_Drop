using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyStats _enemystats;
    [SerializeField] private int _id;
    
    public EnemyStats EnemyStats { get => _enemystats; set => _enemystats = value; }

    public void Start()
    {
        _id = _enemystats.Id;
    }
}
