using TMPro;
using UnityEngine;

public class CallHelicopterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _remainingTime;
    
    public TextMeshProUGUI RemainingTime => _remainingTime;

    private float _arriveTime;
    public float ArriveTime {get => _arriveTime; set => _arriveTime = value;}

    private void Update()
    {
        _arriveTime -= Time.deltaTime;
        
        int seconds = Mathf.FloorToInt(_arriveTime % 60f);
        int milliseconds = Mathf.FloorToInt((_arriveTime * 100f) % 100f);
        
        _remainingTime.text = $"{string.Format("{0:00}:{1:00}", seconds,milliseconds)}";
        if (_arriveTime <= 0)
        {
            Debug.Log("헬리콥터 UI 파괴");
            Destroy(gameObject);
        }
    }
}
