using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StratagemButton : MonoBehaviour
{
    [SerializeField] private StratagemEffect _stratagem;
    [SerializeField] private LoadOut _loadOut;

    [SerializeField] private bool _choice;
    
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _iconText;
    [SerializeField] private TextMeshProUGUI _coolTimeText;
    [SerializeField] private TextMeshProUGUI _useTimeText;
    [SerializeField] private TextMeshProUGUI _waitTimeText;
    
    [SerializeField] private GameObject _ChoiceUI;

    private void Awake()
    {
        _ChoiceUI.SetActive(false);
    }

    private void Start()
    {
        _icon.sprite = _stratagem.Icon;
        _iconText.text = _stratagem.Name;
        _coolTimeText.text = $"Cool Time: {_stratagem.Cooldown}";
        _useTimeText.text = $"Use Limit: {(_stratagem.UseLimit < 0 ? "Unlimited" : _stratagem.UseLimit)}";
        _waitTimeText.text = $"Wait Time: {_stratagem.WaitTime}";
    }

    public void ChoiceStratagem()
    {
        if (!_choice)
        {
            _loadOut.StratagemList.Add(_stratagem);
            _choice = true;
            _ChoiceUI.SetActive(true);
        }
        else
        {
            _loadOut.StratagemList.Remove(_stratagem);
            _choice = false;
            _ChoiceUI.SetActive(false);
        }
        _loadOut.UpdateUI();
    }
}
