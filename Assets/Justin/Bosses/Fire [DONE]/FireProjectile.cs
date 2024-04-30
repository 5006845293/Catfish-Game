using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Rendering;

public class FireProjectile : MonoBehaviour
{

    public float speed = 5f;
    public float amplitude = 1f;
    public float frequency = 1f;
    public float lifespan = 3f;

    private Vector3 direction;
    private Vector3 initPos;
    private GameObject hook;
    

    
    
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        hook = GameObject.FindGameObjectWithTag("Player");

        if(hook != null )
        {
            direction = (hook.transform.forward - initPos).normalized;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(hook == null)
        {
            return;
        }

        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        Vector3 offset = new Vector3(0f, yOffset, 0f);

        Vector3 nextPosition = initPos + direction * speed * Time.deltaTime + offset;
        transform.position = nextPosition;

        Destroy(gameObject, lifespan);
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
            health.TakeDamage(20);
        }

    }

   
}
