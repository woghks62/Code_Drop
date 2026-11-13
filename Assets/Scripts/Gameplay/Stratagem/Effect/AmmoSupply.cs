using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoSupply", menuName = "Data/StratagemEffect/AmmoSupply")]
public class AmmoSupply : StratagemEffect
{
    [SerializeField] private GameObject ammo;
    
    public override IEnumerator ApplyRoutine(float time, Vector3 position)
    {
        Debug.Log("Ammo Apply Routine 실행");
        GameObject obj = Instantiate(ammo, new Vector3(position.x,30,position.z), Quaternion.identity);
        obj.GetComponent<Rigidbody>().useGravity = false;
        yield return new WaitForSeconds(time);
        Debug.Log("Ammo Apply Routine 시간 지난 뒤");
        obj.GetComponent<Rigidbody>().useGravity = true;
    }
}
