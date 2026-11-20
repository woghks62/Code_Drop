using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CallStratagemUI : MonoBehaviour
{
    [SerializeField] private StratagemEffect _stratagemEffect;
    [SerializeField] private Image _iconSprite;
    [SerializeField] private TextMeshProUGUI _remainingTime;
    
    public StratagemEffect StratagemEffect {get => _stratagemEffect; set => _stratagemEffect = value;}
    public Image IconSprite => _iconSprite;
    public TextMeshProUGUI RemainingTime => _remainingTime;

    private float time;
    
    private void Start()
    {
        _iconSprite.sprite = _stratagemEffect.Icon;
        time = _stratagemEffect.WaitTime;
    }

    private void Update()
    {
        time -= Time.deltaTime;
        
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 100f) % 100f);
        
        _remainingTime.text = $"{string.Format("{0:00}:{1:00}", seconds,milliseconds)}";
        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }
}