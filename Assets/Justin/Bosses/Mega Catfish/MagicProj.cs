using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProj : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float homingSpeed = 5f;
    public float projLifetime = 1f;

    private float timer;

    private Transform hook;
    public Vector3 moveDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        hook = GameObject.FindGameObjectWithTag("Player").transform;
        moveDirection = (hook.position - transform.position).normalized;

        Invoke("DestroyProj", projLifetime);
      
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.time;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        Vector3 targetDirection = (hook.position - transform.position).normalized;
        moveDirection = Vector3.Lerp(moveDirection, targetDirection, homingSpeed * Time.deltaTime);
        
        
        

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

    private void DestroyProj()
    {
        Destroy(gameObject);
    }


}
