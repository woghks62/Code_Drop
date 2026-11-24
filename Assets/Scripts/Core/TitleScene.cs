using UnityEngine;

public class TitleScene : MonoBehaviour
{
    public void GameStartButton()
    {
        GameManager.Instance.LoadOutScene();
    }
}
