using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class LoveCatfish : MonoBehaviour
{

    public Transform hook;
    public float speed = 5f;
    public float followDist = 2f;

    private bool isFollowing = false;
    

    private void Start()
    {
        hook = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        Vector2 distToPlayer = transform.position - hook.position;
        
        transform.position = Vector2.MoveTowards(transform.position, hook.position, speed * Time.deltaTime);
    }

}
