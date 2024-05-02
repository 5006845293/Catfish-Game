using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MegaCatfish : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float minX = -5f;
    public float maxX = 5f;
    public float minY = -3f;
    public float maxY = 3f;
    public int projCount = 3;

    public GameObject megaProj;
    public Transform shootPoint;
    public float shootInterval = 2f;
    public AudioClip megaProjSFX;

    private float lastShootTime;
    private float timer = 0f;

    private Vector3 targetPos;

    [SerializeField] private float projAngle = 40f;
    
    // Start is called before the first frame update
    void Start()
    {
        targetPos = GetRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            targetPos = GetRandomPosition();
        }

        if(Time.time - lastShootTime > shootInterval)
        {
            ShootProjectile();
            lastShootTime = Time.time;
        }
    }

    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector3(randomX, randomY, transform.position.z);
    }

    void ShootProjectile()
    {
        for (int i = 0; i < projCount; i++)
        {
            float angle = Mathf.Lerp(-projAngle, projAngle, (float)i / (projCount - 1));
            GameObject projectile = Instantiate(megaProj, shootPoint.position, Quaternion.Euler(0f, 0f, angle));
            projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.right * projectile.GetComponent<MagicProj>().moveSpeed;
        }


        
    }



}
