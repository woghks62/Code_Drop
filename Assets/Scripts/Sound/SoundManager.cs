using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public enum BGMType
{
    Title,LoadOut,Game
}

public enum SFXType
{
    Click = 0,
    
    Pistol = 1,Rifle,Shotgun,Sniper,Reload,EmptyAmmo,
    
    PlayerHit = 7,PlayerDead,
    
    EnemyHit=9, EnemyDead, EnemyAttack,
    
    CodeInput=12, CodeSuccess, CodeFail, BeaconThrow, Bomb, Ammo, Turret,
    
    MissionComplete=19, EscapeCall, Helicopter
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource BGM, SFX;
    public AudioClip[] BGMArr, SFXArr;
    
    public float masterVolume = 1f;
    public float bgmVolume = 1f;
    public float sfxVolume = 1f;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void BGMPlay(BGMType type)
    {
        int index = (int)type;

        BGM.Stop();

        if (index >= 0 && index < BGMArr.Length)
        {
            BGM.clip = BGMArr[index];
            BGM.loop = true;
            BGM.Play();
        }
    }


    public void SFXPlay(SFXType type)
    {
        int index = (int)type;

        if (index >= 0 && index < SFXArr.Length)
        {
            SFX.PlayOneShot(SFXArr[index]);
        }
    }
    

    public void BGMStop(BGMType type)
    {
        int index = (int)type;
        BGM.Stop();
    }
}