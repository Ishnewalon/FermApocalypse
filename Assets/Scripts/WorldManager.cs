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
    // Start is called before the first frame update
    void Start()
    {
        CharacterSpawning();
    }

    private void CharacterSpawning()
    {
        if (GameManager.Instance._playerGender.Equals("male"))
        {
            print("male");
            Instantiate(_malePlayer, Vector3.zero, Quaternion.identity);
        }

        if (GameManager.Instance._playerGender.Equals("nonBinary"))
        {
            print("NB");
            Instantiate(_femalePlayer, Vector3.zero, Quaternion.identity);
        }

        if (GameManager.Instance._playerGender.Equals("female"))
        {
            print("female");
            Instantiate(_nonBinaryPlayer, Vector3.zero, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
