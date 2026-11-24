using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameData _gameData;
    
    public GameData GameData => _gameData;

    private void Awake() => SetSingleton();

    // 타이틀로 돌아가기
    public void TitleScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    
    public void LoadOutScene()
    {
        SceneManager.LoadScene(1);
    }

    public void GameScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    // 게임 종료하기
    public void QuitGame()
    {
#if UNITY_EDITOR
        // 유니티 에디터에서 플레이 모드 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 실제 빌드된 게임에서 애플리케이션 종료
        Application.Quit();
#endif
    }

    public List<Gun> SetGameData()
    {
        List<Gun> _choiceGunlists = new List<Gun>();
        _choiceGunlists.Add(_gameData.ChoiceGun);
        _choiceGunlists.Add(_gameData.Pistol);
        
        return _choiceGunlists;
    }


    private void SetSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ResetData()
    {
        _gameData.ChoiceGun = null;
        _gameData.StratagemEffectList = null;
        _gameData.KillCount = 0;
        _gameData.UseStratagemCount = 0;
        _gameData.PlayTime = 0;
        _gameData.IsCallEscape = false;
    }
}
