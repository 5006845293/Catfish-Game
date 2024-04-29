using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class FireCatfish : MonoBehaviour
{
    public float startValue = -3f;
    public float endValue = 3f;
    public float duration = 1f;

    private float timer;

    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootingInterval = 3f;
    public float projectileSpeed = 5f;
    public int numProj = 6;

   
    

    
    
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {

        Vector3 point = transform.position;
        Vector3 axis = new Vector3(0, 0, 1);
        
        timer += Time.deltaTime;
        float t = Mathf.PingPong(timer / duration, 1f);
        float interpolateVal = Mathf.Lerp(startValue, endValue, t);

        ;
        transform.RotateAround(point, axis, 0.3f);
        transform.position = new Vector3(interpolateVal, 0, interpolateVal);

        timer += Time.deltaTime;

        if (timer > 2)
        {
            timer = 0;
            ShootProjectiles();

        }


    }

    void ShootProjectiles()
    {
        Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
    }

    

    }

    

    
        
        
        
    
        

        


