using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MegaProj : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float homingSpeed = 5f;
    public float splitDelay = 2f;
    public float projLifetime = 3f;
    public GameObject projPrefab;
    public int numSplitProj = 3;

    public MagicProj mp;

    private Transform hook;
    private Vector3 moveDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        hook = GameObject.FindGameObjectWithTag("Player").transform;
        moveDirection = (hook.position - transform.position).normalized;
        Destroy(gameObject, projLifetime);
        Invoke("SplitProjectiles", splitDelay);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        Vector3 targetDirection = (hook.position - transform.position).normalized;
        moveDirection = Vector3.Lerp(moveDirection, targetDirection, homingSpeed * Time.deltaTime);

    }

    void SplitProjectiles()
    {
        for(int i = 0; i < numSplitProj; i++)
        {
            GameObject splitProjectile = Instantiate(projPrefab, transform.position, Quaternion.identity);
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            
            Destroy(splitProjectile, projLifetime);

        }

        Destroy(gameObject);
    }

}
