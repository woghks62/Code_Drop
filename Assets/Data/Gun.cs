using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunStats _stats;
    [SerializeField] private int _id;

    public GunStats Stats => _stats;
    
    private void Start()
    {
        _id = _stats.Id;
    }
}
