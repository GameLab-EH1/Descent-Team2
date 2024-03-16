using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDoorScript : MonoBehaviour
{
    private bool _canBeOpened;
    [SerializeField] private Transform destination;
    private float _openSpeed = 50;
    
    private void OnEnable()
    {
        EventManager.OnRedKeyPickup += CanOpenDoor;
    }
    private void OnDisable()
    {
        EventManager.OnRedKeyPickup -= CanOpenDoor;
    }
    
    private void CanOpenDoor()
    {
        _canBeOpened = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 30 && _canBeOpened)
        {
            StartCoroutine(OpenDoor());
        }
    }
    private IEnumerator OpenDoor()
    {
        Vector3 initialPosition = transform.position;
        float distance = Vector3.Distance(initialPosition, destination.position);
        float elapsedTime = 0f;

        while (elapsedTime < distance / _openSpeed)
        {
            float t = elapsedTime / (distance / _openSpeed);
            transform.position = Vector3.Lerp(initialPosition, destination.position, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = destination.position;
        
    }
}
