using UnityEngine;

[CreateAssetMenu(fileName = "GunStats", menuName = "Data/GunStats")]
public class GunStats : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private int _damage;
    [SerializeField] private float _framingInterval;
    [SerializeField] private int _capacity;
    [SerializeField] private int _spareMagazine;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _bulletSpread;
    
    public int Id => _id;
    public string Name => _name;
    public int Damage => _damage;
    public float FramingInterval => _framingInterval;
    public int Capacity => _capacity;
    public int SpareMagazine => _spareMagazine;
    public float ReloadTime => _reloadTime;
    public float BulletSpread => _bulletSpread;

    public void SetData(
        int id, string name, int damage, float framingInterval,
        int capacity, int spareMagazine, float reloadTime, float bulletSpread)
    {
        _id = id;
        _name = name;
        _damage = damage;
        _framingInterval = framingInterval;
        _capacity = capacity;
        _spareMagazine = spareMagazine;
        _reloadTime = reloadTime;
        _bulletSpread = bulletSpread;
    }
}
