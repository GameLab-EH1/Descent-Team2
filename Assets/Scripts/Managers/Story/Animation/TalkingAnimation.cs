using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _frame, _frame2;
    [SerializeField] private float _animationSpeed;
    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer > _animationSpeed)
        {
            if (_frame.activeSelf)
            {
                _frame2.SetActive(true);
                _frame.SetActive(false);
            }
            else
            {
                _frame.SetActive(true);
                _frame2.SetActive(false);
            }
            _timer = 0;
        }
    }

}
