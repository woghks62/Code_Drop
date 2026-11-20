using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stratagem : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    
    [SerializeField] private StratagemEffect _stratagemEffect;
    [SerializeField] private GameObject _child;
    [SerializeField] private int _useCount;
    [SerializeField] private bool _canExecute;
    [SerializeField] private float _cooltime;
    
    [SerializeField] private CallStratagemUI _callStratagemUI;
    [SerializeField] private Transform _panelParent;
    
    public StratagemEffect StratagemEffect {get => _stratagemEffect; set => _stratagemEffect = value; }
    public GameObject Child {get => _child; set => _child = value; }
    public int UseCount { get => _useCount; set => _useCount = value; }
    public bool CanExecute { get => _canExecute; set => _canExecute = value; }
    public float CoolTime {get => _cooltime; set => _cooltime = value; }


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
            
            Transform beaconposition = gameObject.transform;
            Debug.Log(gameObject.transform.position);
            
            Vector3 position = new Vector3(beaconposition.position.x, 0, beaconposition.position.z);
            
            //StartCoroutine(_stratagemEffect.ApplyRoutine(_stratagemEffect.WaitTime, position));
            
            StartCoroutine(DropRoutine(_stratagemEffect, _stratagemEffect.WaitTime, position));
            
            GameObject beacon = Instantiate(_stratagemEffect.BeaconPrefabs,position,Quaternion.identity);

            if (beacon.TryGetComponent<MeshRenderer>(out var meshRenderer))
            {
                meshRenderer.material= _stratagemEffect.BeaconMaterial;
            }
            //beacon.GetComponent<MeshRenderer>().material = _stratagemEffect.BeaconMaterial;
            
            // GameObject a = Instantiate(_uiprefabs);
            // a.transform.SetParent(_canvas.transform);
            // Image image = a.GetComponentInChildren<Image>();
            // Debug.Log(image.gameObject.name);
            // image = _stratagemEffect.Icon;
            // image.material = _stratagemEffect.BeaconMaterial;
            
            _useCount++;
            _canExecute = false;
            //gameObject.SetActive(false);
            if (_child != null)
            {
                _child.SetActive(false);
            }
        }
    }

    private IEnumerator DropRoutine(StratagemEffect effect, float waittime, Vector3 position)
    {
        CallStratagemUI callStratagemUI = Instantiate(_callStratagemUI);
        callStratagemUI.gameObject.transform.SetParent(_panelParent);
        callStratagemUI.StratagemEffect = effect;
        
        yield return new WaitForSeconds(waittime);
        DropSupply drop = Instantiate(_stratagemEffect.DropObject, new Vector3(position.x, 30, position.z), Quaternion.identity);
        drop.SetEffect(effect,position);
    }
}
