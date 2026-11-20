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

    [SerializeField] private int index =0;
    private void OnTriggerStay(Collider other)
    {
        if (_isPlaneCall)
        {
            return;
        }
        
        if (other.TryGetComponent<PlayerShooter>(out PlayerShooter playerShooter))
        {
            if (playerShooter.gameObject.TryGetComponent<PlayerStratagem>(out PlayerStratagem playerStratagem))
            {
                if (!playerStratagem.PushCodeButtonDown)
                {
                    ResetBeaconCode();
                    playerStratagem.Code = null;
                    index = 0;
                }
            }
            if (!_escapeCode.activeSelf)
            {
                _escapeCode.SetActive(true);
            }

            if (other.TryGetComponent<PlayerStratagem>(out PlayerStratagem player))
            {
                player.IsInEscapeBeacon =  true;
            }

            // 플레이어스트라타잼이 있을때만 조건 통과
            if (other.GetComponent<PlayerStratagem>().Code != null)
            {
                // 탈출코드와 플레이어스트라타잼 코드 비교 후 같은 인덱스 번호가 같으면
                if (_escapeBeaconCode[index] == other.GetComponent<PlayerStratagem>().Code[index])
                {
                    _imagelists[index].color = Color.green;
                    index++;
                }
                // 탈출 코드와 플레이어 스트라타잼 코드가 다르면
                else
                {
                    // 근데 다른데 탈출코드 0번째와 인덱스번호의 틀린 코드가 같으면
                    if (_escapeBeaconCode[0] == other.GetComponent<PlayerStratagem>().Code[index])
                    {
                        Debug.Log("틀렸지만 처음 코드랑 같아요");
                        for (int i = 0; i < index; i++)
                        {
                            _imagelists[i].color = Color.white;
                        }

                        other.GetComponent<PlayerStratagem>().Code = other.GetComponent<PlayerStratagem>().Code[0].ToString();
                        _imagelists[0].color = Color.green;
                        index = 1;
                    }
                    // 탈출코드 0번째와 입력한 코드도 다르다면
                    else
                    {
                        Debug.Log("틀렸어요");
                        for (int i = 0; i < index; i++)
                        {
                            _imagelists[i].color = Color.white;
                            other.GetComponent<PlayerStratagem>().Code = null;
                        }
                        index = 0;
                    }
                }
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

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerStratagem>(out PlayerStratagem player))
        {
            player.IsInEscapeBeacon = false;
            player.Code = null;
            index = 0;
            ResetBeaconCode();
        }
    }

    public void ResetBeaconCode()
    {
        for (int i = 0; i < _imagelists.Length; i++)
        {
            _imagelists[i].color = Color.white;
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
