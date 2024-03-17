using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen, _loseScreen;
    [SerializeField] private GameObject _pauseMenu;
    private void OnEnable()
    {
        EventManager.OnGameEnd += GameEnd;
    }
    private void OnDisable()
    {
        EventManager.OnGameEnd -= GameEnd;
    }

    

    public void GamePause(InputAction.CallbackContext cont)
    {
        if (cont.started)
        {
            if (Time.timeScale == 0f)
            {
                Time.timeScale = 1f;
                _pauseMenu.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;
                _pauseMenu.SetActive(true);
            }
        }
    }

    private void GameEnd(bool isWin)
    {
        Time.timeScale = 0f;
        if (isWin)
        {
            _winScreen.SetActive(true);
        }
        else
        {
            _loseScreen.SetActive(true);
        }
    }
}
