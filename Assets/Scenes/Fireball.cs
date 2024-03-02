using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float damage = 10;
    void Start()
    {
        Invoke("DestroyFireball", lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        MoveFixedUpdate();
        
    }
    private void MoveFixedUpdate()
    {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealth>() != null)
        {
            DamageEnemy(collision);
        }
        DestroyFireball();
    }
    private void DestroyFireball()
    {
        Destroy(gameObject);
    }
    private void DamageEnemy(Collision collision)
    {
        collision.gameObject.GetComponent<EnemyHealth>().DealDamage(damage);
    }
}
