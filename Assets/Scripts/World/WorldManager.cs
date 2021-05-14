using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _malePlayer;
    
    [SerializeField]
    private GameObject _femalePlayer;
    
    [SerializeField]
    private GameObject _nonBinaryPlayer;

    [SerializeField] 
    private GameObject _gameUI;

    [SerializeField] 
    private GameObject _globalLight;

    [SerializeField] 
    private GameObject _musicPlayer;
    
    public GameObject character;

    private Dictionary<string, GameObject> _characterPrefabs = new Dictionary<string, GameObject>();
    
    
    
    void Start()
    {
        SpawnLight();
        _characterPrefabs["Male"] = _malePlayer;
        _characterPrefabs["Female"] = _femalePlayer;
        _characterPrefabs["Non-Binary"] = _nonBinaryPlayer;
        SpawnMusicPlayer();
        CharacterSpawning();
        SpawnGameUI();
    }

    public void SpawnLight()
    {
        if (!GameManager.Instance.hasLightBeenSpawned)
        {
            Instantiate(_globalLight, transform.position, transform.rotation);
            GameManager.Instance.hasLightBeenSpawned = true;
        }
    }

    private void CharacterSpawning()
    {

        if (GameManager.Instance._playerGender == null)
        {
            GameManager.Instance.SetCharacterGender("Male");
        }

        if (!GameManager.Instance.hasSpawned)
        {
            character = Instantiate(_characterPrefabs[GameManager.Instance._playerGender], GameManager.Instance._characterSpawnPoint, Quaternion.identity);
            GameManager.Instance.hasSpawned = true;
        }
        else
        {
            character = GameObject.FindWithTag("Player");
            character.transform.position = GameManager.Instance._characterSpawnPoint;
        }
    }

    public void SpawnGameUI()
    {
        if (!GameManager.Instance.hasGameUISpawned)
        {
            Instantiate(_gameUI, transform.position, transform.rotation);
            GameManager.Instance.hasGameUISpawned = true;
        }
    }

    public void SpawnMusicPlayer()
    {
        if (!GameManager.Instance.hasMusicPlayerSpawned)
        {
            Instantiate(_musicPlayer, transform.position, transform.rotation);
            GameManager.Instance.hasMusicPlayerSpawned = true;
        }
    }
}
