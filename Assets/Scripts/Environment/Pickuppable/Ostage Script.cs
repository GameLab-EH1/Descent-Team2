using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OstageScript : MonoBehaviour
{
    [SerializeField] private int _scoreToAdd;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 30)
        {
            EventManager.onScoreChange?.Invoke(_scoreToAdd);
            Destroy(gameObject);
        }
    }
}
