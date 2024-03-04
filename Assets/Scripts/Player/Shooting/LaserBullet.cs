using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    

    
    void Update()
    {
        transform.Translate(Vector3.forward * (_movementSpeed * Time.deltaTime));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 30)
        {
            gameObject.SetActive(false);
        }
    }
}
