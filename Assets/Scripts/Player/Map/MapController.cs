using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private GameObject _playerPointObject;
    [SerializeField] private Transform _player;
    private Transform _middlePoint;

    private void OnEnable()
    {
        float currentZ = _player.position.z;
        float zValue = currentZ - 1000;
        Vector3 newPosition = new Vector3(_player.position.x, _player.position.y, zValue);
        _playerPointObject.transform.position = newPosition;
        transform.LookAt(_playerPointObject.transform.position);
    }
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        float controllerHorizontalInput = Input.GetAxis("ControllerHorizontal");
        float controllerVerticalInput = Input.GetAxis("ControllerVertical");
        
        float inputX = Mathf.Abs(controllerHorizontalInput) > Mathf.Abs(horizontalInput) ? controllerHorizontalInput : horizontalInput;
        float inputY = Mathf.Abs(controllerVerticalInput) > Mathf.Abs(verticalInput) ? controllerVerticalInput : verticalInput;
        float angleX = inputX * _movementSpeed * Time.deltaTime;
        float angleY = inputY * _movementSpeed * Time.deltaTime;

        
        transform.RotateAround(_playerPointObject.transform.position, Vector3.up, angleX);
        transform.RotateAround(_playerPointObject.transform.position, transform.right, -angleY); 
    }
}
