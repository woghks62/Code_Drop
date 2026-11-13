using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Data/StratagemEffect/Tower")]
public class Tower : StratagemEffect
{
    [SerializeField] private GameObject _tower;

    public override IEnumerator ApplyRoutine(float time, Vector3 position)
    {
        Debug.Log("Tower Apply Routine 실행");
        GameObject obj = Instantiate(_tower, new Vector3(position.x,30,position.z), Quaternion.identity);
        obj.GetComponent<Rigidbody>().useGravity = false;
        yield return new WaitForSeconds(time);
        Debug.Log("Tower Apply Routine 시간 지난 뒤");
        obj.GetComponent<Rigidbody>().useGravity = true;

    }
}
