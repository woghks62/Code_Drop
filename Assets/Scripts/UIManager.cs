using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [SerializeField] private Transform _callPanelParent;
    public Transform CallPanelParent => _callPanelParent;
    
    [Header("게임 종료 결과창")]
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
    
    [Header("Weapon Panel")] 
    [SerializeField] private Image _currentGunIcon;
    [SerializeField] private TextMeshProUGUI _currentGunMagazine;
    [SerializeField] private Image _currentGunCapacity;
    [SerializeField] private Image _choiceStratagemImage1;
    [SerializeField] private Image _choiceStratagemImage2;
    
     public Sprite UpArrowSprite => _uparrowSprite;
     public Sprite DownArrowSprite => _downarrowSprite;
     public Sprite LeftArrowSprite => _leftarrowSprite;
     public Sprite RightArrowSprite => _rightarrowSprite;

    public GameObject GameEndPanel { get => _gameEndPanel; set => _gameEndPanel = value; }

    public void OpenStratagemPanel()
    {
        _stratagemPanel.SetActive(true);
    }

    public void CloseStratagemPanel()
    {
        _stratagemPanel.SetActive(false);
    }
    
    private void Awake()
    {
        Instance = this;
        _gameEndPanel.SetActive(false);
    }

    private float cool;
    private void Start()
    {
        GameManager.Instance.GameData.KillCount = 0;
        GameManager.Instance.GameData.UseStratagemCount = 0;
        GameManager.Instance.GameData.PlayTime = 0f;
        CloseStratagemPanel();
        _choiceStratagemImage1.sprite = GameManager.Instance.GameData.StratagemEffectList[0].Icon;
        _choiceStratagemImage2.sprite = GameManager.Instance.GameData.StratagemEffectList[1].Icon;
    }

    private void Update()
    {
        GameManager.Instance.GameData.PlayTime += Time.deltaTime;
    }
    
    
    public void ChangeMagazine(int magazine)
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

    public void MainButton()
    {
        GameManager.Instance.TitleScene();
    }

    public void ReStartButton()
    {
        GameManager.Instance.GameScene();
    }
}
