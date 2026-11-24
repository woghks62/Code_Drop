using UnityEngine;

public class TitleScene : MonoBehaviour
{
    public void GameStartButton()
    {
        GameManager.Instance.LoadOutScene();
    }
    public void QuitButton()
    {
        GameManager.Instance.QuitGame();
    }
}
