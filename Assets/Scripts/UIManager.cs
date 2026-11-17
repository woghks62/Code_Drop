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

    public Image CheckImage { get => _checkImage; set => _checkImage = value; }
    public TextMeshProUGUI EnemyCountText { get => _enemyCountText; set => _enemyCountText = value; }
    public GameObject GameEndPanel { get => _gameEndPanel; set => _gameEndPanel = value; }

    private void Awake()
    {
        Instance = this;
        _gameEndPanel.SetActive(false);
    }

    private void Start()
    {
        GameManager.Instance.GameData.KillCount = 0;
        GameManager.Instance.GameData.UseStratagemCount = 0;
        GameManager.Instance.GameData.PlayTime = 0f;
    }

    private void Update()
    {
        GameManager.Instance.GameData.PlayTime += Time.deltaTime;
    }

    public void MissionClear()
    {
        _gameEndPanel.SetActive(true);
        _clearText.SetActive(true);
        _failText.SetActive(false);
        Text();
    }

    public void MissionFail()
    {
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
