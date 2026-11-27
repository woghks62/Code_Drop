using UnityEngine;

public class TitleScene : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance?.BGMPlay(BGMType.Title);
    }

    public void GameStartButton()
    {
        GameManager.Instance.LoadOutScene();
    }
    public void QuitButton()
    {
        GameManager.Instance.QuitGame();
    }
}
