using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

// PREFAB TO BE SPAWNED INTO BOSS SCENE WHEN THE CORRECT COMBINATION OF FISH ARE CAUGHT

public class RainbowCatfish : MonoBehaviour
{

    [SerializeField] private float speed = 3f;
    [SerializeField] private float radius = 2f;
    [SerializeField] private float startXPos = 1f;
    [SerializeField] private float startYPos = 0f;

    public GameObject waterProj;
    public Transform projPos;

    private Vector3 dir = Vector3.left;
    private float timer = 0f;
    [SerializeField] private AudioClip waterProjSFX;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        float Xpos = startXPos + Mathf.Cos(timer * speed) * radius;
        float Ypos = startYPos + Mathf.Sin(timer * speed) * radius;
        transform.position = new Vector3(Xpos, Ypos, transform.position.z);

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
