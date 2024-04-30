using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthProjectile : MonoBehaviour
{
    public float launchSpeed = 5f;
    public float arcHeight = 2f;
    public float moveSpeed = 2f;

    private Transform hook;
    private Rigidbody2D rb;
    private Vector2 initPos;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hook = GameObject.FindGameObjectWithTag("Player").transform;
        initPos = transform.position;

        Vector2 initialVelocity = ((Vector2)hook.position - initPos).normalized;
        rb.velocity = new Vector2(initialVelocity.x, Mathf.Sqrt(2f * arcHeight * Mathf.Abs(Physics2D.gravity.y)));
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPosition = new Vector2(hook.position.x, transform.position.y);
        //transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
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
                health.TakeDamage(30);
            }

        }

    }
}
