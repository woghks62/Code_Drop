using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour
{
    [SerializeField] private Image _checkImage;
    [SerializeField] private TextMeshProUGUI _missionText;
    [SerializeField] private TextMeshProUGUI _countText;

    public void SetMission(string mission)
    {
        _missionText.text = mission;
        _checkImage.enabled = false;
        _countText.gameObject.SetActive(false);
    }

    public void SetCount(int current, int goal)
    {
        _countText.gameObject.SetActive(true);
        _countText.text = $"{current} / {goal}";
    }

    public void Complete()
    {
        _checkImage.enabled = true;
    }
}
