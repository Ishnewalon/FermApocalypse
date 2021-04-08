using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject _target;

    private Vector3 _cameraOffset;

    private Vector2 _scrollDelta;

    private const float MINCamSize = 3f;

    private const float MAXCamSize = 6f;

    private Camera _camera;
    
    void Start()
    {
        _camera = GetComponent<Camera>();
        _cameraOffset = new Vector3(0, 0, -10);
    }
    
    void Update()
    {
        
        _scrollDelta = Input.mouseScrollDelta;
        
        // follow target
        if (_target != null)
        {
            transform.position = _target.transform.position + _cameraOffset;
        }

        ManageZoom();
    }

    private void LateUpdate()
    {
        if (_target == null)
        {
            _target = GameObject.FindWithTag("Player");
        }
    }

    private void ManageZoom()
    {
        if (_scrollDelta.y < 0 && _camera.orthographicSize < MAXCamSize)
        {
            _camera.orthographicSize++;
        }

        if (_scrollDelta.y > 0 && _camera.orthographicSize > MINCamSize)
        {
            _camera.orthographicSize--;
        }
    }
}
