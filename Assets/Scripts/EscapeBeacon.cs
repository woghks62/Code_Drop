using System.Collections;
using UnityEngine;

public class EscapeBeacon : MonoBehaviour
{
    [SerializeField] private GameObject _escapeLight;
    [SerializeField] private string _escapeBeaconCode;
    
    
    [SerializeField] private int _EscapeCallTime;
    [SerializeField] private GameObject _escapePlanePrefab;

    [SerializeField] private bool _isPlaneCall;

    private void Start()
    {
        _escapeLight.SetActive(false);
        SetBeaconCode();
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isPlaneCall)
        {
            return;
        }
        
        if (other.GetComponent<PlayerShooter>() != null)
        {
            if (other.GetComponent<PlayerStratagem>().Code == _escapeBeaconCode)
            {
                _escapeLight.SetActive(true);
                StartCoroutine(EscapeRoutine());   
            }
        }
    }

    private IEnumerator EscapeRoutine()
    {
        _isPlaneCall = true;
        Debug.Log("비행기 호출");
        yield return new WaitForSeconds(_EscapeCallTime);
        
        Instantiate(_escapePlanePrefab, new Vector3(transform.position.x,10,transform.position.z), transform.rotation);
    }

    private void SetBeaconCode()
    {
        for (int i = 0; i < 6; i++)
        {
            _escapeBeaconCode += Random.Range(1, 5);
        }
    }
}
