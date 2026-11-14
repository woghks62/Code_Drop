using UnityEngine;

[CreateAssetMenu(fileName = "Bomb", menuName = "Data/StratagemEffect/Bomb")]
public class BombSupply : StratagemEffect
{
    [SerializeField] private GameObject _bomb;
    
    public override void Apply(Vector3 position)
    {
        Instantiate(_bomb, new Vector3(position.x,0,position.z), Quaternion.identity);
    }
}
