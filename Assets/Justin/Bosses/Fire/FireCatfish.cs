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
    public Transform flamePoint;
    public GameObject flamePrefab;
    public float flameDuration = 1f;
    public float flameDelay = 2f;

    private bool isFiring = false;
    
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireFlames());
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

        }

    private IEnumerator FireFlames()
    {
        while(true)
        {
            if(isFiring)
            {
                GameObject flames = Instantiate(flamePrefab, flamePoint.position, Quaternion.identity);
                Destroy(flames, flameDuration);

                isFiring = true;

                yield return new WaitForSeconds(flameDelay);

                isFiring = false;
            }
            else
            {
                yield return null;
            }
        }
    }
        
       

    }

    

    
        
        
        
    
        

        


