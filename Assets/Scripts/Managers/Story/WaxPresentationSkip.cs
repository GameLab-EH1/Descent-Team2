using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaxPresentationSkip : MonoBehaviour
{
    [SerializeField] private GameObject _shipShow;
    private bool _isControllerPressed;

    private void Update()
    {
        for (int i = 0;i < 20; i++) 
        {
            if(Input.GetKeyDown("joystick 1 button "+i))
            {
                _isControllerPressed = true;
            }
        }
        if (Input.anyKeyDown || _isControllerPressed)
        {
            _shipShow.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
