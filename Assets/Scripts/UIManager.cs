using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [SerializeField] private TextMeshProUGUI _enemyCountText;
    [SerializeField] private Image _checkImage;
    
    [SerializeField] private GameObject _gameEndPanel;
    [SerializeField] private GameObject _clearText;
    [SerializeField] private GameObject _failText;
    [SerializeField] private TextMeshProUGUI _playTime;
    [SerializeField] private TextMeshProUGUI _killCount;
    [SerializeField] private TextMeshProUGUI _useStratagemCount;

    [Header("StratagemPanel")] 
    [SerializeField] private GameObject _stratagemPanel;
    
    [Header("Arrow")]
    [SerializeField] private Sprite _uparrowSprite;
    [SerializeField] private Sprite _downarrowSprite;
    [SerializeField] private Sprite _leftarrowSprite;
    [SerializeField] private Sprite _rightarrowSprite;

    [Header("Stratagem1")] 
    [SerializeField] private Stratagem _stratagem1;
    [SerializeField] private Image _stratagemIcon1;
    [SerializeField] private Image[] _stratagemCodelists;
    [SerializeField] private TextMeshProUGUI _coolTime1;
    [SerializeField] private TextMeshProUGUI _remaining1;
    
    [Header("Stratagem2")]
    [SerializeField] private Stratagem _stratagem2;
    [SerializeField] private Image _stratagemIcon2;
    [SerializeField] private Image[] _stratagemCodelists2;
    [SerializeField] private TextMeshProUGUI _coolTime2;
    [SerializeField] private TextMeshProUGUI _remaining2;

    [Header("Weapon Panel")] 
    [SerializeField] private Image _currentGunIcon;
    [SerializeField] private TextMeshProUGUI _currentGunMagazine;
    [SerializeField] private Image _currentGunCapacity;
    [SerializeField] private Image _choiceStratagemImage1;
    [SerializeField] private Image _choiceStratagemImage2;
    

    public Image CheckImage { get => _checkImage; set => _checkImage = value; }
    public TextMeshProUGUI EnemyCountText { get => _enemyCountText; set => _enemyCountText = value; }
    public GameObject GameEndPanel { get => _gameEndPanel; set => _gameEndPanel = value; }

    public void OpenStratagemPanel()
    {
        _stratagemPanel.SetActive(true);
    }

    public void CloseStratagemPanel()
    {
        _stratagemPanel.SetActive(false);
    }
    private void StratagemImage()
    {
        _stratagemIcon1.sprite = GameManager.Instance.GameData.StratagemEffectList[0].Icon;
        _stratagemIcon2.sprite = GameManager.Instance.GameData.StratagemEffectList[1].Icon;
        
        for (int i = 0; i < GameManager.Instance.GameData.StratagemEffectList[0].Code.Length; i++)
        {
            _stratagemCodelists[i].gameObject.SetActive(true);
            if (GameManager.Instance.GameData.StratagemEffectList[0].Code[i].ToString() == "1")
            {
                _stratagemCodelists[i].sprite = _uparrowSprite;
            }
            else if (GameManager.Instance.GameData.StratagemEffectList[0].Code[i].ToString() == "2")
            {
                _stratagemCodelists[i].sprite = _downarrowSprite;
            }
            else if (GameManager.Instance.GameData.StratagemEffectList[0].Code[i].ToString() == "3")
            {
                _stratagemCodelists[i].sprite = _leftarrowSprite;
            }
            else if(GameManager.Instance.GameData.StratagemEffectList[0].Code[i].ToString() == "4")
            {
                _stratagemCodelists[i].sprite = _rightarrowSprite;
            }
        }
        
        for (int i = 0; i < GameManager.Instance.GameData.StratagemEffectList[1].Code.Length; i++)
        {
            _stratagemCodelists2[i].gameObject.SetActive(true);
            if (GameManager.Instance.GameData.StratagemEffectList[1].Code[i].ToString() == "1")
            {
                _stratagemCodelists2[i].sprite = _uparrowSprite;
            }
            else if (GameManager.Instance.GameData.StratagemEffectList[1].Code[i].ToString() == "2")
            {
                _stratagemCodelists2[i].sprite = _downarrowSprite;
            }
            else if (GameManager.Instance.GameData.StratagemEffectList[1].Code[i].ToString() == "3")
            {
                _stratagemCodelists2[i].sprite = _leftarrowSprite;
            }
            else if(GameManager.Instance.GameData.StratagemEffectList[1].Code[i].ToString() == "4")
            {
                _stratagemCodelists2[i].sprite = _rightarrowSprite;
            }
        }
    }
    
    private void Awake()
    {
        Instance = this;
        _gameEndPanel.SetActive(false);
        CheckImage.enabled = false;
    }

    private float cool;
    private void Start()
    {
        GameManager.Instance.GameData.KillCount = 0;
        GameManager.Instance.GameData.UseStratagemCount = 0;
        GameManager.Instance.GameData.PlayTime = 0f;
        StratagemImage();
        CloseStratagemPanel();
        _choiceStratagemImage1.sprite = GameManager.Instance.GameData.StratagemEffectList[0].Icon;
        _choiceStratagemImage2.sprite = GameManager.Instance.GameData.StratagemEffectList[1].Icon;
    }

    private void Update()
    {
        GameManager.Instance.GameData.PlayTime += Time.deltaTime;
        StratagemPanelUI();
    }
    
    private float time1;
    private float time2;
    private string text1;
    private string text2;
    
    private void StratagemPanelUI()
    {
        if (_stratagemPanel == null) return;
        
        time1 = _stratagem1.StratagemEffect.Cooldown - _stratagem1.CoolTime < 0 
            ? 0 : _stratagem1.StratagemEffect.Cooldown - _stratagem1.CoolTime;
        
        time2 = _stratagem2.StratagemEffect.Cooldown - _stratagem2.CoolTime < 0 
            ? 0 : _stratagem2.StratagemEffect.Cooldown - _stratagem2.CoolTime;
        
        if (_stratagem1.StratagemEffect.UseLimit - _stratagem1.UseCount < 0)
            text1 = "Unlimited";
        else
            text1 = (_stratagem1.StratagemEffect.UseLimit - _stratagem1.UseCount).ToString();
        
        if (_stratagem2.StratagemEffect.UseLimit - _stratagem2.UseCount < 0)  
            text2 = "Unlimited";
        else 
            text2 = (_stratagem2.StratagemEffect.UseLimit - _stratagem2.UseCount).ToString();
        
        _coolTime1.text = $"CoolTime : {time1:F2}";
        _remaining1.text = $"Remaining : {text1}";
        _coolTime2.text =  $"CoolTime : {time2:F2}";
        _remaining2.text = $"Remaining : {text2}";
    }

    public void ReloadMagazine(int magazine)
    {
        if (magazine < 0)
        {
            _currentGunMagazine.text = "Unlimited";
        }
        else
        {
            _currentGunMagazine.text = $"x {magazine}";
        }
        
    }

    public void ChangeGunImage(Sprite sprite)
    {
        _currentGunIcon.sprite = sprite;
    }

    public void UseMagazine(Gun gun, int magazine)
    {
        _currentGunCapacity.fillAmount = (float)magazine / gun.Stats.Capacity;
    }

    public void MissionClear()
    {
        Time.timeScale = 0;
        _gameEndPanel.SetActive(true);
        _clearText.SetActive(true);
        _failText.SetActive(false);
        Text();
    }

    public void MissionFail()
    {
        Time.timeScale = 0;
        _gameEndPanel.SetActive(true);
        _clearText.SetActive(false);
        _failText.SetActive(true);
        Text();
    }

    private void Text()
    {
        float playTime = GameManager.Instance.GameData.PlayTime;
        int minutes = Mathf.FloorToInt(playTime / 60);
        int seconds = Mathf.FloorToInt(playTime % 60);
        _playTime.text = $"Play Time : {string.Format("{0:00}:{1:00}", minutes, seconds)}";
        _killCount.text = $"Kill : {GameManager.Instance.GameData.KillCount}";
        _useStratagemCount.text = $"Use Stratagem Count : {GameManager.Instance.GameData.UseStratagemCount}";
    }
}
