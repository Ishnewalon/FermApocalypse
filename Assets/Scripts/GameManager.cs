using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
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
        ENDDAY,
        GOTOBED
    }

    public enum SpawnLocation
    {
        FARMFROMTOWN,
        TOWN,
        HOUSE,
        FARMFROMHOUSE,
        NEARBED
    }
    
    public SpawnLocation _currentSpawnLocation = SpawnLocation.FARMFROMTOWN;
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
    public bool hasMusicPlayerSpawned = false;
    private Sprite[] _allPlantSprites;
    public Dictionary<Enum, List<Sprite>> _plantSprites = new Dictionary<Enum, List<Sprite>>();
    private Dictionary<String, Enum> _plantNameToItemType = new Dictionary<String, Enum>();
    public List<Enum> _cropSeedEnum = new List<Enum>(); 
    public Enum _currentHeldItem = Item.ItemType.PumpkinSeed;
    public List<PlantStateData> _allPlantStates = new List<PlantStateData>();
    
    public void Start()
    {
        DontDestroyOnLoad(this);
        InstanciateSystemPrefab();
        _menuCamera = GameObject.FindWithTag("MainCamera");
        _allPlantSprites = Resources.LoadAll<Sprite>("Sprites/Plants/plants");
        SetUpCropSprites();
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
        if (loadSceneAsync == null)  
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
                switch (_currentLevelName)
                {
                    case "Farm":
                        if (_currentSpawnLocation == SpawnLocation.HOUSE ||
                            _currentSpawnLocation == SpawnLocation.NEARBED)
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
                        if (_currentSpawnLocation != SpawnLocation.FARMFROMHOUSE)
                        {
                            UpdateSpawnLocation(SpawnLocation.NEARBED);
                        }
                        else
                        {
                            UpdateSpawnLocation(SpawnLocation.HOUSE);    
                        }
                        break;
                }
                LoadPlantStates();
                UpdateGameState(GameState.RUNNING);
            }
        }
        print("load completed");
    }
    
    public void UnloadLevel(string levelName)
    {
        if (levelName.Equals("Farm"))
        {
            SavePlantStates();
        }
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
            case GameState.GOTOBED:
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
            case SpawnLocation.NEARBED:
                _characterSpawnPoint = new Vector3(-12.20f, 16, 0);
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
        hours = 15;         //TODO Change back time to 7 for start of day
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
        hours = (hours == 1 ? 0 : 7) ;
        var tempState = GameState.RUNNING;
        switch (_currentGameState)
        {
            case GameState.RUNNING:
                tempState = GameState.ENDDAY;
                break;
            case GameState.ENDDAY:
                tempState = GameState.RUNNING;
                break;
            case GameState.GOTOBED:
                tempState = GameState.ENDDAY;
                break;
        }
        UpdateGameState(tempState);
    }

    public void ToggleGoToBedDialog()
    {
        UpdateGameState(_currentGameState == GameState.RUNNING ? GameState.GOTOBED : GameState.RUNNING);
    }

    private void SetUpCropSprites()
    {
        FillCropDictionaries();
        for (int i = 0; i < _allPlantSprites.Length; i++)
        {
            String spriteName = _allPlantSprites[i].name.Substring(0, _allPlantSprites[i].name.IndexOf("_"));
            print(spriteName);
            _plantSprites[_plantNameToItemType[spriteName]].Add(_allPlantSprites[i]);
        }
    }

    private void FillCropDictionaries()
    {
        _plantNameToItemType.Add("bean", Item.ItemType.BeanSeed);
        _plantNameToItemType.Add("blueberry",Item.ItemType.BlueberrySeed);
        _plantNameToItemType.Add("cabbage", Item.ItemType.CabbageSeed);
        _plantNameToItemType.Add("carrot", Item.ItemType.CarrotSeed);
        _plantNameToItemType.Add("cauliflower", Item.ItemType.CauliflowerSeed);
        _plantNameToItemType.Add("celery", Item.ItemType.CelerySeed);
        _plantNameToItemType.Add("corn", Item.ItemType.CornSeed);
        _plantNameToItemType.Add("eggplant", Item.ItemType.EggplantSeed);
        _plantNameToItemType.Add("grape", Item.ItemType.GrapeSeed);
        _plantNameToItemType.Add("leak", Item.ItemType.LeakSeed);
        _plantNameToItemType.Add("onion", Item.ItemType.OnionSeed);
        _plantNameToItemType.Add("pepper", Item.ItemType.PepperSeed);
        _plantNameToItemType.Add("pineapple", Item.ItemType.PineappleSeed);
        _plantNameToItemType.Add("pumpkin", Item.ItemType.PumpkinSeed);
        _plantNameToItemType.Add("rockmelon", Item.ItemType.RockmelonSeed);
        _plantNameToItemType.Add("squash", Item.ItemType.SquashSeed);
        _plantNameToItemType.Add("strawberry", Item.ItemType.StrawberrySeed);
        _plantNameToItemType.Add("tomato", Item.ItemType.TomatoSeed);
        _plantNameToItemType.Add("turnip", Item.ItemType.TurnipSeed);
        _plantNameToItemType.Add("watermelon", Item.ItemType.WatermelonSeed);
        
        _cropSeedEnum.Add(Item.ItemType.BeanSeed);
        _cropSeedEnum.Add(Item.ItemType.BlueberrySeed);
        _cropSeedEnum.Add(Item.ItemType.CabbageSeed);
        _cropSeedEnum.Add(Item.ItemType.CarrotSeed);
        _cropSeedEnum.Add(Item.ItemType.CauliflowerSeed);
        _cropSeedEnum.Add(Item.ItemType.CelerySeed);
        _cropSeedEnum.Add(Item.ItemType.CornSeed);
        _cropSeedEnum.Add(Item.ItemType.EggplantSeed);
        _cropSeedEnum.Add(Item.ItemType.GrapeSeed);
        _cropSeedEnum.Add(Item.ItemType.LeakSeed);
        _cropSeedEnum.Add(Item.ItemType.OnionSeed);
        _cropSeedEnum.Add(Item.ItemType.PepperSeed);
        _cropSeedEnum.Add(Item.ItemType.PineappleSeed);
        _cropSeedEnum.Add(Item.ItemType.PumpkinSeed);
        _cropSeedEnum.Add(Item.ItemType.RockmelonSeed);
        _cropSeedEnum.Add(Item.ItemType.SquashSeed);
        _cropSeedEnum.Add(Item.ItemType.StrawberrySeed);
        _cropSeedEnum.Add(Item.ItemType.TomatoSeed);
        _cropSeedEnum.Add(Item.ItemType.TurnipSeed);
        _cropSeedEnum.Add(Item.ItemType.WatermelonSeed);
        
        List<Sprite> _bean = new List<Sprite>();
        List<Sprite> _blueberry = new List<Sprite>(); 
        List<Sprite> _cabbage = new List<Sprite>();
        List<Sprite> _carrot = new List<Sprite>();
        List<Sprite> _cauliflower = new List<Sprite>();
        List<Sprite> _celery = new List<Sprite>();
        List<Sprite> _corn = new List<Sprite>();
        List<Sprite> _eggplant = new List<Sprite>(); 
        List<Sprite> _grape = new List<Sprite>();
        List<Sprite> _leak = new List<Sprite>();
        List<Sprite> _onion = new List<Sprite>();
        List<Sprite> _pepper = new List<Sprite>();
        List<Sprite> _pineapple = new List<Sprite>();
        List<Sprite> _pumpkin = new List<Sprite>();
        List<Sprite> _rockmelon = new List<Sprite>();
        List<Sprite> _squash = new List<Sprite>();
        List<Sprite> _strawberry = new List<Sprite>();
        List<Sprite> _tomato = new List<Sprite>();
        List<Sprite> _turnip = new List<Sprite>();
        List<Sprite> _watermelon = new List<Sprite>();
        
        _plantSprites.Add(Item.ItemType.BeanSeed, _bean );
        _plantSprites.Add(Item.ItemType.BlueberrySeed, _blueberry);
        _plantSprites.Add(Item.ItemType.CabbageSeed, _cabbage);
        _plantSprites.Add(Item.ItemType.CarrotSeed, _carrot);
        _plantSprites.Add(Item.ItemType.CauliflowerSeed, _cauliflower);
        _plantSprites.Add(Item.ItemType.CelerySeed, _celery);
        _plantSprites.Add(Item.ItemType.CornSeed, _corn);
        _plantSprites.Add(Item.ItemType.EggplantSeed, _eggplant);
        _plantSprites.Add(Item.ItemType.GrapeSeed, _grape);
        _plantSprites.Add(Item.ItemType.LeakSeed, _leak);
        _plantSprites.Add(Item.ItemType.OnionSeed, _onion);
        _plantSprites.Add(Item.ItemType.PepperSeed, _pepper);
        _plantSprites.Add(Item.ItemType.PineappleSeed, _pineapple);
        _plantSprites.Add(Item.ItemType.PumpkinSeed, _pumpkin);
        _plantSprites.Add(Item.ItemType.RockmelonSeed, _rockmelon);
        _plantSprites.Add(Item.ItemType.SquashSeed, _squash);
        _plantSprites.Add(Item.ItemType.StrawberrySeed, _strawberry);
        _plantSprites.Add(Item.ItemType.TomatoSeed, _tomato);
        _plantSprites.Add(Item.ItemType.TurnipSeed, _turnip);
        _plantSprites.Add(Item.ItemType.WatermelonSeed, _watermelon);
    }

    public void SavePlantStates()
    {
        _allPlantStates.Clear();
        GameObject[] allFarmableTerrain = GameObject.FindGameObjectsWithTag("farmable");
        for (int i = 0; i < allFarmableTerrain.Length; i++)
        {
            print(allFarmableTerrain[i]);
            _allPlantStates.Add(allFarmableTerrain[i].GetComponent<TerrainManager>().getStateData());
        }
    }

    public void LoadPlantStates()
    {
        GameObject[] allFarmableTerrain = GameObject.FindGameObjectsWithTag("farmable");
        if (_allPlantStates.Count > 0)
        {
            for (int i = 0; i < allFarmableTerrain.Length; i++)
            {
                allFarmableTerrain[i].GetComponent<TerrainManager>().setStateData(_allPlantStates[i]);
            }
        }
    }

    public void IncrementGrowingPlant()
    {
        foreach (var plant in _allPlantStates)
        {
            if (plant.isPlanted && plant.plantGrowthStage < 3) { 
                plant.plantGrowthStage++;
            }
        }
    }
}
