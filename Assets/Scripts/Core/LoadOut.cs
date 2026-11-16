using System.Collections.Generic;
using UnityEngine;

public class LoadOut : MonoBehaviour
{
    [SerializeField] private List<Gun> gunlists;
    
    public void ChoiceRifle()
    {
        GameManager.Instance.GameData.ChoiceGun = gunlists[0];
        GameManager.Instance.GameScene();
    }

    public void ChoiceShotGun()
    {
        GameManager.Instance.GameData.ChoiceGun = gunlists[1];
        GameManager.Instance.GameScene();
    }

    public void ChoiceSniper()
    {
        GameManager.Instance.GameData.ChoiceGun = gunlists[2];
        GameManager.Instance.GameScene();
    }
}
