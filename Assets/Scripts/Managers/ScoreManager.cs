using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    private int score;
    [SerializeField] private TMP_InputField _nameInput;

    public UnityEvent<string, int> SubmitScoreEvent;
    private void OnEnable()
    {
        EventManager.onScoreChange += ScoreChange;
    }
    private void OnDisable()
    {
        EventManager.onScoreChange -= ScoreChange;
    }

    private void ScoreChange(int quantity)
    {
        score += quantity;
    }
    public void SubmitScore()
    {
        if (_nameInput.text != null)
        {
            SubmitScoreEvent.Invoke(_nameInput.text, score);
        }
    }
}
