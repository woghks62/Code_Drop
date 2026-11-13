using UnityEngine;

public class Stratagem : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    
    [SerializeField] private StratagemEffect _stratagemEffect;
    [SerializeField] private int _useCount;
    [SerializeField] private float _cooltime;
    public float CoolTime {get => _cooltime; set => _cooltime = value; }

    [SerializeField] private GameObject _child;
    public GameObject Child {get => _child; set => _child = value; }
    
    [SerializeField] private bool _canExecute;
    public bool CanExecute { get => _canExecute; set => _canExecute = value; }
    
    
    public StratagemEffect StratagemEffect {get => _stratagemEffect; set => _stratagemEffect = value; }
    public int UseCount { get => _useCount; set => _useCount = value; }

    private void Update()
    {
        _cooltime += Time.deltaTime;
        if (!_canExecute) return;
        
        _child.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((_targetLayer & (1 << other.gameObject.layer)) != 0)
        {
            if (_canExecute == false) return;
            
            _stratagemEffect.Apply();    
            _useCount++;
            _canExecute = false;
            //gameObject.SetActive(false);
            if (_child != null)
            {
                _child.SetActive(false);
            }
        }
    }
}
