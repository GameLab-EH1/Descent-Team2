using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet1 : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [Header("Only For Boss")]public int Dmg;

    
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
                Destroy(gameObject);
            }
        }else if (other.gameObject.layer != 20 && other.gameObject.layer != 28)
        {
            Destroy(gameObject);
        }
    }
}
