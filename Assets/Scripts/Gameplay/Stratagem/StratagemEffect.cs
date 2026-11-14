using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class StratagemEffect : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _code;
    [SerializeField] private float _cooldown;
    [SerializeField] private int _useLimit;
    [SerializeField] private float _waitTime;
    [SerializeField] private Image _icon;
    [SerializeField] private Material _beaconmaterial;
    [SerializeField] private StratagemEffect _effect;
    
    public string Name => _name;
    public string Code => _code;
    public float Cooldown => _cooldown;
    public int UseLimit => _useLimit;
    public float WaitTime => _waitTime;
    public Image Icon => _icon;
    public Material BeaconMaterial => _beaconmaterial;
    public StratagemEffect Effect => _effect;
    
    public abstract void Apply(Vector3 position);

    public void SetData(string name, string code, float cooldown, int useLimit, float waitTime)
    {
        _name = name;
        _code = code;
        _cooldown = cooldown;
        _useLimit = useLimit;
        _waitTime = waitTime;
    }
}
