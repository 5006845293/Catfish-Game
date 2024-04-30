using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveProjectile : MonoBehaviour
{
    public float speed = 2f;
    private Transform hook;
    public float timer;
    public float projLifetime = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        hook = GameObject.FindGameObjectWithTag("Player").transform;

        if(hook == null )
        {
            Debug.LogError("Player not found!");
        }

        Invoke("DestroyProj", projLifetime);
    }

    // Update is called once per frame
    void Update()
    {
        if(hook != null)
        {
            Vector3 direction = hook.position - transform.position;
            direction.Normalize();
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void DestroyProj()
    {
        Destroy(gameObject);
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
                health.TakeDamage(15);
            }

        }

    }
}

