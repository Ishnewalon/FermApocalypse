using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float walkSpeed = 10f;

    private Vector2 _heading;
    
    private Rigidbody2D _rigidbody2D;

    private Animator _animator;
    
    private AudioSource _audioSource;

    private Inventory _inventory;

    [SerializeField] private UI_Inventory _uiInventory;
    
    private List<AudioClip> _audioClips = new List<AudioClip>();
    
    private readonly int _animatorVelocity = Animator.StringToHash("velocity");
    private readonly int _animatorHeadingX = Animator.StringToHash("headingX");
    private readonly int _animatorHeadingY = Animator.StringToHash("headingY");


    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _audioClips.Add((AudioClip)Resources.Load("Audio/FootSteps/StepsLoop/grass/walking/walking-5"));
        _audioClips.Add((AudioClip)Resources.Load("Audio/PickUpItem/blop"));
    }

    private void Awake()
    {
        _inventory = new Inventory();

        GameManager.Instance.PlayerInventory = _inventory;
        
        _uiInventory.SetInventory(_inventory, transform);
    }

    private void Update()
    {
        _heading = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        
        // if player is moving
        if (!_heading.sqrMagnitude.Equals(0f))
        {
            SetAnimationHeading();
        }

        if (_rigidbody2D.velocity != Vector2.zero)
        {
            _audioSource.clip = _audioClips[0];
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
        else
        {
            _audioSource.Stop();
        }

        // idle vs walk
        _animator.SetFloat(_animatorVelocity, _rigidbody2D.velocity.sqrMagnitude);

        // Toggle Inventory (tab)
        if (Input.GetButtonDown("Inventory"))
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        if (_uiInventory.go.activeInHierarchy)
        {
            _uiInventory.go.SetActive(false);
        }
        else
        {
            _uiInventory.go.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = _heading * walkSpeed;
    }

    private void SetAnimationHeading()
    {
        _animator.SetFloat(_animatorHeadingX, _heading.x);
        _animator.SetFloat(_animatorHeadingY, _heading.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ItemWorld itemWorld = other.GetComponent<ItemWorld>();
        if (itemWorld != null && !_inventory.isFull)
        {
            _audioSource.PlayOneShot(_audioClips[1], 1);
            _inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("goToTown"))
        {
            GameManager.Instance.UnloadLevel("Farm");
            GameManager.Instance.LoadLevel("Town");
        }
        else if (other.gameObject.tag.Equals("goToFarm"))
        {
            GameManager.Instance.UnloadLevel("Town");
            GameManager.Instance.LoadLevel("Farm");
        }
        else if (other.gameObject.tag.Equals("enterHouse"))
        {
            GameManager.Instance.UnloadLevel("Farm");
            GameManager.Instance.LoadLevel("FarmHouse");
        }
        else if (other.gameObject.tag.Equals("exitHouse"))
        {
            GameManager.Instance.UnloadLevel("FarmHouse");
            GameManager.Instance.LoadLevel("Farm");
        }
        else if (other.gameObject.tag.Equals("bed"))
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3(0.5f, 0, 0);
            GameManager.Instance.ToggleGoToBedDialog();
        }
    }


}
