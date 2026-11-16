using System.Collections.Generic;
using UnityEngine;

public class LoadOut : MonoBehaviour
{
    [SerializeField] private List<Gun> gunlists;
    
    [SerializeField] private GameObject _weaponUI;
    [SerializeField] private GameObject _stratagemUI;
    
    [SerializeField] private List<StratagemEffect> _stratagemlists;
    public List<StratagemEffect> StratagemList {get => _stratagemlists; set =>_stratagemlists = value;}
    
    public void ChoiceRifle()
    {
        GameManager.Instance.GameData.ChoiceGun = gunlists[0];
        ChoiceWeaponEnd();
    }

    public void ChoiceShotGun()
    {
        GameManager.Instance.GameData.ChoiceGun = gunlists[1];
        ChoiceWeaponEnd();
    }

    public void ChoiceSniper()
    {
        GameManager.Instance.GameData.ChoiceGun = gunlists[2];
        ChoiceWeaponEnd();
    }

    public void ChoiceStratagemEnd()
    {
        if (_stratagemlists.Count != 2)
        {
            Debug.Log("2개를 고르세요");
            return;
        }
        
        GameManager.Instance.GameData.StratagemEffectList =
            new List<StratagemEffect>(_stratagemlists);
        GameManager.Instance.GameScene();
    }

    private void ChoiceWeaponEnd()
    {
        _weaponUI.gameObject.SetActive(false);
        _stratagemUI.gameObject.SetActive(true);
    }
}
