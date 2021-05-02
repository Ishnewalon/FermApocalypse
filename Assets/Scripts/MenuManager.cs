using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _background;
    
    [SerializeField] 
    private GameObject _startMenu;
    
    [SerializeField] 
    private GameObject _pauseMenu;
    
    [SerializeField] 
    private GameObject _endDayMenu;
    
    [SerializeField]
    private Text _endDayTMP;

    [SerializeField] 
    private GameObject _goToBed;
    
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
        else if (previousState == GameManager.GameState.RUNNING && currentState == GameManager.GameState.ENDDAY)
        {
            _endDayMenu.gameObject.SetActive(true);
            _endDayTMP.text = "End of Day " + (GameManager.Instance.day - 1);
            _background.gameObject.SetActive(true);
        }
        else if (previousState == GameManager.GameState.GOTOBED && currentState == GameManager.GameState.ENDDAY)
        {
            _endDayMenu.gameObject.SetActive(true);
            _endDayTMP.text = "End of Day " + (GameManager.Instance.day - 1) + " Month " 
                + GameManager.Instance.month + " Year " + GameManager.Instance.year;
            _background.gameObject.SetActive(true);
        }
        else if (previousState == GameManager.GameState.RUNNING && currentState == GameManager.GameState.GOTOBED)
        {
            _goToBed.gameObject.SetActive(true);
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
            else if (GameManager.Instance.hours == 1)
            {
                GameManager.Instance.ToggleNewDay();
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

    public void CharacterSelection()
    {
        _playerGender = EventSystem.current.currentSelectedGameObject.name;
    }

    public void Quit()
    {
        GameManager.Instance.QuitGame();
    }

    public void NextDay()
    {
        if (GameManager.Instance._currentSpawnLocation == GameManager.SpawnLocation.FARMFROMTOWN
            || GameManager.Instance._currentSpawnLocation == GameManager.SpawnLocation.FARMFROMHOUSE)
        {
            GameManager.Instance.UnloadLevel("Farm");
            GameManager.Instance.LoadLevel("FarmHouse");
        }
        else if (GameManager.Instance._currentSpawnLocation == GameManager.SpawnLocation.TOWN)
        {
            GameManager.Instance.UnloadLevel("Town");
            GameManager.Instance.LoadLevel("FarmHouse");
        }
        GameManager.Instance.IncrementGrowingPlant();
        _endDayMenu.gameObject.SetActive(false);
        _background.gameObject.SetActive(false);
        GameManager.Instance.ToggleNewDay();
    }

    public void CancelGoToBed()
    {
        _goToBed.gameObject.SetActive(false);
        GameManager.Instance.ToggleGoToBedDialog();
    }

    public void GoToBed()
    {
        GameManager.Instance.minutes = 0;
        GameManager.Instance.hours = 1;
        GameManager.Instance.day++;
        _goToBed.gameObject.SetActive(false);
        GameManager.Instance.ToggleNewDay();
    }
}
