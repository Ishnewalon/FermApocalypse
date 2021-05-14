using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject background;
    
    [SerializeField] 
    private GameObject startMenu;
    
    [SerializeField] 
    private GameObject tutorialMenu;
    
    [SerializeField] 
    private GameObject pauseMenu;
    
    [SerializeField] 
    private GameObject endDayMenu;
    
    [SerializeField]
    private Text endDayTitleTMP;
    
    [SerializeField]
    private Text endDayMoneyStartTMP;
    
    [SerializeField]
    private Text endDayMoneyEndTMP;
    
    [SerializeField]
    private Text endDayProfitTMP;

    [SerializeField] 
    private GameObject goToBed;

    [SerializeField] 
    private GameObject endGame;
    
    private string _playerGender;
    
    public void Start()
    {
        GameManager.Instance.onGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (previousState == GameManager.GameState.PREGAME && currentState == GameManager.GameState.RUNNING)
        {
            background.SetActive(false);
            tutorialMenu.SetActive(false);
        }
        else if (previousState == GameManager.GameState.RUNNING && currentState == GameManager.GameState.PAUSE)
        {
            pauseMenu.SetActive(true);
            background.SetActive(true);
        }
        else if (previousState == GameManager.GameState.RUNNING && currentState == GameManager.GameState.ENDDAY)
        {
            SetEndDayMenu();
        }
        else if (previousState == GameManager.GameState.GOTOBED && currentState == GameManager.GameState.ENDDAY)
        {
            SetEndDayMenu();
        }
        else if (previousState == GameManager.GameState.RUNNING && currentState == GameManager.GameState.GOTOBED)
        {
            goToBed.SetActive(true);
        }
        else if (previousState == GameManager.GameState.ENDDAY && currentState == GameManager.GameState.ENDGAME)
        {
            endDayMenu.SetActive(false);
            endGame.SetActive(true);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.TogglePause();
        }
        else if (GameManager.Instance.hours == 1)
        {
            GameManager.Instance.ToggleNewDay();
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

    public void ToTutorial()
    {
        startMenu.SetActive(false);
        tutorialMenu.SetActive(true);
    }

    public void Restart()
    {
        Destroy(GameObject.Find("GameManager"));
        SceneManager.LoadScene("MainMenu");
    }
    public void ResumeGame()
     {
        pauseMenu.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
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
        if (GameManager.Instance.IsGameWon())
        {
            GameManager.Instance.ToggleEndGame();
        }
        else
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
            endDayMenu.gameObject.SetActive(false);
            background.gameObject.SetActive(false);
            GameManager.Instance.ToggleNewDay();
            GameManager.Instance.moneyAtStartOfDay = GameManager.Instance.PlayerInventory.GetBalance();
        }
    }

    public void CancelGoToBed()
    {
        goToBed.gameObject.SetActive(false);
        GameManager.Instance.ToggleGoToBedDialog();
    }

    public void GoToBed()
    {
        GameManager.Instance.minutes = 0;
        GameManager.Instance.hours = 1;
        GameManager.Instance.day++;
        goToBed.gameObject.SetActive(false);
        GameManager.Instance.ToggleNewDay();
    }

    private void SetEndDayMenu()
    {
        endDayMenu.SetActive(true);
        endDayTitleTMP.text = "Fin de la Journée " + (GameManager.Instance.day - 1) + " Mois " 
                              + GameManager.Instance.month + " Année " + GameManager.Instance.year;
        endDayMoneyStartTMP.text = GameManager.Instance.moneyAtStartOfDay + " $";
        endDayMoneyEndTMP.text = GameManager.Instance.PlayerInventory.GetBalance() + " $";
        var profit = GameManager.Instance.PlayerInventory.GetBalance() - GameManager.Instance.moneyAtStartOfDay;
        if (profit < 0)
        {
            endDayProfitTMP.color = Color.red;
        }
        else
        {
            endDayProfitTMP.color = Color.green;
        }

        endDayProfitTMP.text = profit + " $";
        background.SetActive(true);
    }
}
