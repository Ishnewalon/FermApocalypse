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
        Debug.Log(GameManager.Instance._playerGender);
        if (GameManager.Instance._playerGender == "Male")
        {
            print("Male");
            Instantiate(_malePlayer, Vector3.zero, Quaternion.identity);
        }

        if (GameManager.Instance._playerGender.Equals("Non-Binary"))
        {
            print("NB");
            Instantiate(_nonBinaryPlayer, Vector3.zero, Quaternion.identity);
        }

        if (GameManager.Instance._playerGender.Equals("Female"))
        {
            print("female");
            Instantiate(_femalePlayer, Vector3.zero, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
