using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

// PREFAB TO BE SPAWNED INTO BOSS SCENE WHEN THE CORRECT COMBINATION OF FISH ARE CAUGHT

public class WaterCatfish : MonoBehaviour
{

    [SerializeField] private float speed = 7f;
    [SerializeField] private float leftPos = -4f;
    [SerializeField] private float rightPos = 4f;

    public GameObject waterProj;
    public Transform projPos;

    private Vector3 dir = Vector3.left;
    private float timer;
    [SerializeField] private AudioClip waterProjSFX;

    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);

        if (transform.position.x <= leftPos)
        {
            dir = Vector3.right;
        }
        
        else if(transform.position.x >= rightPos)
        {
            dir = Vector3.left;
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
