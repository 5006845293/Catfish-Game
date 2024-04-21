using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class WaterBullet : MonoBehaviour
{
    private GameObject hook;
    private Rigidbody2D rb;


    public float force = 2f;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hook = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = hook.transform.position - transform.position;
        rb.velocity = new Vector2 (direction.x, direction.y).normalized * force;

        //float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, rot + 135);
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.deltaTime;

        if(timer > 5)
        {
            Debug.Log("item expired");
            //Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if(collision.tag == "Player")
        {
            var health = collision.GetComponent<HealthComponent>();
            if(health != null)
            {
                health.TakeDamage(20);
            }

        }

    }

    




}
