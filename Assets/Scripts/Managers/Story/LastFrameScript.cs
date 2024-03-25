using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastFrameScript : MonoBehaviour
{
    [SerializeField] private float _waitUntilStartGame;
    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _waitUntilStartGame)
        {
            SceneManager.LoadScene("Game");
        }
    }


}
