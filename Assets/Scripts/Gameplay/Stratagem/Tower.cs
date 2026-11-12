using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Data/StratagemEffect/Tower")]
public class Tower : StratagemEffect
{
    public override void Apply()
    {
        Debug.Log("Tower Apply");
    }
}
