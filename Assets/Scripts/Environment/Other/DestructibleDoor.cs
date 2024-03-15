using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DestructibleDoor : MonoBehaviour
{
    [SerializeField] private GameObject[] _states;
    [SerializeField] private int _hp;
    private int _hpSaver;
    private int _statesCounter;

    private void Start()
    {
        _hpSaver = _hp;
        _statesCounter = _states.Length;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.gameObject.layer == 31)
        {
            GotDmg();
        }
    }

    private void GotDmg()
    {
        if (_hp > 0)
        {
            _hp--;
        }
        else if (_statesCounter > 1)
        {
            _statesCounter--;
            _hp = _hpSaver;
            ChangeState();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void ChangeState()
    {
        _states[_statesCounter - 1].SetActive(true);
        _states[_statesCounter].SetActive(false);
    }
}
