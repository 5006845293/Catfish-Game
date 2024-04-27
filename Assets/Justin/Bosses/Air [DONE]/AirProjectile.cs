using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirProjectile : MonoBehaviour
{
    public float speed = 10f;
    public GameObject splitProjectile;
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = -transform.up * speed;
        transform.Rotate(Vector3.forward * 90);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Player")
        {
            var health = collision.GetComponent<HealthComponent>();
            if (health != null)
            {
                health.TakeDamage(10);
            }

        }
    }

    /*void Split()
    {
        Instantiate(splitProjectile, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        Instantiate(splitProjectile, transform.position - Vector3.up * 0.5f, Quaternion.identity);

       
    }*/


}
