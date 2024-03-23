using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    public float damage = 60f;
    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        float speed = 100f;
        bulletRigidbody.velocity = transform.forward * speed;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<EnemyHealth>() != null)
        {
            other.gameObject.GetComponent<EnemyHealth>().DealDamage(damage);
        }
        else if (other.gameObject.GetComponentInParent<EnemyHealth>() != null)
        {
            other.gameObject.GetComponentInParent<EnemyHealth>().DealDamage(damage);
        }
        Destroy(gameObject);
    }
}
