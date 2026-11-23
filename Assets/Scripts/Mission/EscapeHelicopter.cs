using System.Collections;
using UnityEngine;

public class EscapeHelicopter : MonoBehaviour
{
    [SerializeField] private GameObject _rotar;
    [SerializeField] private GameObject _rotar2;
    [SerializeField] private int _rotateSpeed;
    [SerializeField] private int _moveSpeed;
    
    private bool _isLand;

    private void Update()
    {
        _rotar.transform.Rotate(Vector3.up * Time.deltaTime * _rotateSpeed);
        _rotar2.transform.Rotate(Vector3.forward * Time.deltaTime * _rotateSpeed);

        if (!_isLand)
        {
            transform.Translate(Vector3.down * Time.deltaTime * _moveSpeed);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && !_isLand)
        {
            MissionManager.Instance.Arrive();
            _isLand = true;
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            MissionManager.Instance.Escape();
            UIManager.Instance.MissionClear();
        }
    }

    public IEnumerator Escape(int escapeTime)
    {
        Debug.Log($"비행기 출발까지 남은 시간 : {escapeTime}");
        yield return new WaitForSeconds(escapeTime);
        gameObject.SetActive(false);
        UIManager.Instance.MissionFail();
        Debug.Log("비행기가 떠납니다");
    }
}
