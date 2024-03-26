using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textComponent;
    [SerializeField] private string[] _lines;
    [SerializeField] private float _textSpeed;
    [SerializeField] private GameObject _bibiTalk, _waxTalk, _waxPresentation;
    

    private bool _isControllerPressed;
    private bool _isTextFullyWritten;
    
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
            if (_isTextFullyWritten)
            {
                if (_bibiTalk.activeSelf)
                {
                    _waxTalk.SetActive(true);
                    _bibiTalk.SetActive(false);
                }
                else
                {
                    _bibiTalk.SetActive(true);
                    _waxTalk.SetActive(false);
                }
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                _textComponent.text = _lines[index];
                _isTextFullyWritten = true;
            }
        }
    }
    private void StartDialogue()
    {
        index = 0;
        _isTextFullyWritten = false;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (Char c in _lines[index].ToCharArray())
        {
            _textComponent.text += c;
            yield return new WaitForSeconds(_textSpeed);
        }
        _isTextFullyWritten = true;
    }
    private void NextLine()
    {
        if (index < _lines.Length - 1)
        {
            index++;
            _textComponent.text = String.Empty;
            _isTextFullyWritten = false;
            StartCoroutine(TypeLine());
        }
        else if (index >= _lines.Length - 1)
        {
            _waxTalk.SetActive(false);
            _bibiTalk.SetActive(false);
            _textComponent.gameObject.SetActive(false);
            _waxPresentation.SetActive(true);
        }
    }

    public bool IsNextString()
    {
        return _isTextFullyWritten;
    }
}
