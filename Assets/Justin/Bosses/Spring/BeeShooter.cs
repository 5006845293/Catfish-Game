using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BeeShooter : MonoBehaviour
{

    public GameObject beePrefab;
    public float beeSpeed = 5f;
    private float timer = 0f;
    private float shootInterval = 3f;

    //public AudioClip beeClip;


    public Transform shootPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = shootInterval;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2)
        {
            timer = 0;
            shoot();

        }
    }

    void shoot()
    {
        Instantiate(beePrefab, shootPoint.position, Quaternion.identity);
        //AudioManager.instance.PlaySoundClip(beeClip, 30);

    }


}
