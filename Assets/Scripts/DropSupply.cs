using UnityEngine;

public class DropSupply : MonoBehaviour
{
    [SerializeField] private LayerMask _beaconLayer;
    
    private StratagemEffect _stratagemeffect;
    private Vector3 _position;
    
    public void SetEffect(StratagemEffect effect, Vector3 position)
    {
        _stratagemeffect = effect;
        _position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((_beaconLayer & (1 << other.gameObject.layer)) != 0)
        {
            _stratagemeffect.Apply(_position);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
