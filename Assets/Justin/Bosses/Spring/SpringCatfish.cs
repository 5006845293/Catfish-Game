using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringCatfish : MonoBehaviour
{

    public float speed = 3f;
    public float mvmtRange = 4f;
    public GameObject flowerPrefab;
    public Transform flowerSpawnLeft;
    public Transform flowerSpawnRight;
    public float flowerSpeed = 5f;

    private Vector3 initPos;
    private bool flowersActive = false;
    private GameObject flower1;
    private GameObject flower2;

    public int objectsToDestroy = 2;
    private int destroyedObjects = 0;
    public List<GameObject> FlowerList;
    public HealthComponent health;
    
    // Start is called before the first frame update
    //spawn flowers that shoot thorns at player
    void Start()
    {
        
        if(health != null)
        {
            health.canBeDamaged = false;
        }
       
        initPos = transform.position;
        InvokeRepeating("SpawnFlowers", 1f, 8.5f);
    }

    // Update is called once per frame
    void Update()
    {
        float xMvmt = Mathf.Sin(Time.time * speed) * mvmtRange;
        float yMvmt = Mathf.Cos(Time.time * speed) * mvmtRange;

        Vector3 targetPos = initPos + new Vector3(xMvmt, yMvmt, 0f);

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    void SpawnFlowers()
    {
        
        if(!flowersActive)
        {
             flower1 = Instantiate(flowerPrefab, flowerSpawnLeft.position, Quaternion.identity);
             flower2 = Instantiate(flowerPrefab, flowerSpawnRight.position, Quaternion.identity);

            flowersActive = true;

            Invoke("CheckFlowersDestroy", 0.5f);
        }
        
    }

    void CheckFlowersDestroy()
    {
        if(flower1 == null && flower2 == null)
        {
            EnableDamage();
            flowersActive = false;
        }

        else
        {
            Invoke("CheckFlowersDestroy", 0.5f);
            
        }
    }

    private void EnableDamage()
    {
        health.canBeDamaged = true;
    }

}
