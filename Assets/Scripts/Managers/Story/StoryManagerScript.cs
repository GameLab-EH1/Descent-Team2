using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class StoryManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _storyImages;
    [SerializeField] private float _timeForEachFrame;
    private float _timer;
    private bool _isControllerButtonPressed;
    private int _index;



    [SerializeField] private GameObject story1, nextFrame;


    private void Update()
    {
        for (int i = 0;i < 20; i++) 
        {
            if(Input.GetKeyDown("joystick 1 button "+i))
            {
                    _isControllerButtonPressed = true;
            }
        }
        if (_index < _storyImages.Length && story1.activeSelf)
        {
            FirstScrollingStory();
        }
        else
        {
            story1.SetActive(false);
            nextFrame.SetActive(true);
        }
        
    }

    private void FirstScrollingStory()
    {
        _timer += Time.deltaTime;
        if (_timer > _timeForEachFrame || Input.anyKeyDown || _isControllerButtonPressed)
        {
            _storyImages[_index].SetActive(false);
            _index++;
            _isControllerButtonPressed = false;
            _timer = 0;
        }
    }
}
