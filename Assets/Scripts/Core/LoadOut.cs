using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadOut : MonoBehaviour
{
    [SerializeField] private List<Gun> gunlists;
    
    [SerializeField] private GameObject _weaponUI;
    [SerializeField] private GameObject _stratagemUI;
    
    [SerializeField] private List<StratagemEffect> _stratagemlists;
    public List<StratagemEffect> StratagemList {get => _stratagemlists; set =>_stratagemlists = value;}

    [SerializeField] private GameObject _choicePanel;
    [SerializeField] private TextMeshProUGUI _choiceText;
    [SerializeField] private float _panelTime;
    [SerializeField] private float _nextSceneTime;
    
    private void Awake()
    {
        _weaponUI.gameObject.SetActive(true);
        _stratagemUI.gameObject.SetActive(false);
        _choicePanel.SetActive(false);
    }
    
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
        _choicePanel.SetActive(true);
        if (_stratagemlists.Count < 2)
        {
            _choiceText.text = "2개를 선택해 주세요.";
            StartCoroutine(PanelRoutine());
            return;
        }
        else if (_stratagemlists.Count > 2)
        {
            _choiceText.text = "최대 2개까지 \n선택할 수 있습니다.";
            StartCoroutine(PanelRoutine());
        }
        else
        {
            _choiceText.text = "선택이 완료되었습니다.";
            StartCoroutine(PanelRoutine());
        }
        
        GameManager.Instance.GameData.StratagemEffectList =
            new List<StratagemEffect>(_stratagemlists);
        StartCoroutine(StartGameScene());
    }

    private IEnumerator PanelRoutine()
    {
        yield return new WaitForSecondsRealtime(_panelTime);
        _choicePanel.SetActive(false);
    }
    
    private IEnumerator StartGameScene()
    {
        yield return new WaitForSeconds(_nextSceneTime);
        GameManager.Instance.GameScene();
    }

    private void ChoiceWeaponEnd()
    {
        _weaponUI.gameObject.SetActive(false);
        _stratagemUI.gameObject.SetActive(true);
    }
}
