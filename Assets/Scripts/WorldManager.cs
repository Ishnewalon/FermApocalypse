using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        character = Instantiate(_characterPrefabs[GameManager.Instance._playerGender], Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
