using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunStats _stats;
    [SerializeField] private int _id;

    public GunStats Stats {get => _stats; set => _stats = value; }
    
    private void Start()
    {
        _id = _stats.Id;
    }
}
