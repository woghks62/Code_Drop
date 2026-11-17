using System.Collections;
using UnityEngine;

public class EscapePlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            UIManager.Instance.MissionClear();
        }
    }

    public IEnumerator Escape(int escapeTime)
    {
        Debug.Log($"비행기 출발까지 남은 시간 : {escapeTime}");
        yield return new WaitForSeconds(escapeTime);
        gameObject.SetActive(false);
        UIManager.Instance.MissionFail();
        Debug.Log("비행기가 떠납니다");
    }
}
