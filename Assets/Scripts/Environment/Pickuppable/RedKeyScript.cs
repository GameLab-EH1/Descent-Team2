using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKeyScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 30)
        {
            EventManager.OnRedKeyPickup?.Invoke();
            Debug.Log("open");
            Destroy(gameObject);
        }
    }
}
