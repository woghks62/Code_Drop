using UnityEngine;

[CreateAssetMenu(fileName = "Bomb", menuName = "Data/StratagemEffect/Bomb")]
public class BombSupply : StratagemEffect
{
    [SerializeField] private GameObject _bomb;
    [SerializeField] private ParticleSystem _bombEffect;
    
    public override void Apply(Vector3 position)
    {
        Instantiate(_bomb, new Vector3(position.x,0,position.z), Quaternion.identity);
        Instantiate(_bombEffect, new Vector3(position.x, 0, position.z), Quaternion.identity);
    }
}
