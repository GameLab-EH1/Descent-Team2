using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKeyScript : MonoBehaviour
{
    [SerializeField] private int _scoreToAdd;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 30)
        {
            EventManager.OnRedKeyPickup?.Invoke();
            EventManager.onScoreChange?.Invoke(_scoreToAdd);
            Destroy(gameObject);
        }
    }
}
