using UnityEngine;

public class Stratagem : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    
    [SerializeField] private StratagemEffect _stratagemEffect;
    [SerializeField] private int _useCount;
    
    public StratagemEffect StratagemEffect {get => _stratagemEffect; set => _stratagemEffect = value; }
    public int UseCount { get => _useCount; set => _useCount = value; }

    private void OnTriggerEnter(Collider other)
    {
        if ((_targetLayer & (1 << other.gameObject.layer)) != 0)
        {
            _stratagemEffect.Apply();    
            _useCount++;
            gameObject.SetActive(false);
        }
        // 추후에 땅에 닿았을때만 작동하게 변경 예정
        // targetLayer 추가 후 설정 예정
        
    }
}
