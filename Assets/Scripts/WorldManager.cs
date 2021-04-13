using System.Collections;
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

    public GameObject character;

    private Dictionary<string, GameObject> _characterPrefabs = new Dictionary<string, GameObject>();
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _characterPrefabs["Male"] = _malePlayer;
        _characterPrefabs["Female"] = _femalePlayer;
        _characterPrefabs["Non-Binary"] = _nonBinaryPlayer;
        
        CharacterSpawning();
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


    // Update is called once per frame
    void Update()
    {
        
    }
}
