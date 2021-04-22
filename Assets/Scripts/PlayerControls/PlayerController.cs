using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float walkSpeed = 20f;

    private Vector2 _heading;
    
    private Rigidbody2D _rigidbody2D;

    private Animator _animator;
    
    private readonly int _animatorVelocity = Animator.StringToHash("velocity");
    private readonly int _animatorHeadingX = Animator.StringToHash("headingX");
    private readonly int _animatorHeadingY = Animator.StringToHash("headingY");


    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        
    }
    
    private void Update()
    {
        _heading = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        
        // if player is moving
        if (!_heading.sqrMagnitude.Equals(0f))
        {
            SetAnimationHeading();
        }

        // idle vs walk
        _animator.SetFloat(_animatorVelocity, _rigidbody2D.velocity.sqrMagnitude);

        
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.tag);
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
