using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
public class GameData : ScriptableObject
{
    [SerializeField] private Gun _choiceGun;
    [SerializeField] private Gun _pistol;

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
