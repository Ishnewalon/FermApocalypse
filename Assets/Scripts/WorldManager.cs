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
    
    public GameObject character;

    private Dictionary<string, GameObject> _characterPrefabs = new Dictionary<string, GameObject>();
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnLight();
        _characterPrefabs["Male"] = _malePlayer;
        _characterPrefabs["Female"] = _femalePlayer;
        _characterPrefabs["Non-Binary"] = _nonBinaryPlayer;
        SpawnGameUI();
        CharacterSpawning();
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
        Debug.Log(GameManager.Instance._playerGender);
        
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

        Debug.Log(SceneManager.GetActiveScene().name);
    }

    public void SpawnGameUI()
    {
        if (!GameManager.Instance.hasGameUISpawned)
        {
            Instantiate(_gameUI, transform.position, transform.rotation);
            GameManager.Instance.hasGameUISpawned = true;
        }
    }
}
