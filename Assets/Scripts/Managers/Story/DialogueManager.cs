using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textComponent;
    [SerializeField] private string[] _lines;
    [SerializeField] private float _textSpeed;

    private bool _isControllerPressed;

    private int index;

    private void Start()
    {
        _textComponent.text = String.Empty;
        StartDialogue();
    }
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
            if (_textComponent.text == _lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                _textComponent.text = _lines[index];
            }
        }
    }
    private void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (Char c in _lines[index].ToCharArray())
        {
            _textComponent.text += c;
            yield return new WaitForSeconds(_textSpeed);
        }
    }
    private void NextLine()
    {
        if (index < _lines.Length - 1)
        {
            index++;
            _textComponent.text =String.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
