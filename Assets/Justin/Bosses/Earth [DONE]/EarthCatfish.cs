using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class EarthCatfish : MonoBehaviour
{
    public float speed = 2f;
    public float rotationSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 mvmtDir;

    public GameObject mudball;
    public float shootInterval = 2f;
    public float projectileSpeed;
    public AudioClip mudballSFX;

    public Transform hook;
    private bool canShoot = true;
    private float timer = 0f;

    public HealthComponent health;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if(health != null)
        {
            health.canBeDamaged = true;
        }
        
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        mvmtDir = new Vector2(horizInput, vertInput);

        if(canShoot)
        {
            Shoot();
            canShoot = false;
            timer = 0f;
        }

        else
        {
            timer += Time.deltaTime;
            if(timer >= shootInterval)
            {
                canShoot = true;
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        Vector2 velocity = mvmtDir * speed;
        rb.velocity = velocity;
    }

    private void Rotate()
    {
        float angle = Mathf.Atan2(mvmtDir.y, mvmtDir.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(mudball, transform.position, Quaternion.identity);
        AudioManager.instance.PlaySoundClip(mudballSFX, 10);
        Vector2 direction = (hook.position - transform.position).normalized;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;
    }
}
