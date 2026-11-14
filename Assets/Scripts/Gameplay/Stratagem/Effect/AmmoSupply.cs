using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoSupply", menuName = "Data/StratagemEffect/AmmoSupply")]
public class AmmoSupply : StratagemEffect
{
    [SerializeField] private GameObject ammo;
    
    public override void Apply(Vector3 position)
    {
        Instantiate(ammo, new Vector3(position.x,0,position.z), Quaternion.identity);
    }
}
