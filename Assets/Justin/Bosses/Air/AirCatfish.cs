using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class AirCatfish : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float moveRange = 5f;

    public GameObject airProj;
    public Transform shootPoint;
    public int projCount = 3;
    public float shootInterval = 2f;
    public AudioClip knifeSound;

    private float lastShootTime;


    private Transform hook;
    private float timer = 0f;
    private bool isShooting = false;
    [SerializeField] float projAngle = 25f;


    private Vector2 startPos;
    private Vector2 targetPos;
    private bool movingRight = true;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        SetNewTarget();
        hook = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if ((Vector2)transform.position == targetPos)
        {
            SetNewTarget();
        }

        if(Time.time - lastShootTime > shootInterval)
        {
            ShootProjectile();
            lastShootTime = Time.time;
        }
    }

    void SetNewTarget()
    {
        float newX = Random.Range(startPos.x - moveRange, startPos.x + moveRange);
        float newY = Random.Range(startPos.y - moveRange, startPos.y + moveRange);
        targetPos = new Vector2(newX, newY);

        if(targetPos.x < transform.position.x)
        {
            FlipSprite();
        }

        else
        {
            FlipSprite();
        }
    }

    void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        movingRight = !movingRight;
    }

    void ShootProjectile()
    {
        for(int i = 0; i < projCount; i++)
        {
            float angle = Mathf.Lerp(-projAngle, projAngle, (float)i / (projCount - 1));

            GameObject projectile = Instantiate(airProj, shootPoint.position, Quaternion.Euler(0f, 0f, angle));
            projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.right * projectile.GetComponent<AirProjectile>().speed;
            AudioManager.instance.PlaySoundClip(knifeSound, 50);
        }

    }

    
}
