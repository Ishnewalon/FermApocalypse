using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _pauseMenu;
    private string _playerGender;
    public void Start()
    {
        GameManager.Instance.onGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (previousState == GameManager.GameState.PREGAME && currentState == GameManager.GameState.RUNNING)
        {
            _background.gameObject.SetActive(false);
            _startMenu.gameObject.SetActive(false);
        }
        else if (previousState == GameManager.GameState.RUNNING && currentState == GameManager.GameState.PAUSE)
        {
            _pauseMenu.gameObject.SetActive(true);
            _background.gameObject.SetActive(true);
        }
        
    }
    void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.RUNNING)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.TogglePause();
            }
        }

        if (GameManager.Instance.CurrentGameState == GameManager.GameState.PREGAME)
        {
           
            GameManager.Instance.SetCharacterGender(_playerGender);
        }
    }

    public void Play()
    {
        GameManager.Instance.StartGame();
    }
    
    public void ResumeGame()
     {
         _pauseMenu.gameObject.SetActive(false);
         _background.gameObject.SetActive(false);
        GameManager.Instance.TogglePause();
     }

    public void Quit()
    {
        GameManager.Instance.QuitGame();
    }

}
