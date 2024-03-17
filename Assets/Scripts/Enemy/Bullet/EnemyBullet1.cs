using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet1 : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [HideInInspector] public int Dmg;

    
    void Update()
    {
        transform.Translate(Vector3.forward * (_movementSpeed * Time.deltaTime));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 30)
        {
            {
                EventManager.OnShieldPickOrDmg?.Invoke(Dmg);
            }
        }
    }
}
