using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EscapeBeacon : MonoBehaviour
{
    [SerializeField] private GameObject _escapeLight;
    [SerializeField] private string _escapeBeaconCode;
    
    
    [SerializeField] private int _escapeArriveTime;
    [SerializeField] private EscapePlane _escapePlanePrefab;
    [SerializeField] private int _escapeTime;
    [SerializeField] private TextMeshProUGUI _waitTimer;
    [SerializeField] private float _waitTime;

    [SerializeField] private bool _isPlaneCall;

    [SerializeField] private GameObject _escapeCode;
    [SerializeField] private Image[] _imagelists;
    
    [SerializeField] private MeshRenderer _meshRenderer;
    
    private void Start()
    {
        _escapeLight.SetActive(false);
        _waitTimer.gameObject.SetActive(false);
        SetBeaconCode();
        SetCodeImage();
        _escapeCode.SetActive(false);
        _waitTime = _escapeArriveTime;
    }

    private void Update()
    {
        if (!_isPlaneCall)
        {
            return;
        }
        
        _waitTime -= Time.deltaTime;
        _waitTimer.text = _waitTime.ToString("0");
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isPlaneCall)
        {
            return;
        }
        
        if (other.GetComponent<PlayerShooter>() != null)
        {
            if (!_escapeCode.activeSelf)
            {
                _escapeCode.SetActive(true);
            }
            
            if (other.GetComponent<PlayerStratagem>().Code == _escapeBeaconCode)
            {
                _meshRenderer.enabled = false;
                _escapeCode.SetActive(false);
                _escapeLight.SetActive(true);
                _waitTimer.gameObject.SetActive(true);
                GameManager.Instance.GameData.IsCallEscape = true;
                StartCoroutine(EscapeRoutine());   
            }
        }
    }
    
    private IEnumerator EscapeRoutine()
    {
        _isPlaneCall = true;
        Debug.Log("비행기 호출");
        yield return new WaitForSeconds(_escapeArriveTime);
        _waitTimer.gameObject.SetActive(false);
        
        EscapePlane escapePlane = Instantiate(_escapePlanePrefab, new Vector3(transform.position.x,10,transform.position.z), transform.rotation);
        escapePlane.StartCoroutine(escapePlane.Escape(_escapeTime));
    }

    private void SetCodeImage()
    {
        for (int i = 0; i < _escapeBeaconCode.Length; i++)
        {
            if (_escapeBeaconCode[i].ToString() == "1")
            {
                _imagelists[i].sprite = UIManager.Instance.UpArrowSprite;
            }
            else if (_escapeBeaconCode[i].ToString() == "2")
            {
                _imagelists[i].sprite = UIManager.Instance.DownArrowSprite;
            }
            else if (_escapeBeaconCode[i].ToString() == "3")
            {
                _imagelists[i].sprite = UIManager.Instance.LeftArrowSprite;
            }
            else if (_escapeBeaconCode[i].ToString() == "4")
            {
                _imagelists[i].sprite = UIManager.Instance.RightArrowSprite;
            }
        }
    }
    
    private void SetBeaconCode()
    {
        for (int i = 0; i < 6; i++)
        {
            _escapeBeaconCode += Random.Range(1, 5);
        }
    }
}
