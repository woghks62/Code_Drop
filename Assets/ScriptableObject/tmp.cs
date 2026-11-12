using UnityEngine;

public class tmp : MonoBehaviour
{
    [SerializeField] private StratagemEffect _stratagemEffect;

    private void Start()
    {
        _stratagemEffect.Apply();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            _stratagemEffect = new Tower();
            _stratagemEffect.Apply();
        }
    }
}
