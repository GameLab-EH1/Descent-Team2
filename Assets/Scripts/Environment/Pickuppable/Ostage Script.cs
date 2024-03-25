using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OstageScript : MonoBehaviour
{
    [SerializeField] private int _scoreToAdd;
    [SerializeField] private AudioClip _pickUpSound;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        LookAtLerped(transform, _mainCamera.transform, 1f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 30)
        {
            EventManager.onScoreChange?.Invoke(_scoreToAdd);
            AudioManager.instance.PlaySoundEffect(_pickUpSound, transform, 1f);
            Destroy(gameObject);
        }
    }
    private void LookAtLerped(Transform self, Transform target, float t)
    {
        Vector3 relativePos = target.position - self.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        self.rotation = Quaternion.Lerp(self.rotation, toRotation, t);
    }
}
