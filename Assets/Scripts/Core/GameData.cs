using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
public class GameData : ScriptableObject
{
    [SerializeField] private Gun _choiceGun;
    [SerializeField] private Gun _pistol;
    
    [SerializeField] private List<StratagemEffect> _stratagemEffectlists;
    public List<StratagemEffect> StratagemEffectList {get => _stratagemEffectlists; set => _stratagemEffectlists = value; }

    public Gun ChoiceGun
    {
        get => _choiceGun;
        set => _choiceGun = value;
    }

    public Gun Pistol
    {
        get => _pistol;
        set => _pistol = value;
    }
}
