using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class LoveCatfish : MonoBehaviour
{

    public GameObject hook;
    public float speed;
    public float followDist = 3f;

    public GameObject projPrefab;
    public Transform spawnPoint;
    public float shootingInterval = 2f;
    public AudioClip kissSFX;

    private float lastShootTime;

    private float distance;

    

    private void Start()
    {
     
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, hook.transform.position);
        Vector2 direction = hook.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if(distance < followDist)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, hook.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.back * angle);
        }

        if (Time.time - lastShootTime >= shootingInterval)
        {
            ShootProjectile();
            AudioManager.instance.PlaySoundClip(kissSFX, 20);
            lastShootTime = Time.time;
        }
    }

    void ShootProjectile()
    {
        GameObject projectile = Instantiate(projPrefab, spawnPoint.position, Quaternion.identity);
    }

}
