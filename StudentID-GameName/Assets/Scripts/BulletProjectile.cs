using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public float speed;

    private Rigidbody bulletRigidbody;
    public GameObject ps;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        bulletRigidbody.velocity = transform.forward * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
   
       // if(other.tag != gameObject.tag)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponentInParent<EnemyAI>().TakeDamage(5);
                var pst = Instantiate(ps, transform.position, Quaternion.identity);
                Destroy(pst, 3);
            }
                
            Destroy(gameObject);
        }
       
    }
}
