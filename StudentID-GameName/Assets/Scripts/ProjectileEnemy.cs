using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{
    public float speed;

    private Rigidbody bulletRigidbody;
    public float damage;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
       // bulletRigidbody.velocity = transform.forward * speed;
    }
    private void OnTriggerEnter(Collider other)
    {

     //   if(other.tag != "Enemy")
        {

            if(other.CompareTag("Player"))
            {
                other.GetComponentInParent<PlayerBehviour>().TakeDamage(damage);
                Destroy(gameObject);
            }
           
        }

    }
}
