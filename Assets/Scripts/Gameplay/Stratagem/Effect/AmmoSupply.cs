using UnityEngine;

[CreateAssetMenu(fileName = "AmmoSupply", menuName = "Data/StratagemEffect/AmmoSupply")]
public class AmmoSupply : StratagemEffect
{
    public override void Apply()
    {
        Debug.Log("AmmoSupply Apply");
    }
}
