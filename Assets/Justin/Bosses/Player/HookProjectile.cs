using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookProjectile : MonoBehaviour
{
    public float projectileSpeed = 10f;
    public int damage;
    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Boss")
        {
            var health = collision.GetComponent<HealthComponent>();
            if (health != null)
            {
                health.TakeDamage(25);
            }
        }
    }

        // Update is called once per frame
        void Update()
        {

        }
     
}
