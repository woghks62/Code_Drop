using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Data/StratagemEffect/Tower")]
public class Tower : StratagemEffect
{
    [SerializeField] private GameObject _tower;
    
    public override void Apply(Vector3 position)
    {
        Instantiate(_tower, new Vector3(position.x,0,position.z), Quaternion.identity);
    }
}
