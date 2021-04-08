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
    
    // Start is called before the first frame update
    void Start()
    {
        CharacterSpawning();
    }

    private void CharacterSpawning()
    {
        if (GameManager.Instance._playerGender == "Male")
        {
            character = Instantiate(_malePlayer, Vector3.zero, Quaternion.identity);
        }

        if (GameManager.Instance._playerGender.Equals("Non-Binary"))
        {
            character = Instantiate(_nonBinaryPlayer, Vector3.zero, Quaternion.identity);
        }

        if (GameManager.Instance._playerGender.Equals("Female"))
        {
            character = Instantiate(_femalePlayer, Vector3.zero, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
