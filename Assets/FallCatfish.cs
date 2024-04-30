using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

// PREFAB TO BE SPAWNED INTO BOSS SCENE WHEN THE CORRECT COMBINATION OF FISH ARE CAUGHT

public class FallCatfish : MonoBehaviour
{

    [SerializeField] private float speed = 7f;
    [SerializeField] private float boundary = 4f;


    public GameObject waterProj;
    public Transform projPos;

    private Vector3 targetPosition;
    private Vector3[] directions = { Vector3.right, Vector3.down, Vector3.left, Vector3.up };
    private int currentDirIndex = 0;

    private float timer;
    [SerializeField] private AudioClip waterProjSFX;
    void Start()
    {
        targetPosition = transform.position + directions[currentDirIndex] * boundary;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if(transform.position == targetPosition)
        {
            currentDirIndex = (currentDirIndex + 1) % directions.Length;
            targetPosition = transform.position + directions[currentDirIndex] * boundary;
        }

        timer += Time.deltaTime;

        if (timer > 2)
        {
            timer = 0;
            shoot();

        }


    }

    void shoot()
    {
        Instantiate(waterProj, projPos.position, Quaternion.identity);
        AudioManager.instance.PlaySoundClip(waterProjSFX, 30);

    }
}
