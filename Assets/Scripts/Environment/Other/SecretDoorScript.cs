using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDoorScript : MonoBehaviour
{
    [SerializeField] private Transform destination;
    private float _openSpeed = 50;
    [SerializeField] private AudioClip openDoorAudio;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.gameObject.layer == 31)
        {
            StartCoroutine(OpenDoor());
        }
    }

    public void OpenWithMinigun()
    {
        StartCoroutine(OpenDoor());
    }

    private IEnumerator OpenDoor()
    {
        AudioManager.instance.PlaySoundEffect(openDoorAudio, transform, 1f);
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
