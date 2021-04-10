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
        Vector3 startPosition = Vector3.zero;
        Debug.Log(GameManager.Instance._playerGender);
        if (GameManager.Instance._playerGender == null)
        {
            GameManager.Instance.SetCharacterGender("Male");
        }

        if (SceneManager.GetActiveScene().name.Equals("Farm"))
        {
            startPosition = new Vector3(-22, 28, 0);
        }
        else if (SceneManager.GetActiveScene().name.Equals("Town") || SceneManager.GetActiveScene().name.Equals("MainMenu"))
        {
            startPosition = new Vector3(32, 21, 0);
        }
        Debug.Log(SceneManager.GetActiveScene().name);
        character = Instantiate(_characterPrefabs[GameManager.Instance._playerGender], startPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
