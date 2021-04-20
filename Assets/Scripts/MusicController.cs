using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    
    private List<AudioClip> _audioClips = new List<AudioClip>();

    
    void Start()
    {
        _audioClips.Add((AudioClip)Resources.Load("Audio/DayTime/Potato"));
        _audioClips.Add((AudioClip)Resources.Load("Audio/DayTime/SunnyDay"));
        _audioClips.Add((AudioClip)Resources.Load("Audio/DayTime/Chunky_Monkey"));
        _audioClips.Add((AudioClip)Resources.Load("Audio/DayTime/Tiny_Blocks"));
        _audioClips.Add((AudioClip)Resources.Load("Audio/NightTime"));
        GameManager.Instance.onGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (previousState == GameManager.GameState.PREGAME && currentState == GameManager.GameState.RUNNING)
        {
            SetDayMusic();
        }
        else if (previousState == GameManager.GameState.ENDDAY && currentState == GameManager.GameState.RUNNING)
        {
            SetDayMusic();
        }
    }
    void Update()
    {
        if (GameManager.Instance.hours == 19)
        {
           SetNightMusic();
        }

        if (GameManager.Instance.hours == 7 && GameManager.Instance.minutes == 0)
        {
            SetDayMusic();
        }
    }
    
    public void SetDayMusic()
    {
        _audioSource.Stop();
        int randomNumber = Random.Range(0, _audioClips.Count - 1);
        _audioSource.clip = _audioClips[randomNumber];
        _audioSource.Play();
    }

    public void SetNightMusic()
    {
        _audioSource.Stop();
        _audioSource.clip = _audioClips[_audioClips.Count - 1];
        _audioSource.Play();
    }
}
