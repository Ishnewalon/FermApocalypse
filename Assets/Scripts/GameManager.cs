using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSE,
        ENDDAY
    }

    public enum SpawnLocation
    {
        FARMFROMTOWN,
        TOWN,
        HOUSE,
        FARMFROMHOUSE
    }
    
    private SpawnLocation _currentSpawnLocation = SpawnLocation.FARMFROMTOWN;
    public Vector3 _characterSpawnPoint = new Vector3(32, 21, 0);

    public Boolean hasSpawned = false;
        
    public Events.EventGameState onGameStateChanged;
    private List<AsyncOperation> _loadOperations = new List<AsyncOperation>();
    public GameObject[] systemPrefabs;
    private List<GameObject> _instanceSystemPrefabs = new List<GameObject>();
    private GameState _currentGameState = GameState.PREGAME;
    private GameObject _menuCamera;
    private string _currentLevelName = string.Empty;
    public string _playerGender = string.Empty;
    public bool hasGameUISpawned = false;
    public double seconds;
    public double minutes;
    public double hours;
    public double day;
    public double month;
    public double year;
    public bool hasLightBeenSpawned = false;
    public void Start()
    {
        DontDestroyOnLoad(this);
        InstanciateSystemPrefab();
        _menuCamera = GameObject.FindWithTag("MainCamera");
    }
    
    void InstanciateSystemPrefab()
    {
        GameObject prefabInstance;
        for (int i = 0; i < systemPrefabs.Length; i++)
        {
            prefabInstance = Instantiate(systemPrefabs[i]);
            _instanceSystemPrefabs.Add(prefabInstance);
        }        
    }
    
    public void LoadLevel(string levelName)
    {
        _currentLevelName = levelName;
        
        AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (loadSceneAsync == null)  // La scene existe dans le build setting
        {
            print("error loading scene : " + levelName);
            return;
        }
        loadSceneAsync.completed += OnLoadSceneComplete;
        _loadOperations.Add(loadSceneAsync);
    }
    
    private void OnLoadSceneComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);
            if (_loadOperations.Count == 0)
            {
                UpdateGameState(GameState.RUNNING);

                switch (_currentLevelName)
                {
                    case "Farm":
                        if (_currentSpawnLocation == SpawnLocation.HOUSE)
                        {
                            UpdateSpawnLocation(SpawnLocation.FARMFROMHOUSE);
                        }
                        else
                        {
                            UpdateSpawnLocation(SpawnLocation.FARMFROMTOWN);
                        }
                        break;
                    case "Town":
                        UpdateSpawnLocation(SpawnLocation.TOWN);
                        break;
                    case "FarmHouse":
                        UpdateSpawnLocation(SpawnLocation.HOUSE);
                        break;
                }
            }
        }
        print("load completed");
    }
    
    public void UnloadLevel(string levelName)
    {
        AsyncOperation unloadSceneAsync = SceneManager.UnloadSceneAsync(levelName);
        if (unloadSceneAsync == null)
        {
            print("error unloading scene : " + levelName);
            return;
        }

        unloadSceneAsync.completed += OnUnloadSceneComplete;
    }
    
    private void OnUnloadSceneComplete(AsyncOperation obj)
    {
        print("unload completed");
    }
    
    void UpdateGameState(GameState newGameState)
    {
        var previousGameState = _currentGameState;
        _currentGameState = newGameState;
        switch (_currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1;
                break;
            case GameState.RUNNING:
                Time.timeScale = 1;
                break;
            case GameState.PAUSE:
                Time.timeScale = 0;
                break;
            case GameState.ENDDAY:
                Time.timeScale = 0;
                break;
        }
    
        onGameStateChanged.Invoke(_currentGameState, previousGameState);
    }
    
    public GameState CurrentGameState
    {
        get => _currentGameState;
        private set => _currentGameState = value;
    }
    
    void UpdateSpawnLocation(SpawnLocation newSpawnLocation)
    {
        _currentSpawnLocation = newSpawnLocation;
        switch (_currentSpawnLocation)
        {
            case SpawnLocation.FARMFROMTOWN:
                _characterSpawnPoint = new Vector3(32, 21, 0);
                break;
            case SpawnLocation.TOWN:
                _characterSpawnPoint = new Vector3(-26, 13, 0);
                break;
            case SpawnLocation.HOUSE:
                _characterSpawnPoint = new Vector3(-10, 14, 0);
                break;
            case SpawnLocation.FARMFROMHOUSE:
                _characterSpawnPoint = new Vector3(9.5f, 5f, 0);
                break;
        }
    }
    
    public SpawnLocation CurrentSpawnLocation
    {
        get => _currentSpawnLocation;
        private set => _currentSpawnLocation = value;
    }
    
    public void StartGame()
    {
        _menuCamera.GetComponent<AudioListener>().enabled = false;
        LoadLevel("Farm");
        hours = 15;
        minutes = 0;
        seconds = 0;
        day = 1;
        month = 1;
        year = 1;
    }
    
    public void TogglePause()
    {
        UpdateGameState(_currentGameState == GameState.RUNNING ? GameState.PAUSE : GameState.RUNNING);
    }

    public void QuitGame()
    {
        print("Quitting game");
        Application.Quit();
    }

    public void SetCharacterGender(string gender)
    {
        _playerGender = gender;
    }
    
    public void ToggleNewDay()
    {
        UpdateGameState(_currentGameState == GameState.RUNNING ? GameState.ENDDAY : GameState.RUNNING);
    }
}
