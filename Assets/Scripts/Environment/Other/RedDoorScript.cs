using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDoorScript : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnRedKeyPickup += OpenDoor;
    }
    private void OnDisable()
    {
        EventManager.OnRedKeyPickup -= OpenDoor;
    }
    
    private void OpenDoor()
    {
        Destroy(gameObject);
    }
}
