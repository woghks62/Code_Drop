using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StratagemUI : MonoBehaviour
{
    [SerializeField] private GameObject _stratagemPanel;
    
    [SerializeField] private Stratagem _stratagem;
    [SerializeField] private Image _stratagemIcon;
    [SerializeField] private Image[] _stratagemCodelists;
    [SerializeField] private TextMeshProUGUI _coolTime;
    [SerializeField] private TextMeshProUGUI _remaining;
    
    [SerializeField] private PlayerStratagem _playerStratagem;
    
    private float time;
    private string text;
    private bool _setImage;
    
    private void Update()
    {
        StratagemPanelUI();
        
        string inputCode = _playerStratagem.Code;

        if (inputCode == null)
        {
            inputCode = "";
        }

        UpdateCodeColors(inputCode);
    }
    
    private void UpdateCodeColors(string inputCode)
    {
        string targetCode = _stratagem.StratagemEffect.Code;

        bool isMatching = true;

        if (inputCode.Length > targetCode.Length)
        {
            isMatching = false;
        }
        else
        {
            for (int i = 0; i < inputCode.Length; i++)
            {
                if (inputCode[i] != targetCode[i])
                {
                    isMatching = false;
                    break;
                }
            }
        }

        for (int i = 0; i < _stratagemCodelists.Length; i++)
        {
            if (isMatching && i < inputCode.Length)
            {
                _stratagemCodelists[i].color = Color.green;
            }
            else
            {
                _stratagemCodelists[i].color = Color.white;
            }
        }
    }

    private void StratagemImage()
    {
        _stratagemIcon.sprite = _stratagem.StratagemEffect.Icon;
        
        for (int i = 0; i < _stratagem.StratagemEffect.Code.Length; i++)
        {
            _stratagemCodelists[i].gameObject.SetActive(true);
            if (_stratagem.StratagemEffect.Code[i].ToString() == "1")
            {
                _stratagemCodelists[i].sprite = UIManager.Instance.UpArrowSprite;
            }
            else if (_stratagem.StratagemEffect.Code[i].ToString() == "2")
            {
                _stratagemCodelists[i].sprite = UIManager.Instance.DownArrowSprite;
            }
            else if (_stratagem.StratagemEffect.Code[i].ToString() == "3")
            {
                _stratagemCodelists[i].sprite = UIManager.Instance.LeftArrowSprite;
            }
            else if(_stratagem.StratagemEffect.Code[i].ToString() == "4")
            {
                _stratagemCodelists[i].sprite = UIManager.Instance.RightArrowSprite;
            }
        }
    }
    
    private void StratagemPanelUI()
    {
        if (!_setImage)
        {
            StratagemImage();   
            _setImage = true;
        }
        
        if (_stratagemPanel == null) return;
        
        time = _stratagem.StratagemEffect.Cooldown - _stratagem.CoolTime < 0 
            ? 0 : _stratagem.StratagemEffect.Cooldown - _stratagem.CoolTime;

        if (_stratagem.StratagemEffect.UseLimit - _stratagem.UseCount < 0)
        {
            text = "Unlimited";
        }
        else
        {
            text = (_stratagem.StratagemEffect.UseLimit - _stratagem.UseCount).ToString();
        }
            
        _coolTime.text = $"CoolTime : {time:F2}";
        _remaining.text = $"Remaining : {text}";

    }
}
