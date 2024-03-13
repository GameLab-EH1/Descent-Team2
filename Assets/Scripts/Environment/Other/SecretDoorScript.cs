using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDoorScript : MonoBehaviour
{
    [SerializeField] private Transform destination;
    private bool _isOpening;
    private float _openSpeed = 50;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.gameObject.layer == 31)
        {
            StartCoroutine(OpenDoor());
        }
    }
    

    private IEnumerator OpenDoor()
    {
        _isOpening = true;
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

        _isOpening = false;
    }
}
