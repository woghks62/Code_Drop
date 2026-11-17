using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
public class GameData : ScriptableObject
{
    [SerializeField] private Gun _choiceGun;
    [SerializeField] private Gun _pistol;
    
    [SerializeField] private List<StratagemEffect> _stratagemEffectlists;

    [SerializeField] private float _playTime;
    [SerializeField] private int _killCount;
    [SerializeField] private int _useStratagemCount;
    [SerializeField] private bool _isCallEscape;
    
    public List<StratagemEffect> StratagemEffectList {get => _stratagemEffectlists; set => _stratagemEffectlists = value; }
    public Gun ChoiceGun { get => _choiceGun; set => _choiceGun = value; }
    public Gun Pistol { get => _pistol; set => _pistol = value; }
    public float PlayTime { get => _playTime; set => _playTime = value; }
    public int KillCount { get => _killCount; set => _killCount = value; }
    public int UseStratagemCount { get => _useStratagemCount; set => _useStratagemCount = value; }
    public bool IsCallEscape { get => _isCallEscape; set => _isCallEscape = value; }
}
