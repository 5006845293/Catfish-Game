using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

// PREFAB TO BE SPAWNED INTO BOSS SCENE WHEN THE CORRECT COMBINATION OF FISH ARE CAUGHT

public class WinterCatfish : MonoBehaviour
{
    public float minX = -4f;
    public float maxX = 4f; 
    public float minY = -4f; 
    public float maxY = 4f; 
    public float speed = 4f;

    private Vector3 targetPosition;
    private float timer;

    public GameObject waterProj;
    public Transform projPos;
    [SerializeField] private AudioClip waterProjSFX;

    void Start()
    {
        targetPosition = GetRandomPosition();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            targetPosition = GetRandomPosition();
        }

        timer += Time.deltaTime;

        if (timer > 2)
        {
            timer = 0;
            shoot();

        }
    }

    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        return new Vector3(randomX, randomY, transform.position.z);
    }

    void shoot()
    {
        Instantiate(waterProj, projPos.position, Quaternion.identity);
        AudioManager.instance.PlaySoundClip(waterProjSFX, 30);

    }
}
