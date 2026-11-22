using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadOut : MonoBehaviour
{
    [SerializeField] private List<Gun> gunlists;
    
    [SerializeField] private GameObject _weaponUI;
    [SerializeField] private GameObject _stratagemUI;
    
    [SerializeField] private List<StratagemEffect> _stratagemlists;
    public List<StratagemEffect> StratagemList {get => _stratagemlists; set =>_stratagemlists = value;}

    [SerializeField] private float _panelTime;
    [SerializeField] private float _nextSceneTime;
    
    [SerializeField] private GameObject _rifleChoice;
    [SerializeField] private GameObject _shotgunChoice;
    [SerializeField] private GameObject _sniperChoice;
    [SerializeField] private GameObject _gunNextButton;
    [SerializeField] private GameObject _backButton;
    [SerializeField] private GameObject _gameStartButton;
    
    [SerializeField] private TextMeshProUGUI _weaponStepText;
    [SerializeField] private TextMeshProUGUI _stratagemStepText;

    [SerializeField] private GameObject _choiceCountObject;
    [SerializeField] private TextMeshProUGUI _choiceCountText;
    
    private void Awake()
    {
        GameManager.Instance.ResetData();
        
        _gameStartButton.GetComponent<Image>().color = new Color32(169, 176, 179, 255);
        
        _weaponUI.gameObject.SetActive(true);
        _stratagemUI.gameObject.SetActive(false);
        _rifleChoice.SetActive(false);
        _shotgunChoice.SetActive(false);
        _sniperChoice.SetActive(false);
        
        _gunNextButton.SetActive(true);
        _backButton.SetActive(false);
        _gameStartButton.SetActive(false);
        
        _choiceCountObject.SetActive(false);
        _choiceCountText.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        _choiceCountText.text = $"{_stratagemlists.Count} / 2";
        if (_stratagemlists.Count != 2)
        {
            _gameStartButton.GetComponent<Image>().color = new Color32(169, 176, 179, 255);
        }
        else
        {
            _gameStartButton.GetComponent<Image>().color = new Color32(217, 180, 82, 255);
        }
    }
    
    public void ChoiceRifle()
    {
        GameManager.Instance.GameData.ChoiceGun = gunlists[0];
        //ChoiceWeaponEnd();
        _rifleChoice.SetActive(true);
        _shotgunChoice.SetActive(false);
        _sniperChoice.SetActive(false);
    }

    public void ChoiceShotGun()
    {
        GameManager.Instance.GameData.ChoiceGun = gunlists[1];
        //ChoiceWeaponEnd();
        _rifleChoice.SetActive(false);
        _shotgunChoice.SetActive(true);
        _sniperChoice.SetActive(false);
    }

    public void ChoiceSniper()
    {
        GameManager.Instance.GameData.ChoiceGun = gunlists[2];
        //ChoiceWeaponEnd();
        _rifleChoice.SetActive(false);
        _shotgunChoice.SetActive(false);
        _sniperChoice.SetActive(true);
    }

    public void ChoiceStratagemEnd()
    {
        if (_stratagemlists.Count != 2)
        {
            return;
        }
        
        GameManager.Instance.GameData.StratagemEffectList =
            new List<StratagemEffect>(_stratagemlists);
        //StartCoroutine(StartGameScene());
        GameManager.Instance.GameScene();
    }
    
    private IEnumerator StartGameScene()
    {
        yield return new WaitForSeconds(_nextSceneTime);
        GameManager.Instance.GameScene();
    }

    public void ChoiceWeaponEnd()
    {
        if (GameManager.Instance.GameData.ChoiceGun == null)
        {
            return;
        }
        
        _weaponUI.gameObject.SetActive(false);
        _stratagemUI.gameObject.SetActive(true);
        _choiceCountObject.SetActive(true);
        _choiceCountText.gameObject.SetActive(true);
        
        _gunNextButton.SetActive(false);
        _backButton.SetActive(true);
        _gameStartButton.SetActive(true);
        
        _weaponStepText.color = new Color32(169, 176, 179, 255);
        _stratagemStepText.color = new Color32(217, 180, 82, 255);
    }

    public void BackButton()
    {
        _weaponUI.SetActive(true);
        _stratagemUI.SetActive(false);
        _choiceCountObject.SetActive(false);
        _choiceCountText.gameObject.SetActive(false);

        _gunNextButton.SetActive(true);
        _backButton.SetActive(false);
        _gameStartButton.SetActive(false);

        _weaponStepText.color = new Color32(217, 180, 82, 255);
        _stratagemStepText.color = new Color32(169, 176, 179, 255);
    }
}
